using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySpider
{
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
        private bool useWebSite;

        /// <summary>
        /// 是否根据网站的url结构存储资源，如果是则除了HtmlDirPath的其他属性均作废
        /// </summary>
        public bool UseWebSite
        {
            get { return useWebSite; }
            set { useWebSite = value; }
        }

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
            : this(false, htmlDirPath, null, null, null, null)
        {

        }

        public DirConfig(bool useWebSite, string htmlDirPath)
            : this(useWebSite, htmlDirPath, null, null, null, null)
        {

        }

        public DirConfig(string htmlDirPath, string imgDirPath, string jsDirPath, string cssDirPath, string flashDirPath)
            : this(false, htmlDirPath, imgDirPath, jsDirPath, cssDirPath, flashDirPath)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="useWebSite">是否根据网站的url结构存储资源</param>
        /// <param name="htmlDirPath">页面保存文件夹完整路径</param>
        /// <param name="imgDirPath">图片保存文件夹完整路径</param>
        /// <param name="jsDirPath">JS保存文件夹完整路径</param>
        /// <param name="cssDirPath">CSS保存文件夹完整路径</param>
        /// <param name="flashDirPath">Flash保存文件夹完整路径</param>
        public DirConfig(bool useWebSite, string htmlDirPath, string imgDirPath, string jsDirPath, string cssDirPath, string flashDirPath)
        {
            this.useWebSite = useWebSite;
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
            if (!this.useWebSite)
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

        /// <summary>
        /// 将 uriStr 参数中的地址转换为以HtmlDirPath做为根目录的地址。
        /// 例如：
        /// HtmlDirPath 为  D:\dev\www
        /// webUrl 参数的值为 http://demo.com/css/style.css
        /// 输出的结果为 D:\dev\www\demo.com\css\style.css
        /// </summary>
        /// <param name="savePath">下载的HTML页面的保存路径(绝对路径)</param>
        /// <param name="uriStr">要转换的uri字符串</param>
        /// <returns></returns>
        public static string ResolveLocalUrl(string savePath, string uriStr)
        {
            return ResolveLocalUrl(savePath, new Uri(uriStr));
        }

        /// <summary>
        /// 将 uri 参数中的地址转换为以HtmlDirPath做为根目录的地址
        /// 例如：
        /// HtmlDirPath 为  D:\dev\www
        /// uri 参数的地址为 http://demo.com/css/style.css
        /// 输出的结果为 D:\dev\www\demo.com\css\style.css
        /// </summary>
        /// <param name="savePath">下载的HTML页面的保存路径(绝对路径)</param>
        /// <param name="Uri">要转换的 Uri 对象</param>
        /// <returns></returns>
        public static string ResolveLocalUrl(string savePath, Uri uri)
        {
            //获取URI的绝对路径，并转换为物理路径形式
            var absPath = uri.AbsolutePath.Trim('/').Replace('/', '\\');

            //去除URI中的文件名
            if (Path.HasExtension(absPath))
            {
                absPath = Path.GetDirectoryName(absPath);
            }

            //返回 HtmlDirPath + URI的域名 + URI的绝对路径 的的地址
            return Path.Combine(Path.Combine(savePath, uri.Authority), absPath);
        }
    }
}