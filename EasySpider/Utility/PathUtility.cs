using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace EasySpider.Utility
{
    public static class PathUtility
    {
        /// <summary>
        /// 以一个绝对url为参考，将另一个地址也转换为绝对地址
        /// </summary>
        /// <param name="absoluteUrl">页面url地址，一个绝对地址</param>
        /// <param name="href">需要被转换的地址，此地址可能已经是绝对地址也可能是相对地址等等</param>
        /// <returns>返回一个转换以后的绝对地址</returns>
        public static string ConvertToAbsoluteHref(string absoluteUrl, string href)
        {
            //注意，在此方法中，不能将最终生成的href改变原始大小写，因为淘宝上图片img的src路径是经过加密的，如果字母大小写改变则不能正常显示图片
            href = href.Trim();
            //如果实在href里面调用JS，则返回当前JS调用
            if (href.ToLower().StartsWith("javascript"))
            {
                return href;
            }
            //如果以./开头，则表示以currentUrl作为参考url
            if (href.StartsWith("./"))
            {
                href = href.Substring(2);
            }
            string scheme = "";
            if (href.Length > 5)
            {
                scheme = href.Substring(0, 6).ToLower();
            }
            //如果href以http开头，则说明href已经是一个绝对地址
            if (scheme.StartsWith("http:") || href.StartsWith("https:"))
            {
                return href;
            }

            Uri uri = new Uri(absoluteUrl);
            //获取类似http://a.com:789
            string start = uri.Scheme + "://" + uri.Authority;
            //如果href使用绝对路径
            if (href.StartsWith("/"))
            {
                return start + href;
            }//如果href使用类似 ../之类的相对路径
            else if (href.StartsWith("../"))
            {
                int count = 0;
                int index = 0;
                while ('.' == href[index] && '.' == href[index + 1] && '/' == href[index + 2])
                {
                    count++;
                    index += 3;
                }

                for (int i = 0; i < uri.Segments.Length - 1 - count; i++)
                {
                    start += uri.Segments[i];
                }
                start += href.Substring(index);
                return start;

            }//如果href和当前路径在同一目录
            else
            {
                //如果currentUrl是类似于http://aa.com或者http://aa.com/aa/之类的路径，则应该将uri.Segments中的最后一个字符串也算进去
                for (int i = 0; i < (uri.Segments[uri.Segments.Length - 1].EndsWith("/") ? uri.Segments.Length : uri.Segments.Length - 1); i++)
                {
                    start += uri.Segments[i];
                }
                start += href;
                return start;
            }
        }

        /// <summary>
        /// 根据一个参考路径和一个要引用的路径，得到一个在参考路径中引用需要被引用的路径的相对路径
        /// </summary>
        /// <param name="basePath">参考文件夹的完整路径(此参数必须是一个文件夹路径)</param>
        /// <param name="referencePath">需要根据相对文件夹路径进行转换的文件夹路径(此参数也可以是一个文件路径，用于获取在目录basePath的某个文件中引用此文件使用的相对路径)</param>       
        /// <returns>返回一个相对路径，此相对路径用于在basePath中引用dirPath</returns>
        public static string GetRelativePath(string basePath, string referencePath)
        {
            StringBuilder sb = new StringBuilder(30);
            StringBuilder sbTemp = new StringBuilder(30);
            basePath = basePath.Trim().TrimEnd('\\');
            referencePath = referencePath.Trim().TrimEnd(new char['\\']);
            //如果为true，则basePath比dirPath级数要大或者相等，否则dirPath比basePath级数大
            bool flag;
            //如果dirPath不是一个绝对路径，则dirPath中的 \ 替换为 / 然后返回
            if (!Path.IsPathRooted(referencePath))
            {
                return referencePath.Replace('\\', '/');
            }
            //如果参考路径不是一个绝对路径，则返回一个空字符串
            if (!Path.IsPathRooted(basePath))
            {
                return "";
            }
            //如果两个路径不是在同一个盘，则返回一个空字符串
            if (Path.GetPathRoot(basePath)[0] != Path.GetPathRoot(referencePath)[0])
            {
                return "";
            }

            //如果两个文件夹路径相等，则返回一个空字符串
            if (string.Compare(basePath, referencePath, true) == 0)
            {
                return "";
            }
            int baseLevel = GetFolderLevel(basePath);
            int dirLevel = GetFolderLevel(referencePath);
            flag = baseLevel >= dirLevel;
            while (baseLevel != dirLevel)
            {
                //如果参考路径级别比转换路径级别小
                if (baseLevel < dirLevel)
                {
                    sb.Insert(0, "/" + referencePath.Substring(referencePath.LastIndexOf('\\') + 1));
                    referencePath = Path.GetDirectoryName(referencePath);
                    dirLevel = GetFolderLevel(referencePath);
                }
                else
                {
                    sb.Append("../");
                    basePath = Path.GetDirectoryName(basePath);
                    baseLevel = GetFolderLevel(basePath);

                }
            }
            //如果当两个目录级别相同时，且父目录相同，则在此处返回
            if (string.Compare(basePath, referencePath, true) == 0)
            {
                if (sb.Length > 0 && sb[0] == '/')
                    sb.Remove(0, 1);
                return sb.ToString();
            }

            if (sb.Length > 0 && sb[0] == '/')
            {
                sb.Remove(0, 1);
            }
            //运行到此处，两个目录级别相同，但是父目录不同
            while (0 != string.Compare(basePath, referencePath, true))
            {
                sbTemp.Insert(0, "/" + referencePath.Substring(referencePath.LastIndexOf('\\') + 1));
                basePath = Path.GetDirectoryName(basePath);
                referencePath = Path.GetDirectoryName(referencePath);
                sb.Insert(0, "../");
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == '/')
            {
                sb.Length = sb.Length - 1;
            }

            if (flag)
            {
                sb.Append(sbTemp.ToString());
            }
            else
            {
                int index = sb.ToString().LastIndexOf("../");
                if (index == -1)
                {
                    index = 0;
                }
                else
                {
                    index += 2;
                }
                sb.Insert(index, sbTemp.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取一个不与当前路径冲突的路径，也就是说指定路径已存在则返回一个新路径，否则返回指定路径
        /// </summary>
        /// <param name="filePath">文件完整路径</param>        
        /// <returns>返回可以用的文件完全路径</returns>
        public static string GetNoConflictFilePath(string filePath, List<string> conflictList)
        {
            return GetNoConflictFilePath(filePath, 1, conflictList);
        }

        /// <summary>
        /// 获取一个路径所含的文件夹的级数，即路径包含的 \ 的数量
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>返回一个整数</returns>
        private static int GetFolderLevel(string path)
        {
            //如果参考路径不是一个绝对路径，则返回一个空字符串
            if (!Path.IsPathRooted(path))
            {
                return 0;
            }
            int count = 0;
            foreach (var ch in path)
            {
                if (ch == '\\')
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 获取一个文件夹路径的指定级数的目录名,第n级是第n个 \ 后面的文件夹名
        /// </summary>
        /// <param name="path">完整文件夹路劲</param>
        /// <param name="level">指定的级数</param>
        /// <returns>指定级数的文件夹名称</returns>
        private static string GetDirNameByLevel(string path, int level)
        {
            //如果参考路径不是一个绝对路径，则返回一个空字符串
            if (!Path.IsPathRooted(path))
            {
                return "";
            }
            string[] arr = path.Split('\\');
            if (level > arr.Length)
            {
                return "";
            }
            return arr[level];
        }

        /// <summary>
        /// 根据路径判断文件是否已经存在，如果存在则更改文件名称
        /// </summary>
        /// <param name="filePath">文件完整路径</param>        
        /// <param name="count">如果文件已经存在，则在文件后加数字的起始数字</param>
        /// <param name="conflictList">文件路径冲突列表，将与预订路径都冲突的路径均加入此列表。如果conflictList的数量为0，则说明没有路径冲突。</param>
        /// <returns>返回可以用文件完全路径</returns>
        private static string GetNoConflictFilePath(string filePath, int count, List<string> conflictList)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            conflictList.Add(filePath);
            if (File.Exists(filePath))
            {
                //保证不会出现类似reset_1_2_3_4.css之类的文件名，如果已经使用是在递归中第二次调用此方法，去先去除上次加的文件后缀，保证只出现类似reset_4.css之类的文件名
                if (count > 1)
                {
                    fileName = fileName.Substring(0, fileName.LastIndexOf('_'));
                }
                fileName = fileName + "_" + count;
                filePath = Path.GetDirectoryName(filePath) + "\\" + fileName + Path.GetExtension(filePath);
                return GetNoConflictFilePath(filePath, count + 1, conflictList);
            }
            conflictList.RemoveAt(conflictList.Count - 1);
            return filePath;
        }

        /// <summary>
        /// 根据页面url、资源url以及基础目录，获取资源在本地的保存目录
        /// </summary>
        /// <param name="pageUrl">页面的绝对url</param>
        /// <param name="resourceUrl">资源的绝对url</param>
        /// <param name="htmlDir">基础目录，目录计算以此目录为参考值，最终得到的保存目录肯定是基础目录的子孙级目录</param>
        /// <returns>返回本地保存目录的路径，以\结尾</returns>
        public static string GetSaveDir(string pageUrl, string resourceUrl, string htmlDir)
        {
            Match pageMatch = RegexCollection.RegUrl.Match(pageUrl);
            Match resourceMatch = RegexCollection.RegUrl.Match(resourceUrl);
            //函数返回的本地目录路径（如果结尾有\，则去掉）
            string saveDir = htmlDir.TrimEnd('\\');
            //页面url和资源url的父级path
            string pageDir = pageMatch.Groups["path"].Value.Trim(),
                   resourceDir = resourceMatch.Groups["path"].Value.Trim();
            pageDir = pageDir != string.Empty ? Path.GetDirectoryName(pageDir) ?? string.Empty : string.Empty;
            resourceDir = resourceDir != string.Empty ? Path.GetDirectoryName(resourceDir) ?? string.Empty : string.Empty;
            /*
            //将页面url和资源url按照/分段
            List<string> pageUrlSegments = new List<string>(pagePath.Split(','));
            List<string> resourceUrlSegments = new List<string>(resourcePath.Split(','));
            //剔除列表里的空字符串
            for (int i = 0; i < pageUrlSegments.Count; i++)
            {
                if ("" == pageUrlSegments[i])
                {
                    pageUrlSegments.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < resourceUrlSegments.Count; i++)
            {
                if ("" == resourceUrlSegments[i])
                {
                    resourceUrlSegments.RemoveAt(i);
                    i--;
                }
            }
            */
            if (pageMatch.Success && resourceMatch.Success)
            {
                //1.判断页面url和资源url的域名、端口是否一致，如果不一致，则在当前目录下建立一个域名目录
                if (!(0 == string.Compare(pageMatch.Groups["domain"].Value, resourceMatch.Groups["domain"].Value, true) && pageMatch.Groups["port"].Value == resourceMatch.Groups["port"].Value))
                {
                    saveDir += "\\" + EncodePath(resourceMatch.Groups["domain"].Value + (resourceMatch.Groups["port"].Value == "" ? "" : "$" + resourceMatch.Groups["port"].Value));
                    if ("" != resourceDir)
                    {
                        saveDir += "\\" + resourceDir;
                    }
                }
                else
                {
                    //如果资源url是页面url的子孙级url，则在保存目录在基础目录下
                    if (resourceDir.Length >= pageDir.Length && resourceDir.Substring(0, pageDir.Length) == pageDir)
                    {
                        resourceDir = resourceDir.Substring(pageDir.Length);
                        if ("" != resourceDir)
                        {
                            saveDir += "\\" + resourceDir;
                        }
                    }//否则创建一个以资源url的域名、端口问名的目录，将资源保存在此目录下
                    else
                    {
                        saveDir += "\\" + EncodePath(resourceMatch.Groups["domain"].Value + (resourceMatch.Groups["port"].Value == "" ? "" : "$" + resourceMatch.Groups["port"].Value));
                        if ("" != resourceDir)
                        {
                            saveDir += "\\" + resourceDir;
                        }
                    }
                }
                saveDir = Path.GetFullPath(saveDir);
            }
            //如果目录不存在，则创建
            if (Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            return saveDir.TrimEnd('\\') + "\\";
        }

        /// <summary>
        /// 对文件夹名或者文件名进行编码
        /// </summary>
        /// <param name="dirName">文件夹名或者文件名</param>
        /// <returns>返回编码后的名称</returns>
        static string EncodePath(string dirName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(dirName));
            return dirName;
        }

    }
}
