using System;
using System.IO;
using System.IO.Compression;

namespace DotNet.Common.Compression 
{
    /// <summary>
    /// using deflate compressing data
    /// </summary>
    public static class Deflate 
    {
        #region Static Functions

        /// <summary>
        /// Compresses data
        /// </summary>
        /// <param name="bytes">The byte array to be compressed</param>
        /// <returns>A byte array of compressed data</returns>
        public static byte[] Compress(byte[] bytes) 
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            using (MemoryStream stream = new MemoryStream()) 
            {
                using (DeflateStream zipStream = new DeflateStream(stream, CompressionMode.Compress, true)) 
                {
                    zipStream.Write(bytes, 0, bytes.Length);
                    zipStream.Close();
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Decompresses data
        /// </summary>
        /// <param name="bytes">The byte array to be decompressed</param>
        /// <returns>A byte array of uncompressed data</returns>
        public static byte[] Decompress(byte[] bytes) 
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            using (MemoryStream stream = new MemoryStream()) 
            {
                using (DeflateStream zipStream = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress, true)) 
                {
                    byte[] buffer = new byte[4096];
                    while (true) 
                    {
                        int size = zipStream.Read(buffer, 0, buffer.Length);
                        if (size > 0) stream.Write(buffer, 0, size);
                        else break;
                    }
                    zipStream.Close();
                    return stream.ToArray();
                }
            }
        }
        #endregion
    }
}
