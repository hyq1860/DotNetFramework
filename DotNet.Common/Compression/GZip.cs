using System;
using System.IO;
using System.IO.Compression;

namespace DotNet.Common.Compression 
{
    /// <summary>
    /// using GZip does compression
    /// </summary>
    public static class GZip 
    {
        #region Static Functions
        /// <summary>
        /// Compresses byte data
        /// </summary>
        /// <param name="bytes">Byte array to be compressed</param>
        /// <returns>A byte array of compressed data</returns>
        public static byte[] Compress(byte[] bytes) 
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Compress, true)) 
                {
                    zipStream.Write(bytes, 0, bytes.Length);
                    zipStream.Close();
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Decompresses byte data
        /// </summary>
        /// <param name="bytes">Byte array to be decompressed</param>
        /// <returns>A byte array of decompressed data</returns>
        public static byte[] Decompress(byte[] bytes) 
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            using (MemoryStream stream = new MemoryStream()) 
            {
                using (GZipStream zipStream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress, true)) 
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
