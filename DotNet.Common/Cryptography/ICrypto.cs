using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace DotNet.Common.Cryptography
{

    public interface ICrypto
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedBase64ConnectString">base64编码的加密过的字符串</param>
        /// <returns></returns>
        string Decrypt(string encryptedBase64ConnectString);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainConnectString">明文</param>
        /// <returns></returns>
        string Encrypt(string plainConnectString);
    }
}

