// -----------------------------------------------------------------------
// <copyright file="Helper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DotNet.Web.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #region 正则辅助
    class RegexCollection
    {
        public static RegexOptions RegOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;

        /// <summary>
        /// 判读一个字符串是否url的正则，其中Groups如下：protocol是协议名，domain是url的域名，port是域名对应的端口，path是url的在服务器上代表的绝对路径（例如/aa/bb,或者/aa/bb.cc/dd.html等等），query是GET传值部分（不包括问好），hash是#后面的部分
        /// </summary>
        public static Regex RegUrl = new Regex(@"^(?<protocol>https?)://(?<domain>(?:[0-9a-z-_]+(?<!-)\.){0,}[0-9a-z-_]+(?<!-)\.[0-9a-z-_]+|localhost)(?:\:(?<port>\d+))?(?<!-)(?<path>(?:(?<!/)/[0-9a-z-_.]+/?){0,})(?:\?(?<query>[^#]+))?(?:#(?<hash>[^\r\n]*))?$", RegOptions);
        /*^https?://(?<prefixdomain>(?:[0-9a-z-_]+(?<!-)\.){0,})(?<topdomain>[0-9a-z-_]+(?<!-)\.[0-9a-z-_]+)(?:\:(?<port>\d+))?(?<!-)/?(?<path>(?:(?<!/)/[0-9a-z-_.]+/?){0,})(?:\?(?<query>[^#]+))?(?:#(?<fragment>[^\r\n]*))?$*/

        /// <summary>
        /// 验证form中action的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["action"]为action的值
        /// </summary>66
        public static Regex RegFormAction = new Regex(@"<form[\s\S]+?action=(?<quote>['""]?)(?<action>[^'""\s><]+)\1[\s\S]*?>", RegOptions);

        /// <summary>
        /// 验证href的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["href"]为href的值
        /// </summary>
        public static Regex RegHref = new Regex(@"href=(?<quote>['""]?)(?<href>[^'""\s><]+)\k<quote>", RegOptions);

        /// <summary>
        /// 验证src的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["src"]为src的值
        /// </summary>
        public static Regex RegSrc = new Regex(@"src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>", RegOptions);

        /// <summary>
        /// 从一个域名中获取一级域名
        /// </summary>
        public static Regex RegLevelDomain = new Regex(@"[^\.]+\.[^\.]+$", RegOptions);

        /// <summary>
        /// 获取HTML中Meta标签中的Content-Type值
        /// </summary>
        public static Regex RegMetaContentType = new Regex(@"<\s*meta[^<>]+content=['""]?text/html;charset=([\w-]+)['""]?[^<>]*>", RegOptions);

        /// <summary>
        /// 一个匹配Script标签的正则,Groups[1]是两个script标签之间的内容，它可以匹配如下内容：
        /*
        <script language=""javascript"" type='text/javascript' src='../../1.js'>   
                  function getColor() {
                       alert(123)
                   }          
                  //因为<script>左边有引号，</script>右边有引号，所以匹配
                  document.write('<script></script>');   
                 </script>
         * */
        /// </summary>
        public static Regex RegScript = new Regex(@"<script[^>]*>(((?!(?<!['""])<\/?script>(?!['""]))[\s\S])*)</script>", RegOptions);

        /// <summary>
        /// 验证css引入，如果匹配成功，则Groups["src"]是css文件href的值
        /// </summary>
        public static Regex RegCssLink = new Regex(@"<link[^>]*(?:(?:href=(?<quote1>['""]?)(?<src>[^'""\s><]+)\k<quote1>[^>]*rel=(?<quote2>['""]?)stylesheet\k<quote2>)|(?:rel=(?<quote3>['""]?)stylesheet\k<quote3>[^>]*href=(?<quote4>['""]?)(?<src>[^'""\s><]+)\k<quote4>))[^>]*>", RegOptions);

        /// <summary>
        /// 验证script脚本引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是Js文件src的值
        /// </summary>
        public static Regex RegScriptLink = new Regex(@"<script[^>]*src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>[^>]*></script>", RegOptions);

        /// <summary>
        /// 验证Img图片引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是img标签src的值
        /// </summary>
        public static Regex RegImg = new Regex(@"<img[^>]*src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>[^>]*>", RegOptions);

        /// <summary>
        /// 验证Falsh引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是flash的src的值
        /// </summary>
        public static Regex RegFlash = new Regex(@"(?<quote>['""]?)(?<src>[^'""\s><]+\.swf)\k<quote>", RegOptions);

        /// <summary>
        /// 验证css文件中引用的图片，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是背景图片的url
        /// </summary>
        public static Regex RegCssImgUrl = new Regex(@"url\((?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>\)", RegOptions);

        /// <summary>
        /// 验证css文件中指定的编码类型，如果匹配成功，则Groups[2]是css文件的编码类型
        /// </summary>
        public static Regex RegCssContentType = new Regex(@"@charset\s*([""']?)([^'"";\s]+)\1", RegOptions);
    }

    /// <summary>
    /// 一个封装正则相关的字符串处理类
    /// </summary>
    class RegOpration
    {
        /// <summary>
        /// 用指定的正则表达式截取html文本中你想要的内容(全局搜索)
        /// </summary>
        /// <param name="html">html字符串</param>
        /// <param name="reg">正则表达式</param>
        /// <returns>返回匹配的MatchCollection</returns>
        public static MatchCollection SearchByRegex(string html, Regex reg)
        {
            return reg.Matches(html);
        }
    }
    #endregion

    #region 编解码

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

    #endregion

    #region 文件
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
    #endregion

    #region 路径

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
    }

    #endregion

    #region 流

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

    #endregion

    #region

    /// <summary>
    /// 此类存储所有资源的保存路径
    /// </summary>
    public class DirConfig
    {
        private string htmlDirPath;
        private string imgDirPath;
        private string jsDirPath;
        private string cssDirPath;
        private string flashDirPath;

        /// <summary>
        /// 页面保存文件夹完整路径
        /// </summary>
        public string HtmlDirPath
        {
            get { return htmlDirPath; }
            set { htmlDirPath = value; }
        }

        /// <summary>
        /// 图片保存文件夹完整路径
        /// </summary>
        public string ImgDirPath
        {
            get { return imgDirPath; }
            set { imgDirPath = value; }
        }

        /// <summary>
        /// JS保存文件夹完整路径
        /// </summary>
        public string JsDirPath
        {
            get { return jsDirPath; }
            set { jsDirPath = value; }
        }

        /// <summary>
        /// CSS保存文件夹完整路径
        /// </summary>
        public string CssDirPath
        {
            get { return cssDirPath; }
            set { cssDirPath = value; }
        }

        /// <summary>
        /// Flash保存文件夹完整路径
        /// </summary>
        public string FlashDirPath
        {
            get { return flashDirPath; }
            set { flashDirPath = value; }
        }


        public DirConfig(string htmlDirPath)
            : this(htmlDirPath, null, null, null, null)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="htmlDirPath">页面保存文件夹完整路径</param>
        /// <param name="imgDirPath">图片保存文件夹完整路径</param>
        /// <param name="jsDirPath">JS保存文件夹完整路径</param>
        /// <param name="cssDirPath">CSS保存文件夹完整路径</param>
        /// <param name="flashDirPath">Flash保存文件夹完整路径</param>
        public DirConfig(string htmlDirPath, string imgDirPath, string jsDirPath, string cssDirPath, string flashDirPath)
        {
            this.htmlDirPath = htmlDirPath;
            this.imgDirPath = imgDirPath;
            this.jsDirPath = jsDirPath;
            this.cssDirPath = cssDirPath;
            this.flashDirPath = flashDirPath;
            if (string.IsNullOrEmpty(imgDirPath))
            {
                this.imgDirPath = htmlDirPath + @"\img";
            }
            if (string.IsNullOrEmpty(jsDirPath))
            {
                this.jsDirPath = htmlDirPath + @"\js";
            }
            if (string.IsNullOrEmpty(cssDirPath))
            {
                this.cssDirPath = htmlDirPath + @"\css";
            }
            if (string.IsNullOrEmpty(flashDirPath))
            {
                this.flashDirPath = htmlDirPath + @"\flash";
            }
        }



        /// <summary>
        /// 检查所有路径是否在同一个磁盘下
        /// </summary>
        /// <returns>如果是则返回true，否则返回false</returns>
        public bool CheckLegal()
        {
            string[] arr;
            string drive;
            this.StrictPath();
            if (string.IsNullOrEmpty(htmlDirPath) || string.IsNullOrEmpty(imgDirPath) || string.IsNullOrEmpty(cssDirPath) || string.IsNullOrEmpty(jsDirPath) || string.IsNullOrEmpty(flashDirPath))
            {
                return false;
            }
            arr = new string[] { imgDirPath, cssDirPath, jsDirPath, flashDirPath };
            drive = Directory.GetDirectoryRoot(htmlDirPath);
            foreach (string path in arr)
            {
                if (Directory.GetDirectoryRoot(path) != drive)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 规范好所有路径配置
        /// </summary>
        public void StrictPath()
        {
            this.htmlDirPath = Path.GetFullPath(this.htmlDirPath.TrimEnd('\\'));
            this.imgDirPath = Path.GetFullPath(this.imgDirPath.TrimEnd('\\'));
            this.jsDirPath = Path.GetFullPath(this.jsDirPath.TrimEnd('\\'));
            this.cssDirPath = Path.GetFullPath(this.cssDirPath.TrimEnd('\\'));
            this.flashDirPath = Path.GetFullPath(this.flashDirPath.TrimEnd('\\'));
        }

        /// <summary>
        /// 判断指定的目录是否存在，不存在则创建
        /// </summary>
        public void CreateDir()
        {
            //创建css目录
            if (!Directory.Exists(this.cssDirPath))
            {
                Directory.CreateDirectory(this.cssDirPath);
            }
            //创建JS目录
            if (!Directory.Exists(this.jsDirPath))
            {
                Directory.CreateDirectory(this.jsDirPath);
            }
            //创建Flash目录
            if (!Directory.Exists(this.flashDirPath))
            {
                Directory.CreateDirectory(this.flashDirPath);
            }
            //创建图片目录
            if (!Directory.Exists(this.imgDirPath))
            {
                Directory.CreateDirectory(this.imgDirPath);
            }
        }
    }

    #endregion
}
