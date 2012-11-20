using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace DotNet.Common.Cryptography
{

    public enum CryptoAlgorithm
    {
        DES ,
        RC2 ,
        Rijndael ,
        TripleDES ,
        RSA ,
        DSA ,
        Unknow=1024 ,
    }
}

