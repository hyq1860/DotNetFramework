using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

namespace DotNet.Common.Cryptography
{
    public static class EncrypHelper
    {
        public static string EncrypString(string sourse)
        {
            if (sourse == null)
            {
                return string.Empty;
            }
            ICrypto des =  CryptoManager.GetCrypto( CryptoAlgorithm.DES );
            return des.Encrypt(sourse);
        }

        public static string DecryptString(string sourse)
        {
            if (sourse == null)
            {
                return null;
            }
            ICrypto des =  CryptoManager.GetCrypto( CryptoAlgorithm.DES );
            sourse = sourse.Replace(" ", "+"); //HttpUtility.UrlEncode(sourse);
            return des.Decrypt(sourse);
        }

        public static string GetMD5(string s, string inputCharset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }
    }
}
