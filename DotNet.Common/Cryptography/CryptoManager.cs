using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace DotNet.Common.Cryptography
{
    public sealed class CryptoManager
    {
        private CryptoManager( )
        {
        }

        public static ICrypto GetCrypto( CryptoAlgorithm algorithm )
        {
            ICrypto crypto = null;
            switch ( algorithm )
            {
                case CryptoAlgorithm.DES:
                    crypto = new Sym_DES( );
                    break;

                case CryptoAlgorithm.RC2:
                    crypto = new Sym_RC2( );
                    break;

                case CryptoAlgorithm.Rijndael:
                    crypto = new Sym_Rijndael( );
                    break;

                case CryptoAlgorithm.TripleDES:
                    crypto = new Sym_TripleDES( );
                    break;

                case CryptoAlgorithm.RSA:
                    crypto = new Asym_RSA( );
                    break;

                default:
                    Debug.Assert( false );
                    break;
            }
            Debug.Assert( crypto != null );
            return crypto;
        }
    }
}

