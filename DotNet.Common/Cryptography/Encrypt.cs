using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace DotNet.Common
{
    /*
    1.MD5 : EncryptOneWay<System.Security.Cryptography.MD5CryptoServiceProvider, System.Text.UTF8Encoding>();
    2. sha1:EncryptOneWay<System.Security.Cryptography.SHA1CryptoServiceProvider>();
    3.SHA256:EncryptOneWay<System.Security.Cryptography.SHA256Cng>();
     */
    public class CryptographyHelper
    {
        /// <summary>
        /// 不可逆加密
        /// </summary>
        /// <typeparam name="Algorithm">加密HASH算法</typeparam>
        /// <typeparam name="StringEncoding">字符编码</typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptOneWay<Algorithm, StringEncoding>(string str)where Algorithm : HashAlgorithm where StringEncoding : Encoding 
        {
            Encoding enco = Activator.CreateInstance<StringEncoding>();
            byte[] inputBye = enco.GetBytes(str);
            byte[] bytes = Activator.CreateInstance<Algorithm>().ComputeHash(inputBye);
            return BitConverter.ToString(bytes).Replace("-", ""); ;
        }
        /// <summary>
        /// 不可逆加密
        /// </summary>
        /// <typeparam name="Algorithm">加密HASH算法</typeparam>
        /// <param name="str">字符编码</param>
        /// <returns></returns>
        public static string EncryptOneWay<Algorithm>(string str) where Algorithm : HashAlgorithm 
        {
            return EncryptOneWay<Algorithm, System.Text.UTF8Encoding>(str);
        }

        #region aes加解密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainStr, string key, string iv, bool returnNull) 
        {
            string encrypt = AESEncrypt(plainStr, key, iv);
            return returnNull ? encrypt : (encrypt ?? string.Empty);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">向量</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainStr, string key, string iv) 
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);//对称算法的密钥
            byte[] rgbIv = Encoding.UTF8.GetBytes(iv);//对称算法的初始化向量
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);
            string encrypt = null;
            using (Rijndael aes = Rijndael.Create()) 
            {
                aes.Mode = CipherMode.CBC;//
                aes.KeySize = 256;//
                using (MemoryStream mStream = new MemoryStream()) 
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write)) 
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
                aes.Clear();//释放资源
            }
            return encrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr, string key, string iv, bool returnNull) 
        {
            string decrypt = AESDecrypt(encryptStr, key, iv, returnNull);
            return returnNull ? decrypt : (decrypt ?? string.Empty);
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr, string key, string iv) 
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIv = Encoding.UTF8.GetBytes(iv);
            byte[] byteArray = Convert.FromBase64String(encryptStr);
            string decrypt = null;
            using (Rijndael aes = Rijndael.Create()) 
            {
                using (MemoryStream mStream = new MemoryStream()) 
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write)) {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
                aes.Clear();
            }
            return decrypt;
        }
        #endregion

        #region Base64加解密
        /// <summary>
        /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
        /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
        /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
        /// 系統中而不同。
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string str) 
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string str) 
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(decbuff);
        } 
        #endregion

        #region md5(Message Digest Algorithm 5:消息摘要算法第五版)
        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5_32(string input) 
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++) 
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_16(string input) 
        {
            return MD5_32(input).Substring(8, 16);
        }
        /// <summary>
        /// 获得8位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_8(string input) 
        {
            return MD5_32(input).Substring(8, 8);
        }
        /// <summary>
        /// 获得4位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_4(string input) 
        {
            return MD5_32(input).Substring(8, 4);
        }

        public static string MD5EncryptHash(string input) 
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            //copy to a char array which can be passed to a String constructor
            Array.Copy(res, temp, res.Length);
            //return the result as a string
            return new String(temp);
        }
        #endregion

        #region SHA(Secure Hash Algorithm:安全散列算法)
        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
        public static string SHA256(string str) 
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(sha256Data);
            return Convert.ToBase64String(result);  //返回长度为44字节的字符串
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText">要加密的字符</param>
        /// <param name="passPhrase">密码</param>
        /// <param name="saltValue">密钥 salt</param>
        /// <param name="hashAlgorithm">hash算法</param>
        /// <param name="passwordIterations"></param>
        /// <param name="initVector">初始化字符范围</param>
        /// <param name="keySize">128、192 或 256 位的密钥长度</param>
        /// <returns></returns>
        public static string Base64AESDecrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize) 
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(cipherText);//参数s的长度小于 4 或不是 4 的偶数倍时，将会抛出FormatException。
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
            ICryptoTransform transform = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class Base62 
    {
        private static string base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public static String Encoding(ulong num) 
        {
            if (num < 1)
                throw new Exception("num must be greater than 0.");

            StringBuilder sb = new StringBuilder();
            for (; num > 0; num /= 62) 
            {
                sb.Append(base62[(int)(num % 62)]);
            }
            return sb.ToString();
        }

        public static ulong Decoding(String str) 
        {
            str = str.Trim();
            if (str.Length < 1)
                throw new Exception("str must not be empty.");

            ulong result = 0;
            for (int i = 0; i < str.Length; i++) 
            {
                result += (ulong)(base62.IndexOf(str[i]) *  System.Math.Pow(62, i));
            }
            return result;
        }

        /// <summary>
        /// OTP(One-Time-Pad,一次一密） 创建 salt 值
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GenerateSalt(int size)
        {
            //创建强随机数
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            crypto.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// OTP(One-Time-Pad,一次一密）
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GeneratePwdHash(string pwd, string salt)//加密
        {
            string saltpwd = string.Concat(pwd, salt);//用明文与 salt 值组合
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(saltpwd, "SHA1");
            return password;
        }
    }
}
