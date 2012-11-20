using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace EasySpider.Utility
{
    /// <summary>
    /// 封装有关文件操作的一些公用方法
    /// </summary>
    public class FileUtility
    {
        /// <summary>
        /// 根据一个本地路径已经一个描述文件内容的内存流实例，将文件保存的本地。如果有文件路径冲突则不会覆盖保存，而是对比冲突内容，判断是否需要保存为一个不重名的文件
        /// </summary>
        /// <param name="filePath">文件保存的绝对路径</param>
        /// <param name="memoryStream">一个内存流实例</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string SaveFile(string filePath, MemoryStream memoryStream)
        {
            List<string> conflictList = new List<string>(10);
            //得到一个不会与先用文件冲突的路径，并且将冲突路径都加入conflictList
            filePath = PathUtility.GetNoConflictFilePath(filePath, conflictList);
            //如果存在文件路径冲突
            if (conflictList.Count > 0)
            {
                //遍历文件冲突列表，判断是否有与下载的文件完全相同的
                foreach (string conflictPath in conflictList)
                {
                    using (FileStream fileStream = new FileStream(conflictPath, FileMode.Open, FileAccess.Read))
                    {
                        //对比内存流和文件流，如果相等则不用重复保存文件
                        if (StreamUtility.IsEqual(memoryStream, fileStream, false))
                        {
                            return conflictPath;
                        }
                    }
                }
            }
            //将下载的文件从内存流中保存到本地
            if (StreamUtility.SaveMemoryStream(memoryStream, filePath))
            {
                return filePath;
            }
            return null;
        }

        /// <summary>
        /// 将一段文本保存到本地
        /// </summary>
        /// <param name="filePath">文件本地路径</param>
        /// <param name="text">文本内容</param>
        /// <param name="encode">指定保存编码</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        public static string SaveText(string filePath, string text, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(text);
            using (MemoryStream memoryStream = new MemoryStream(byteArray, 0, byteArray.Length, false, true))
            {
                filePath = SaveFile(filePath, memoryStream);
            }
            return filePath;
        }

        /// <summary>
        /// 改变某个文件的编码类型
        /// </summary>
        /// <param name="path"></param>
        /// <param name="code"></param>
        public static void ChangeTxtEncode(string path, Encoding code)
        {
            string html;
            using (StreamReader reader = new StreamReader(path, EncodeUtility.GetTextCode(path)))
            {
                html = reader.ReadToEnd();
            }

            //转换编码之后，同时要转换Meta里面声明的编码类型
            if (Regex.Match(html, "<\\s*meta\\s*http-equiv\\s*=\\s*['\"]Content-Type['\"]\\s*content\\s*=\\s*['\"]text/html;\\s*charset=(.){5,10}['\"]\\s*/?\\s*>", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                html = Regex.Replace(html, "<\\s*meta\\s*http-equiv\\s*=\\s*['\"]Content-Type['\"]\\s*content\\s*=\\s*['\"]text/html;\\s*charset=(.){5,10}['\"]\\s*/?\\s*>", string.Format("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\"/>", code.WebName), RegexOptions.IgnoreCase | RegexOptions.Compiled);

                html = Regex.Replace(html, "<\\s*meta\\s*content\\s*=\\s*['\"]text/html;\\s*charset=(.){5,10}['\"]\\s*http-equiv\\s*=\\s*['\"]Content-Type['\"]\\s*/?\\s*>", string.Format("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\"/>", code.WebName), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
            {
                html = Regex.Replace(html, "<\\s*/\\s*head\\s*>", string.Format("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\"/>\r\n</head>", code.WebName), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            using (StreamWriter writer = new StreamWriter(path, false, code))
            {
                writer.Write(html);
            }
        }

        /// <summary>
        /// 获取一个8位数的随机文件名
        /// </summary>        
        /// <returns>返回文件名</returns>
        public static string GetRandomFileName()
        {
            return GetRandomFileName(8);
        }

        /// <summary>
        /// 获取一个随机文件名
        /// </summary>
        /// <param name="count">文件名的位数，必须在1-32之间</param>
        /// <returns>返回文件名</returns>
        public static string GetRandomFileName(int count)
        {
            if (count > 32)
            {
                count = 32;
            }
            if (count < 1)
            {
                count = 8;
            }
            return Guid.NewGuid().ToString("N").Substring(0, count);
        }

        /// <summary>
        /// 修改某个文件的title等等
        /// </summary>
        /// <param name="fileName">完全文件名</param>
        /// <param name="title">title</param>
        /// <param name="keywords">keywords</param>
        /// <param name="description">description</param>
        /// <param name="codeName">文件编码</param>
        public static void Update(string fileName, string title, string keywords, string description, Encoding newCode)
        {
            string html = File.ReadAllText(fileName, newCode);
            html = Regex.Replace(html, "<title>((?!</title>).*)</title>", "<title>" + title + "</title>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //替换keywords
            html = Regex.Replace(html, "<\\s*meta\\s*name=\\s*['\"]keywords['\"]\\s*content=\\s*['\"]((?!/>).*)['\"]\\s*/?\\s*>", "<meta name=\"keywords\" content=\"" + keywords + "\"/>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            html = Regex.Replace(html, "<\\s*meta\\s*content=\\s*['\"]((?!/>).*)['\"]\\s*name=\\s*['\"]keywords['\"]\\s*/?\\s*>", "<meta name=\"keywords\" content=\"" + keywords + "\"/>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
