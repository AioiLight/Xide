using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Apocalypse.Docks.Manage.StageProp
{
    public class PropClassAsmFactory
    {
        /// <summary>
        /// クラススクリプトから直接インスタンスの生成
        /// </summary>
        public static object CreateInstance(string basefile, out string classname)
        {
            return Activator.CreateInstance(CreateAsm(basefile, out classname).GetType(classname));
        }

        /// <summary>
        /// クラススクリプトからTypeの生成
        /// </summary>
        public static Type CreateType(string basefile, out string classname)
        {
            return CreateAsm(basefile, out classname).GetType(classname);
        }

        //Reference asms
        static string[] Assemblies = new[]{
        "System.dll",
        "System.Windows.Forms.dll",
        "System.Drawing.dll",
        "System.Design.dll",
        "System.Drawing.Design.dll",
        System.Windows.Forms.Application.ExecutablePath
        };

        /// <summary>
        /// クラススクリプトからアセンブリの生成
        /// </summary>
        public static Assembly CreateAsm(string basefile, out string classname)
        {
            //コードの生成
            var code = CreateCode(basefile, out classname);

            //デバッグ出力
            System.Diagnostics.Debug.WriteLine("-- output code --");
            System.Diagnostics.Debug.WriteLine(code);
            System.Diagnostics.Debug.WriteLine("-- end --");

            //コンパイラの設定
            var cscp = new CSharpCodeProvider();
            CompilerParameters param = new CompilerParameters();
            param.GenerateInMemory = true;
            param.ReferencedAssemblies.AddRange(Assemblies);

            //コンパイル
            CompilerResults cr = cscp.CompileAssemblyFromSource(param, new[] { code });
            if (cr.Errors.Count != 0)
            {
                //エラーがあれば出力
                System.Diagnostics.Debug.WriteLine("-- errors --");
                foreach (var c in cr.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(c.ToString());
                }
                System.Diagnostics.Debug.WriteLine("-- end --");
            }
            try
            {
                return cr.CompiledAssembly;
            }
            catch (Exception)
            {
                //コード生成失敗
                throw new Exception("Internal code generation failed.");
            }
        }

        /// <summary>
        /// コード生成メイン
        /// </summary>
        private static string CreateCode(string basefile, out string classname)
        {
            string script = null;

            //スクリプト内容読み込み
            using (StreamReader sr = new StreamReader(basefile))
                script = sr.ReadToEnd();

            //ベースファイル名からクラス名作成
            classname = "_INTERNAL_" + basefile.Replace(".", "");

            StringBuilder sb = new StringBuilder();

            //クラス定義
            //表示名コンバート用属性
            sb.AppendLine("[System.ComponentModel.TypeConverter(typeof(Apocalypse.Docks.Editor.StageProp.PropClassAsmFactory.PropertyDisplayConverter))]");
            sb.AppendLine("public class " + classname + ": Apocalypse.Data.IProperty{");

            //スクリプト読み込み->IEnumerable<PropDesc>->翻訳
            foreach (var item in AnalyzeScript(script))
            {
                //item.RawSourceがコードとして完結している場合、ここで挿入
                if (item.RawSourceWellFormatted)
                    sb.AppendLine(item.RawSource);

                //変数本体の作成
                sb.AppendLine("private " + item.TypeString + " _" + item.Name + "=" + item.DefaultValue + ";");

                //アクセサ生成
                //各種属性セット
                sb.AppendLine("[");

                //読み取り専用
                sb.AppendLine("System.ComponentModel.ReadOnlyAttribute(" + item.IsReadonly.ToString().ToLower() + "),");

                //カテゴリ
                if (item.Category != null)
                    sb.AppendLine("System.ComponentModel.CategoryAttribute(\"" + item.Category + "\"),");

                //デフォルト値
                if (item.DefaultValue != null && !item.ignoreDefaultValue)
                    sb.AppendLine("System.ComponentModel.DefaultValueAttribute(" + item.DefaultValue + "),");

                //説明
                if (item.Description != null)
                    sb.AppendLine("System.ComponentModel.DescriptionAttribute(\"" + item.Description + "\"),");

                //表示名
                if (item.ViewName != null)
                    sb.AppendLine("Apocalypse.Docks.Editor.StageProp.PropClassAsmFactory.PropertyDisplayNameAttribute(\"" + item.ViewName + "\"),");

                //拡張属性
                if (item.ExtraAttr != null)
                    sb.AppendLine(item.ExtraAttr);
                sb.AppendLine("]");

                //アクセサ本体
                sb.AppendLine("public " + item.TypeString + " " + item.Name + "{get{return _" + item.Name + ";}");

                //WellFormatでないRawSourceはset内に埋め込む
                if (!item.RawSourceWellFormatted && item.RawSource != null)
                {
                    sb.AppendLine("set{");
                    sb.AppendLine(item.RawSource);
                    sb.AppendLine("_" + item.Name + " = value;}}");
                }
                else
                {
                    sb.AppendLine("set{_" + item.Name + " = value;}}");
                }
            }
            //クラス終了
            sb.AppendLine("}");
            return sb.ToString();
        }

        //プロパティデータクラス
        class PropertyDescription
        {
            public string Name = null;
            public string ViewName = null;
            public string Description = null;
            public string Category = null;
            public object DefaultValue = null;
            public string TypeString = null;
            public bool IsReadonly = false;
            public string RawSource = null;
            public string ExtraAttr = null;
            public bool ignoreDefaultValue = false;
            public bool RawSourceWellFormatted = false;
        }

        //すでにenumで登録された型の一覧
        static List<string> enumTypes = new List<string>();

        /// <summary>
        /// スクリプト翻訳本体
        /// </summary>
        private static IEnumerable<PropertyDescription> AnalyzeScript(string script)
        {
            //スクリプトが無効ならすぐ抜ける
            if (script == null || script == String.Empty)
                yield break;

            //改行で区切る
            //RemoveEmptyEntriesを使うとデバッグ行番号がずれるので使わない
            var scripts = script.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            //無かったら抜ける
            if (scripts == null || scripts.Length == 0)
                yield break;

            //enum登録済型一覧の初期化
            enumTypes.Clear();

            //行ごとに実行
            for (int i = 0; i < scripts.Length; i++)
            {
                var pd = new PropertyDescription();
                try
                {
                    //■現在の状態:
                    //■type<arg> name=value ?[cat/view]desc

                    //先頭・末尾の空白文字除去
                    var line = scripts[i].Trim(new[] { ' ', '\t' });
                    //空行・コメント行は無視
                    if (line == "" || line.StartsWith("#"))
                        continue;

                    //最初に出てくるスペースを探す
                    var spidx = line.IndexOf(' ');
                    //スペース無いのはおかしいです
                    if (spidx < 0)
                        throw new Exception("Invalid Format(No space)");

                    //Typeの判別
                    GetType(line.Substring(0, spidx + 1), ref pd);
                    line = line.Remove(0, spidx + 1);

                    //■name=value ?[cat/view]desc

                    //最初に出てくる?を探す
                    spidx = line.IndexOf('?');
                    //見つかったら？
                    if (spidx >= 0)
                    {
                        //解説を読む
                        GetDescription(line.Substring(spidx), ref pd);
                        line = line.Remove(spidx);
                    }

                    //■name=value
                    
                    //空白文字除去
                    line = line.Replace(" ", "").Replace("\t", "");
                    //nameとvalueを読む
                    GetNameAndDefaultValue(line, ref pd);
                }
                catch (Exception e)
                {
                    //エラーのキャッチ
                    throw new Exception("Define Analyzing Error:" + e.Message + " @l" + i.ToString());
                }
                yield return pd;
            }
            yield break;
        }

        /// <summary>
        /// Typeの取得
        /// </summary>
        private static void GetType(string typestr, ref PropertyDescription pd)
        {
            //引数の取得(<>の中身)
            string[] option = null;
            int opv = typestr.IndexOf('<');
            if (opv >= 0)
            {
                var opt = typestr.Substring(opv);
                opt = opt.Remove(opt.Length - 2, 1);
                opt = opt.Remove(0, 1);
                option = opt.Split(',');
                typestr = typestr.Remove(opv);
            }

            //読み取り専用属性かどうか(頭に*があるかどうか)
            if (typestr.StartsWith("*"))
            {
                pd.IsReadonly = true;
                typestr = typestr.Remove(0, 1);
            }

            //タイプ判別
            switch (typestr.Trim().ToLower())
            {
                //文字列型
                case "str":
                case "string":
                    pd.TypeString = typeof(string).FullName;
                    //引数:
                    //(<最大文字数>)
                    if (option != null && option.Length > 0)
                    {
                        int val0 = int.Parse(option[0]);
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("string[] val = value.Split('\\n');");
                        sb.AppendLine("value = string.Join(\"\\n\",val);");
                        if (val0 > 0)
                        {
                            sb.AppendLine("if (value.Length > " + val0.ToString() + ")");
                            sb.AppendLine("value = value.Substring(0, " + val0.ToString() + ");");
                        }
                        pd.RawSource = sb.ToString();
                    }
                    break;
                //複数行文字列型
                case "text":
                    //引数:
                    //(<最大行数(,最大文字数)>)
                    pd.TypeString = typeof(string).FullName;
                    if (option != null && option.Length > 0)
                    {
                        int val0 = int.Parse(option[0]);
                        int val1 = option.Length >= 2 ? int.Parse(option[1]) : 0;
                        StringBuilder sb = new StringBuilder();
                        if (val0 > 0)
                        {
                            sb.AppendLine("string[] val = value.Split('\\n');");
                            sb.AppendLine("if (val.Length > " + val0.ToString() + " )");
                            sb.AppendLine("value = string.Join(\"\\n\", val, 0, " + val0.ToString() + ");");
                        }
                        if (val1 > 0)
                        {
                            sb.AppendLine("if (value.Length > " + val1.ToString() + ")");
                            sb.AppendLine("value = value.Substring(0, " + val0.ToString() + ");");
                        }
                        pd.RawSource = sb.ToString();
                    }
                    pd.ExtraAttr = "System.ComponentModel.EditorAttribute(typeof(System.ComponentModel.Design.MultilineStringEditor),typeof(System.Drawing.Design.UITypeEditor))";
                    break;
                //URL型
                case "url":
                case "uri":
                    pd.TypeString = typeof(string).FullName;
                    break;
                //FILE型
                case "file":
                    pd.TypeString = typeof(string).FullName;
                    pd.ExtraAttr = "System.ComponentModel.TypeConverter(typeof(Apocalypse.Docks.Editor.StageProp.PropClassAsmFactory.FileNameConverter))";
                    break;
                //Boolean型
                case "bool":
                case "is":
                case "boolean":
                    pd.TypeString = typeof(bool).FullName;
                    break;
                //Integer型
                case "int":
                case "integer":
                case "num":
                    pd.TypeString = typeof(int).FullName;
                    //引数:
                    //(<最大値(,最小値)>)
                    if (option != null && option.Length > 0)
                    {
                        int val0 = int.Parse(option[0]);
                        int val1 = option.Length >= 2 ? int.Parse(option[1]) : 0;
                        StringBuilder sb = new StringBuilder();
                        if (val0 > 0)
                        {
                            sb.AppendLine("if (value > " + val0.ToString() + " )");
                            sb.AppendLine("value = " + val0.ToString() + ";");
                        }
                        if (val1 > 0)
                        {
                            sb.AppendLine("if (value < " + val1.ToString() + " )");
                            sb.AppendLine("value = " + val1.ToString() + ";");
                        }
                        pd.RawSource = sb.ToString();
                    }
                    break;
                //COLOR型
                case "color":
                    pd.TypeString = typeof(System.Drawing.Color).FullName;
                    break;
                //ENUM型
                case "enum":
                    //引数(!省略不可):
                    //<Item0(,Item1(,Item2(,...)))>
                    if (option != null && option.Length > 0)
                    {
                        //optionの中身でMD5ハッシュを取り型名にする
                        pd.TypeString = "_ENUM_" + K.Snippets.Security.CalcuateMD5(string.Join(",", option), "APOCALYPSE_XPD");
                        //すでに存在しているものと衝突しないことを確認
                        if (!enumTypes.Contains(pd.TypeString))
                        {
                            StringBuilder sb = new StringBuilder();
                            //Enum型作成
                            sb.AppendLine("[System.ComponentModel.TypeConverter(typeof(Apocalypse.Docks.Editor.StageProp.PropClassAsmFactory.EnumDisplayNameConverter))]");
                            sb.AppendLine("public enum " + pd.TypeString + "{");
                            for (int i = 0; i < option.Length; i++)
                            {
                                //実態はEnumItem{0}
                                //DisplayNameで変更する
                                sb.Append("[Apocalypse.Docks.Editor.StageProp.PropClassAsmFactory.EnumDisplayName(\"" + option[i] + "\")] EnumItem" + i.ToString());
                                if (i < option.Length - 1)
                                    sb.AppendLine(",");
                            }
                            //作成完了
                            sb.AppendLine("}");
                            pd.RawSource = sb.ToString();
                            //フォーマットを満たしている
                            pd.RawSourceWellFormatted = true;
                            //型一覧に追加
                            enumTypes.Add(pd.TypeString);
                        }
                    }
                    else
                    {
                        throw new Exception("Type \"enum\" requires at least 1 argument.");
                    }
                    break;
                //ヒットしなかった
                default:
                    throw new Exception("Unknown type was used:" + typestr);
                    //pd.TypeString = typestr;
                    //if (option != null && option.Length != 0)
                    //{
                    //    pd.RawSource = String.Join(",", option);
                    //    pd.RawSourceWellFormatted = true;
                    //}
                    //break;
            }
        }

        /// <summary>
        /// 解説の取得
        /// </summary>
        private static void GetDescription(string descraw, ref PropertyDescription pd)
        {
            //最初の1文字(?)を削除
            descraw = descraw.Remove(0, 1);

            //最初が[で始まっていたら、[]の中身を取得して処理
            if (descraw.StartsWith("["))
            {
                int edp = descraw.IndexOf(']');
                if (edp < 0)
                    throw new Exception("Category descript end point (\"]\") not found.");
                //カテゴリ
                string cat = descraw.Substring(1, edp - 1);
                descraw = descraw.Substring(edp + 1);
                int evp = cat.IndexOf("/");
                if (evp > 0)
                {
                    //表示名
                    pd.ViewName = cat.Substring(evp + 1);
                    cat = cat.Remove(evp);
                }
                pd.Category = cat;
            }
            //解説
            pd.Description = descraw;
        }

        /// <summary>
        /// 名前とデフォルト値の抽出
        /// </summary>
        private static void GetNameAndDefaultValue(string nv, ref PropertyDescription pd)
        {
            //=の位置を探す
            int eqp = nv.IndexOf('=');
            //=があれば
            if (eqp > 0)
            {
                //=より後の文字を取る
                string dval = nv.Substring(eqp + 1);
                nv = nv.Remove(eqp);

                //タイプごとにデフォルト値としてパースし、セット
                if (pd.TypeString == typeof(bool).FullName)
                    pd.DefaultValue = (dval.ToLower() == "true").ToString().ToLower();
                else if (pd.TypeString == typeof(int).FullName)
                    pd.DefaultValue = int.Parse(dval);
                else if (pd.TypeString == typeof(string).FullName)
                    pd.DefaultValue = "\"" + dval + "\"";
                else if (pd.TypeString == typeof(Uri).FullName)
                    pd.DefaultValue = new Uri(dval);
                else if (pd.TypeString == typeof(System.Drawing.Color).FullName)
                {
                    pd.DefaultValue = "System.Drawing.Color.FromArgb(" + dval + ")";
                    pd.ignoreDefaultValue = true;
                }
                else if (pd.TypeString.StartsWith("_ENUM_"))
                    pd.DefaultValue = "(" + pd.TypeString + ")" + int.Parse(dval);
                else
                    throw new Exception("Unsupported valiable type to substitution.(Type:" + pd.TypeString + ")");
            }
            else
            {
                //システムのデフォルト値をセット
                if (pd.TypeString.StartsWith("_ENUM_"))
                    pd.DefaultValue = 0;
                else
                {
                    if (Type.GetType(pd.TypeString).IsClass)
                        pd.DefaultValue = null;
                    else
                        pd.DefaultValue = 0;
                }
            }
            pd.Name = nv;
        }

        /// <summary>
        /// プロパティ表示名を外部から設定するための属性。
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class PropertyDisplayNameAttribute : Attribute
        {
            private string myPropertyDisplayName;

            public PropertyDisplayNameAttribute(string name)
            {
                myPropertyDisplayName = name;
            }

            public string PropertyDisplayName
            {
                get { return myPropertyDisplayName; }
            }
        }

        /// <summary>
        /// プロパティ表示名でPropertyDisplayPropertyDescriptorクラスを使用するために
        /// TypeConverter属性に指定するためのTypeConverter派生クラス。
        /// </summary>
        public class PropertyDisplayConverter : TypeConverter
        {
            public PropertyDisplayConverter()
            {
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object instance, Attribute[] filters)
            {
                PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);

                PropertyDescriptorCollection properies = TypeDescriptor.GetProperties(instance, filters, true);
                foreach (PropertyDescriptor desc in properies)
                {
                    collection.Add(new PropertyDisplayPropertyDescriptor(desc));
                }

                return collection;
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        /// <summary>
        /// プロパティの説明（＝情報）を提供するクラス。DisplayNameをカスタマイズする。
        /// </summary>
        public class PropertyDisplayPropertyDescriptor : PropertyDescriptor
        {
            private PropertyDescriptor oneProperty;

            public PropertyDisplayPropertyDescriptor(PropertyDescriptor desc)
                : base(desc)
            {
                oneProperty = desc;
            }

            public override bool CanResetValue(object component)
            {
                return oneProperty.CanResetValue(component);
            }

            public override Type ComponentType
            {
                get
                {
                    return oneProperty.ComponentType;
                }
            }

            public override object GetValue(object component)
            {
                return oneProperty.GetValue(component);
            }

            public override string Description
            {
                get
                {
                    return oneProperty.Description;
                }
            }

            public override string Category
            {
                get
                {
                    return oneProperty.Category;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return oneProperty.IsReadOnly;
                }
            }

            public override void ResetValue(object component)
            {
                oneProperty.ResetValue(component);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return oneProperty.ShouldSerializeValue(component);
            }

            public override void SetValue(object component, object value)
            {
                oneProperty.SetValue(component, value);
            }

            public override Type PropertyType
            {
                get
                {
                    return oneProperty.PropertyType;
                }
            }

            public override string DisplayName
            {
                get
                {
                    PropertyDisplayNameAttribute attrib =
                        (PropertyDisplayNameAttribute)oneProperty.Attributes[typeof(PropertyDisplayNameAttribute)];
                    if (attrib != null)
                    {
                        return attrib.PropertyDisplayName;
                    }

                    return oneProperty.DisplayName;
                }
            }

        }

        /// <summary>
        /// ファイル名のコンバータ。
        /// </summary>
        public class FileNameConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                //あとからやる
                return new StandardValuesCollection(new[]{
                    "a","b","c.txt"});
            }
        }

        /// <summary>
        /// Enum項目名の書き換えに使うクラス
        /// </summary>
        [AttributeUsage(AttributeTargets.All)]
        public class EnumDisplayNameAttribute : Attribute
        {
            public EnumDisplayNameAttribute(string name)
            {
                this.name = name;
            }
            public string Name
            {
                get { return GetName(CultureInfo.CurrentCulture); }
            }
            public virtual string GetName(CultureInfo culture)
            {
                return this.name;
            }
            private string name;
        }
        /// <summary>
        /// Enum項目名の書き換えに使うクラス　その２
        /// </summary>
        public class EnumDisplayNameConverter : EnumConverter
        {
            public EnumDisplayNameConverter(Type type)
                : base(type)
            {
            }
            public override object ConvertFrom(ITypeDescriptorContext context,
                                               CultureInfo culture,
                                               object valueToConvert)
            {
                string value = valueToConvert as string;
                if (value != null)
                {
                    foreach (FieldInfo field in base.EnumType.GetFields())
                    {
                        string name = this.GetDisplayName(field, culture);
                        if (name == value)
                            return field.GetValue(null);
                    }
                }
                return base.ConvertFrom(context, culture, valueToConvert);
            }
            public override object ConvertTo(ITypeDescriptorContext context,
                                             CultureInfo culture,
                                             object value,
                                             Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    string valueName = Enum.GetName(base.EnumType, value);
                    if (valueName != null)
                    {
                        FieldInfo field = base.EnumType.GetField(valueName);
                        string name = this.GetDisplayName(field, culture);
                        if (name != null)
                            return name;
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            private string GetDisplayName(FieldInfo field, CultureInfo culture)
            {
                if (field == null)
                    return null;
                Type type = typeof(EnumDisplayNameAttribute);
                Attribute attr = Attribute.GetCustomAttribute(field, type);
                EnumDisplayNameAttribute disp = attr as EnumDisplayNameAttribute;
                return (disp == null) ? null : disp.GetName(culture);
            }
        }
    
    }
}

