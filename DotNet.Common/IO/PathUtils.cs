using System;
using System.Text.RegularExpressions;

using DotNet.Common.Utility;

namespace DotNet.Common.IO
{
    /// <summary>
    /// 路径相关操作helper
    /// </summary>
    public static class PathUtils
    {
        internal const int MAX_PATH_LENGTH = 260;

        private static string _baseDirectory = GetBaseDirectory();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetBaseDirectory()
        {
            string bd = string.Empty;
            try
            {
                bd = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/');
            }
            catch
            {
                
            }
            return bd;
        }

        /// <summary>
        /// 获取完全限定位置路径。
        /// </summary>
        /// <param name="fullPathOrRelativePath">完全限定位置路径(D:\\)或相对路径(~/Web)。</param>
        /// <returns>
        /// 如果 <paramref name="fullPathOrRelativePath"/> 是完全限定位置路径，则直接返回。<br/>
        /// 如果 <paramref name="fullPathOrRelativePath"/> 是相对路径，则返回该相对路径的对应的完全限定位置路径。
        /// </returns>
        /// <remarks><paramref name="fullPathOrRelativePath"/> 不可以是站点虚拟目录路径。</remarks>
        public static string GetFullPath(string fullPathOrRelativePath)
        {
            string path = fullPathOrRelativePath;

            if (!IsFullPath(path))
            {
                if (fullPathOrRelativePath.StartsWith("~"))
                    path = path.TrimStart('~');

                path = string.Format("{0}\\{1}",
                    _baseDirectory,
                    path.TrimStart('\\', '/').Replace("/", "\\"));
            }

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string ToVirtualPath(string fullPath)
        {
            return string.Format("~/{0}", fullPath.Replace("\\", "/").Replace(_baseDirectory.Replace("\\", "/"), "").TrimStart(new char[] { '/' }));
        }

        /// <summary>
        /// 获取一个值，该值指示指定的路径字符串是否包含完全限定位置的文件或目录。
        /// </summary>
        /// <param name="path">要测试的路径。</param>
        /// <returns>如果 <paramref name="path"/> 包含完全限定位置的字符串（例如“C:\”），则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法不验证路径或文件名是否存在。
        /// <para>
        /// <typeparamref name="IsFullPath"/> 为 <paramref name="path"/> 字符串（例如 “\\MyDir”和“\\MyDir\\MyFile.txt”和 “C:\\MyDir”和“C:\\MyDir\\MyFile.txt”）返回 true。
        /// 它为 <paramref name="path"/> 字符串（例如“MyDir”和“MyDir\\MyFile.txt”）返回 false。
        /// </para>
        /// </para>
        /// </remarks>
        private static Regex _re_dir = new Regex(@"^[A-Z]:(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex _re_shared_dir = new Regex(@"^(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static bool IsFullPath(string path)
        {
            if (StringHelper.IsEmpty(path))
            {
                return false;
            }
            //// 磁盘目录模式 (格式：D:\\)
            //Regex dirRegex = new Regex(@"^[A-Z]:(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase);
            //// 磁盘共享目录模式(格式：\\MyComputer)
            //Regex sharedDirRegex = new Regex(@"^(\\{1,2}([^\\/:\*\?<>\|]*))*$", RegexOptions.IgnoreCase);

            return _re_dir.IsMatch(path) || _re_shared_dir.IsMatch(path);
        }

        /// <summary>
        /// 获取一个值，该值指示指定的路径字符串是否包含完全限定位置的文件。
        /// </summary>
        /// <param name="path">要测试的路径。</param>
        /// <returns>如果 <paramref name="path"/> 包含完全限定位置的字符串（例如“C:\MyFile.txt”），则为 true；否则为 false。</returns>
        /// <remarks>
        /// 此方法不验证路径或文件名是否存在。
        /// <para>
        /// <typeparamref name="IsFileFullPath"/> 为 <paramref name="path"/> 字符串（例如“\\MyDir\\MyFile.txt”和“C:\\MyDir\\MyFile.txt”）返回 true。
        /// 它为 <paramref name="path"/> 字符串（例如“MyDir”和“MyDir\\MyFile.txt”）返回 false。
        /// </para>
        /// </remarks>
        private static Regex _re_file = new Regex(@"^[A-Z]:\\{1,2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex _re_shared_file = new Regex(@"^\\{2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static bool IsFileFullPath(string path)
        {
            if (StringHelper.IsEmpty(path))
            {
                return false;
            }
            //// 磁盘目录文件模式 (格式：D:\\Test.gif)
            //Regex dirRegex = new Regex(@"^[A-Z]:\\{1,2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase);
            //// 磁盘共享文件模式(格式：\\MyComputer\Test.gif)
            //Regex sharedDirRegex = new Regex(@"^\\{2}(([^\\/:\*\?<>\|]+)\\{1,2})+([^\\/:\*\?<>\|]+)(\.[A-Z]+)$", RegexOptions.IgnoreCase);

            return _re_file.IsMatch(path) || _re_shared_file.IsMatch(path);
        }

    }
}