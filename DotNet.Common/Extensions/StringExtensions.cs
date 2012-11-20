using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Common
{
    /// <summary>
    /// string扩展方法
    /// </summary>
    public static class StringExtensions 
    {
        /// <summary>
        /// 将指定的值转换为有符号整数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static int ToInt32(this string str,int defaultValue)
        {
 	        if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 || !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defaultValue;
 	  
 	        int rv;
 	        if (Int32.TryParse(str, out rv))
 	            return rv;
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iValue"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static string TransCoding(this int iValue, eTrans eType) 
        {
            return Convert.ToString(iValue, (int)eType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selfChar"></param>
        /// <param name="encoding"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static string GetCorrectCoding(this string selfChar, Encoding encoding, eTrans eType) 
        {
            int iUnicode = (int)char.Parse(selfChar);
            return iUnicode.TransCoding(eType);
        }

        /// <summary>
        /// 字符串包含
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsContains(this string self,string value)
        {
            if(value==null)
            {
                return false;
            }
            if(value.Length==0)
            {
                return self.Length == value.Length;
            }
            return self.Contains(value);
        }


        /// <summary>
        /// 字符串为null或者空格
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string s) 
        {
            if (s == null)
                return true;
            return (s.Trim() == string.Empty);
        }

        /// <summary>
        /// Repeats the specified string n times.
        /// </summary>
        /// <param name="instr">The input string.</param>
        /// <param name="n">The number of times input string 
        /// should be repeated.</param>
        /// <returns></returns>
        public static string Repeat(this string instr, int n) 
        {
            //根据重复的次数长短来选择内部实现
            //string与stringbuilder的性能差距阀值点来确定n，此处暂定n为100
            if (string.IsNullOrEmpty(instr))
                return instr;
            if(n<=100)
            {
                string result = string.Empty;
                for (var i = 0; i < n; i++)
                {
                    result += instr;
                }
                return result;
            }
            else
            {
                var result = new StringBuilder(instr.Length * n);
                return result.Insert(0, instr, n).ToString();
            }
        }

        /// <summary>
        /// Returns a copy of this string with the characters in reverse order.
        /// 字符串翻转
        /// </summary>
        /// <param name="s">The string to reverse</param>
        /// <returns>A reversed copy of the original string</returns>
        public static string Reverse(this string s) 
        {
            StringBuilder builder = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                builder.Insert(0, c);
            }
            return builder.ToString();
        }


        /*
         string s = " This is a test of the Emergency Broadcast System. ";
        string token;
        int pos = 0;
        // Parse each word in the string
        while (s.ParseToken(" \t\r\n", ref pos, out token))
	        Console.WriteLine(token);
        // Wait for Enter key
        Console.ReadLine();
         */
        /// <summary>
        /// Parses a token with the specified delimiters.
        /// </summary>
        /// <param name="s">The string to parse</param>
        /// <param name="delimiters">Characters to use as delimiters</param>
        /// <param name="pos">Current string position</param>
        /// <param name="result">Returns any parsed token</param>
        /// <returns>True if a token was found; otherwise, false</returns>
        public static bool ParseToken(this string s, string delimiters, ref int pos, out string result) 
        {
            // Find token start
            while (pos < s.Length && delimiters.IndexOf(s[pos]) >= 0)
                pos++;
            int start = pos;
            // Find token end
            while (pos < s.Length && delimiters.IndexOf(s[pos]) < 0)
                pos++;
            // Extract token
            result = s.Substring(start, pos - start);
            // Return value that indicates if token was found
            return result.Length > 0;
        }

        /// <summary>
        /// Suitable for password hashing
        /// </summary>
        /// <param name="salt">A random salt will be generated if input is null</param>
        /// <returns>Lenght 64</returns>
        public static string ComputeHash(this string str, ref string salt) 
        {
            //if you don't care about the hash being reverted
            //SHA1Managed is faster with a smaller output
            var hasher = new SHA256Managed();
            int keyLength = 4;

            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] key = new byte[keyLength];
            byte[] dataReady = new byte[data.Length + keyLength];

            if (salt == null) 
            {
                //random salt
                var random = new RNGCryptoServiceProvider();
                random.GetNonZeroBytes(key);
                salt = Convert.ToBase64String(key);
            }
            //memory consuming operation
            Array.Copy(Encoding.UTF8.GetBytes(salt), key, keyLength);
            Array.Copy(data, dataReady, data.Length);
            Array.Copy(key, 0, dataReady, data.Length, keyLength);

            //hash
            return BitConverter.ToString(hasher.ComputeHash(dataReady)).Replace("-", string.Empty);
        }

        /// <summary>
        /// Encrypt using Rijndael
        /// </summary>
        public static string Encrypt(this string str, string password) 
        {
            byte[] salt = Encoding.UTF8.GetBytes(password.Length.ToString());
            byte[] text = Encoding.UTF8.GetBytes(str);
            byte[] cipher;

            var key = new PasswordDeriveBytes(password, salt);

            using (ICryptoTransform encryptor = new RijndaelManaged().CreateEncryptor(key.GetBytes(32), key.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(text, 0, text.Length);
                        cryptoStream.FlushFinalBlock();
                        cipher = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(cipher);
        }

        /// <summary>
        /// Decrypt Rijndael encrypted string
        /// </summary>
        public static string Decrypt(this string str, string password) 
        {
            byte[] salt = Encoding.UTF8.GetBytes(password.Length.ToString());
            byte[] text = Convert.FromBase64String(str);
            var key = new PasswordDeriveBytes(password, salt);
            int outLen;

            using (ICryptoTransform decryptor = new RijndaelManaged().CreateDecryptor(key.GetBytes(32), key.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(text))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        text = new byte[text.Length];
                        outLen = cryptoStream.Read(text, 0, text.Length);
                    }
                }
            }
            return Encoding.UTF8.GetString(text, 0, outLen);
        }
    }
}
