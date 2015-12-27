using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Snippets
{
    public static partial class Security
    {
        public static string CalcuateMD5(string src,string key)
        {
            //文字列をバイト型配列に変換する
            byte[] data = System.Text.Encoding.ASCII.GetBytes(src);
            byte[] keyData = System.Text.Encoding.ASCII.GetBytes(key);

            //HMACMD5オブジェクトの作成
            System.Security.Cryptography.HMACMD5 hmacmd5 =
                new System.Security.Cryptography.HMACMD5(keyData);
            //ハッシュ値を計算
            byte[] bs = hmacmd5.ComputeHash(data);

            //byte型配列を16進数の文字列に変換
            string result = BitConverter.ToString(bs).ToLower().Replace("-", "");
            return result;
        }
    }
}
