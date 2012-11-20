using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasySpider
{
    /// <summary>
    /// 需要Post到服务器的客户端文件封装
    /// </summary>
    public class HttpPostFile
    {
        private Stream stream;
        private string fileName;
        private string contentType;
        private string clientName;

        /// <summary>
        /// 文件对应的流
        /// </summary>
        public Stream Stream
        {
            get { return stream; }
            set { stream = value; }
        }
        
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        
        /// <summary>
        /// 文件的mime类型
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }
        
        /// <summary>
        /// 文件在表单中的name
        /// </summary>
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        public HttpPostFile(string fileName,string contentType,string clientName,Stream stream)
        {
            this.fileName = fileName;
            this.contentType = contentType;
            this.clientName = clientName;
            this.stream = stream;
        }

        /// <summary>
        /// 根据一个本地文件路径。创建一个HttpPostFile实例并返回
        /// </summary>
        /// <param name="filepath">文件绝对路径</param>
        /// <param name="clientName">文件在form中的name</param>
        /// <param name="mime">文件的mime类型，如果为null则自动获取</param>
        /// <returns>返回一个HttpPostFile实例</returns>
        public static HttpPostFile FromFilePath(string filepath,string clientName,string mime)
        {
            FileStream stream = new FileStream(filepath, FileMode.Open);
            if (string.IsNullOrEmpty(mime))
            {
                mime = Mime.GetMimeType(Path.GetExtension(filepath));
            }
            return new HttpPostFile(Path.GetFileName(filepath), mime, clientName, stream);
        }
    }
}
