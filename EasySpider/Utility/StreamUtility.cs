using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySpider.Utility
{
    public static class StreamUtility
    {
        /// <summary>
        /// 将一个内存流保存为本地文件（保存完毕以后不会释放内存流，需要手动释放）
        /// </summary>
        /// <param name="memoryStream">内存流实例</param>
        /// <param name="filePath">完整的文件路径</param>
        /// <returns>保存成功返回true，否则返回false</returns>
        public static bool SaveMemoryStream(MemoryStream memoryStream, string filePath)
        {
            if (!memoryStream.CanRead || File.Exists(filePath))
            {
                return false;
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            //如果文件所属目录不存在，则先创建目录
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                file.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            return true;
        }

        /// <summary>
        /// 判读两个流是否完全相等，比较完毕以后，如果两个流均支持CanSeek，则会将他们的位置设置到流的起始处
        /// </summary>
        /// <param name="left">第一个流</param>
        /// <param name="right">第二个流</param>
        /// <param name="close">比较完毕以后是否关闭两个流</param> 
        /// <returns>如果两个流中的完全相等，返回true，否则返回false</returns>
        public static bool IsEqual(Stream left, Stream right, bool close)
        {
            int leftByte = 1, rightByte = 1;
            bool equal = false;
            try
            {
                //两个流必须都可读，否则没办法比较
                if (left.CanRead && right.CanRead)
                {
                    //两个流必须都支持查找，不然不能确定是从起始处开始比较
                    if (left.CanSeek && right.CanSeek)
                    {
                        //如果两个流的长度相等
                        if (left.Length == right.Length)
                        {
                            left.Seek(0, SeekOrigin.Begin);
                            right.Seek(0, SeekOrigin.Begin);
                            while (leftByte != -1 && rightByte != -1)
                            {
                                //如果当前字节不相等，则返回false
                                if (leftByte != rightByte)
                                {
                                    return equal;
                                }
                                leftByte = left.ReadByte();
                                rightByte = right.ReadByte();
                            }
                            equal = true;
                        }
                    }
                }
                return equal;
            }
            finally
            {
                //将流的位置设置到起始处
                if (left.CanSeek && right.CanSeek)
                {
                    left.Seek(0, SeekOrigin.Begin);
                    right.Seek(0, SeekOrigin.Begin);
                }
                if (close)
                {
                    left.Close();
                    right.Close();
                }

            }
        }

        /// <summary>
        ///将一个内存流复制到一个新的内存流
        /// </summary>
        /// <param name="memory">一个内存流</param>
        /// <returns>返回一个新的内存流实例，不要忘记释放</returns>
        public static MemoryStream CloneMemory(MemoryStream memory)
        {
            byte[] buffer = new byte[memory.Length];
            memory.Seek(0, SeekOrigin.Begin);
            memory.Read(buffer, 0, buffer.Length);
            memory.Position = 0;
            return new MemoryStream(buffer);
        }
    }
}
