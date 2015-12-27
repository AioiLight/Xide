using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace Apocalypse.Data
{
    /// <summary>
    /// マップチップデータのコンテナ
    /// </summary>
    [XmlRoot("chipsdef")]
    public class ChipDataContainer
    {
        /// <summary>
        /// マップチップデータXMLをデシリアライズ
        /// </summary>
        public static ChipDataContainer Deserialize(string path)
        {
            return K.Snippets.Files.LoadXML<ChipDataContainer>(path, true);
        }

        /// <summary>
        /// ExtraDraw用画像パス
        /// </summary>
        [XmlElement("xdpictpath")]
        public string ExtraDrawSrc = "[ExtraDraw画像ファイルのパスを記述]";

        /// <summary>
        /// チップのデフォルト大きさ
        /// </summary>
        [XmlIgnore]
        public Size ChipDefaultSize { get; set; }
        [XmlElement("chipdefsize")] //for serialization support
        public string _chipDefSize
        {
            get { return ChipDefaultSize.Width + "," + ChipDefaultSize.Height; }
            set
            {
                if (value == null)
                {
                    ChipDefaultSize = new Size(0, 0);
                    return;
                }
                int v1, v2;
                value.GetTwoCSVInt(out v1, out v2);
                ChipDefaultSize = new Size(v1, v2);
            }
        }

        /// <summary>
        /// LinkStrのバイト数
        /// </summary>
        [XmlElement("linkstrbyte")]
        public int ByteSize { get; set; }

        /// <summary>
        /// チップデータ実体への配列
        /// </summary>
        [XmlArray("chips"), XmlArrayItem("chip")]
        public ChipData[] ChipDatas { get; set; }
    }

    public class ChipData
    {
        /// <summary>
        /// リンク文字(マップチップで実際に出力される文字)
        /// </summary>
        [XmlAttribute("str")]
        public string LinkStr { get; set; }

        /// <summary>
        /// 関連設定項目(プロパティ名で指定)
        /// </summary>
        [XmlAttribute("rel")]
        public string RelatedCog { get; set; }

        /// <summary>
        /// 設定実体配列
        /// </summary>
        [XmlArray("datas"), XmlArrayItem("data")]
        public ChipDataContent[] Contents { get; set; }

        /// <summary>
        /// オーバービューで塗りつぶす色
        /// </summary>
        [XmlIgnore()]
        public Color Color = Color.Black;
        [XmlAttribute("clr")] //for serialization support
        public string _colorConv
        {
            get { return ColorTranslator.ToHtml(this.Color); }
            set
            {
                if (value == null)
                    this.Color = Color.Black;
                else if (value.StartsWith("#"))
                    this.Color = ColorTranslator.FromHtml(value);
                else
                    this.Color = Color.FromName(value);
            }
        }

        public class ChipDataContent
        {
            /// <summary>
            /// 関連設定項目の設定値(これによって判別される、ない場合は無視)
            /// </summary>
            [XmlAttribute("relval")]
            public string RelValue { get; set; }

            /// <summary>
            /// チップ名前
            /// </summary>
            [XmlElement("name")]
            public string Name { get; set; }

            /// <summary>
            /// チップの解説
            /// </summary>
            [XmlElement("desc")]
            public string Description = null;

            /// <summary>
            /// チップ画像からクリップする領域
            /// </summary>
            [XmlIgnore()]
            public Rectangle DrawClipRect = new Rectangle();
            [XmlElement("loc")] //for serialization support
            public string _drawClipPos
            {
                get { return DrawClipRect.X + "," + DrawClipRect.Y; }
                set
                {
                    if (value == null)
                    {
                        DrawClipRect.X = 0;
                        DrawClipRect.Y = 0;
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    DrawClipRect.X = v1;
                    DrawClipRect.Y = v2;
                }
            }
            [XmlElement("size")] //for serialization support
            public string _drawClipSize
            {
                get { return DrawClipRect.Width + "," + DrawClipRect.Height; }
                set
                {
                    if (value == null)
                    {
                        DrawClipRect.Width = 0;
                        DrawClipRect.Height = 0;
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    DrawClipRect.Width = v1;
                    DrawClipRect.Height = v2;
                }
            }

            /// <summary>
            /// サイズが変化する場合の中心チップの左上位置
            /// </summary>
            [XmlIgnore()]
            public Point CenterPoint = new Point();
            [XmlElement("center")] //for serialization support
            public string _centerPoint
            {
                get { return CenterPoint.X + "," + CenterPoint.Y; }
                set
                {
                    if (value == null)
                    {
                        CenterPoint = new Point(0, 0);
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    CenterPoint = new Point(v1, v2);
                }
            }

            /// <summary>
            /// 拡張描画データ
            /// </summary>
            [XmlElement("extra")]
            public ExtraDrawData ExtraDraw = new ExtraDrawData();
            public class ExtraDrawData
            {
                /// <summary>
                /// 拡張描画を実行するか
                /// </summary>
                bool Enabled { get { return DrawPos.X >= 0 && DrawPos.Y >= 0; } }

                /// <summary>
                /// チップよりも後ろ側に描画するか
                /// </summary>
                [XmlAttribute("backdraw")]
                public bool BackgroundDraw = false;

                /// <summary>
                /// クリップの位置
                /// </summary>
                /// <remarks>
                /// ExtraDrawの仕様として、必ずデフォルトの大きさでクリッピングされ、
                /// 必ずデフォルト中心位置を左上にして描画されます。
                /// </remarks>
                [XmlIgnore()]
                public Point DrawPos = new Point(-1, -1);
                [XmlText()] //for serialization support
                public string _drawPos
                {
                    get { return DrawPos.X + "," + DrawPos.Y; }
                    set
                    {
                        if (value == null)
                        {
                            DrawPos = new Point(-1, -1);
                            return;
                        }
                        int v1, v2;
                        value.GetTwoCSVInt(out v1, out v2);
                        DrawPos = new Point(v1, v2);
                    }
                }
            }
        }
    }

    public static class StringExtender
    {
        public static void GetTwoCSVInt(this string csv, out int val1, out int val2)
        {
            var c = csv.Split(',');
            if (c.Length != 2)
                throw new Exception("Invalid num format in " + csv);
            if (!int.TryParse(c[0], out val1))
                throw new Exception("Numeric parse error(1) in " + csv);
            if (!int.TryParse(c[1], out val2))
                throw new Exception("Numeric parse error(2) in " + csv);
        }
    }
}
