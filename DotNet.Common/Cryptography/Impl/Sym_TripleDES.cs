using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace DotNet.Common.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Sym_TripleDES : ICrypto
    {
        private static byte[] desIV = new byte[8];
        private static byte[] desKey = new byte[] { 
            0x61, 0x62, 0x63, 100, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 110, 0x6f, 0x70, 
            0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 120
         };

        public string Decrypt(string encryptedBase64ConnectString)
        {
            MemoryStream stream = new MemoryStream(200);
            stream.SetLength(0L);
            byte[] buffer = Convert.FromBase64String(encryptedBase64ConnectString);
            TripleDES edes = new TripleDESCryptoServiceProvider();
            edes.KeySize = 0xc0;
            CryptoStream stream2 = new CryptoStream(stream, edes.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Flush();
            stream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer2 = new byte[stream.Length];
            stream.Read(buffer2, 0, buffer2.Length);
            stream2.Close();
            stream.Close();
            return Encoding.Unicode.GetString(buffer2);
        }

        public string Encrypt(string plainConnectString)
        {
            MemoryStream stream = new MemoryStream(200);
            stream.SetLength(0L);
            byte[] bytes = Encoding.Unicode.GetBytes(plainConnectString);
            TripleDES edes = new TripleDESCryptoServiceProvider();
            CryptoStream stream2 = new CryptoStream(stream, edes.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream.Flush();
            stream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream2.Close();
            stream.Close();
            return Convert.ToBase64String(buffer, 0, buffer.Length);
        }
    }
}

