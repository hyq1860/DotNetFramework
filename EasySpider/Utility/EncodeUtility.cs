using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySpider.Utility
{
    /// <summary>
    /// 封装编码相关的一些公用方法
    /// </summary>
    public static class EncodeUtility
    {
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
    }
}
