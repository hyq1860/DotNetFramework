using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNet.Common
{
    public static class StreamExtensions 
    {
        /*
            using(var stream = response.GetResponseStream())
            using(var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                // Do something with copied data
            }
         */
        public static void CopyTo(this Stream fromStream, Stream toStream) 
        {
            InnerCopyTo(fromStream, toStream, 8092);
        }

        public static void CopyTo(this Stream fromStream, Stream toStream,int length) 
        {
            InnerCopyTo(fromStream, toStream, length);
        }

        /// <summary>
        /// </summary>
        /// <param name="fromStream">
        /// The from stream.
        /// </param>
        /// <param name="toStream">
        /// The to stream.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static void InnerCopyTo(Stream fromStream, Stream toStream,int length) 
        {
            if (fromStream == null)
                throw new ArgumentNullException("fromStream");
            if (toStream == null)
                throw new ArgumentNullException("toStream");
            if(length<=0)
                throw new ArgumentException("length必须大于0");
            var bytes = new byte[length];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0) 
            {
                toStream.Write(bytes, 0, dataRead);
            }
        }

        /// <summary>
        /// 获取文本文件的编码格式
        /// </summary>
        /// <param name="filePath">文件的完整路径</param>
        /// <returns>返回得到的文本编码，如果获取失败，则返回null</returns>
        public static Encoding GetTextCode(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return GetTextCode(fileStream);
            }
        }

        /// <summary>
        /// 获取文本流的编码格式
        /// </summary>
        /// <param name="stream">一个流</param>
        /// <returns>返回得到的文本编码，如果获取失败，则返回null</returns>
        public static Encoding GetTextCode(Stream stream)
        {
            byte[] bytes;
            //读取文件的前三个字节
            BinaryReader reader = new BinaryReader(stream);
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            bytes = reader.ReadBytes(3);

            //编码类型 Coding=编码类型.ASCII;   
            if (bytes[0] >= 0xEF)
            {
                if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                {
                    return Encoding.UTF8;
                }
                else if (bytes[0] == 0xFE && bytes[1] == 0xFF)
                {
                    return Encoding.BigEndianUnicode;
                }
                else if (bytes[0] == 0xFF && bytes[1] == 0xFE)
                {
                    return Encoding.Unicode;
                }
                else
                {
                    return Encoding.Default;
                }
            }
            else
            {
                return null;
            }
        }


        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] ToBytes(this Stream stream) 
        { 
            byte[] bytes = new byte[stream.Length]; 
            stream.Read(bytes, 0, bytes.Length); 
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin); 
            return bytes; 
        } 
        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream ToStream(this byte[] bytes) 
        { 
            Stream stream = new MemoryStream(bytes); 
            return stream; 
        } 

        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public static void ToFile(this Stream stream,string fileName) 
        { 
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length]; 
            stream.Read(bytes, 0, bytes.Length); 
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin); 
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create); 
            BinaryWriter bw = new BinaryWriter(fs); 
            bw.Write(bytes); 
            bw.Close(); 
            fs.Close(); 
        } 

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public static Stream FileToStream(string fileName) 
        { 
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read); 
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length]; 
            fileStream.Read(bytes, 0, bytes.Length); 
            fileStream.Close(); 
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes); 
            return stream; 
        }
    }
}
