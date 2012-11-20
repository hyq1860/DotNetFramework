using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using EasySpider.Utility;

namespace EasySpider
{
    /// <summary>
    /// 对HttpWebResponse做上一层封装
    /// </summary>
    public class CHttpWebResponse : IDisposable
    {
        public CHttpWebResponse(HttpWebResponse response)
        {
            this.target = response;
            this.success = null != response;
        }
        private HttpWebResponse target;
        private MemoryStream memoryStream;
        private bool success;
        private string content;
        private Encoding encode;
        /// <summary>
        /// 使用默认编码对服务器输出内容解码后的字符串，很有可能乱码，所以设置为私有
        /// </summary>
        private string undecodeContent;

        /// <summary>
        /// 服务器返回的文本内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        /// <summary>
        /// 创建当前对象时对应的HttpWebResponse实例
        /// </summary>
        public HttpWebResponse Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// 当前HttpWebResponse实例输出流转换后的内存流
        /// </summary>
        public MemoryStream MemoryStream
        {
            get { return memoryStream; }
            set { memoryStream = value; }
        }

        /// <summary>
        /// 当前文本的编码（此属性仅当内容是文本时才有用）
        /// </summary>
        public Encoding Encode
        {
            get { return encode; }
            set { encode = value; }
        }

        /// <summary>
        /// 从当前实例中获取服务器返回的内容
        /// </summary>
        /// <param name="encode">指定编码，如果未指定则使用自动职别编码</param>
        /// <param name="close">指定获取内容以后是否释放资源</param>
        /// <returns>返回一个字符串，如果获取内容失败，则返回一个空字符串</returns>
        public string GetContent(Encoding encode, bool close)
        {
            try
            {
                if (null == this.target)
                {
                    return "";
                }
                if (null == this.content)
                {
                    //将服务器输出的流存储到一个MemoryStream实例中;
                    this.SetMemoryStream();
                    //如果未指定编码，则获取服务器返回的流的编码，以免读取网页出现乱码            
                    if (encode == null)
                    {
                        encode = this.GetEncoding();
                    }
                    this.content = encode.GetString(this.memoryStream.GetBuffer(), 0, (int)this.memoryStream.Length);
                }
                return this.content;
            }
            finally
            {
                //释放资源
                if (close)
                {
                    this.Close();
                }
            }

        }

        /// <summary>
        /// 从当前实例中获取服务器返回的内容，并释放相关资源
        /// </summary>
        /// <returns>返回一个字符串，如果获取内容失败，则返回一个空字符串</returns>
        public string GetContent()
        {
            return this.GetContent(null, true);
        }

        /// <summary>
        /// 根据当前实例的url以及mime类型，获取一个文件名
        /// </summary>
        /// <returns>返回文件名</returns>
        public string GetFileName()
        {
            //文件名、文件后缀名、已经文件的mime类型
            string fileName, ext, mimeType;
            fileName = Path.GetFileName(this.Target.ResponseUri.AbsolutePath);
            //如果从url中获取文件名失败，则创建一个随机文件名
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = FileUtility.GetRandomFileName();
            }
            //如果文件没有扩展名，尝试从Mime类型中获取扩展名
            if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
            {
                mimeType = this.Target.ContentType;
                if (mimeType != null && -1 != mimeType.IndexOf(';'))
                {
                    mimeType = mimeType.Substring(0, mimeType.IndexOf(';'));
                }
                ext = Mime.GetExtension(mimeType);
                if (!string.IsNullOrEmpty(ext))
                {
                    fileName += "." + ext;
                }
            }
            return fileName;
        }

        /// <summary>
        /// 将当前服务器返回HTML的封装为一个WebPage实例并返回
        /// </summary>
        /// <returns>返回一个WebPage实例</returns>
        public WebPage GetWebPage()
        {
            return new WebPage(this.GetContent(), this.target.ResponseUri.AbsoluteUri, this.GetEncoding());
        }

        /// <summary>
        /// 将当前HttpWebResponse实例中的服务器返回流复制到一个MemoryStream的实例
        /// </summary>
        public void SetMemoryStream()
        {
            if (null == this.target)
            {
                return;
            }
            //内存流仅实例化一次
            if (null != this.memoryStream)
            {
                return;
            }
            //当前实例
            HttpWebResponse response = this.target;
            string responseEncoding = response.ContentEncoding.ToLower();
            //获取当前实例的网络输出流
            Stream stream = response.GetResponseStream();
            //一个整型变量，记录每次从GZip压缩流中读取的字节数
            int count = 0;
            //将网络输出流读取到内存流的中转字节数组
            byte[] buffer;

            this.memoryStream = new MemoryStream(response.ContentLength > 0 ? (int)response.ContentLength : 3000);
            //每次从服务器返回流中读取5000个字节
            buffer = new byte[5000];
            try
            {
                //如果服务器输出流使用了GZip或者Deflate压缩，如果是则进行解压，然后复制到内存流
                if (responseEncoding.Contains("gzip") || responseEncoding.Contains("deflate"))
                {
                    Stream compressStream;
                    if (responseEncoding.Contains("gzip"))
                    {
                        compressStream = new GZipStream(stream, CompressionMode.Decompress);
                    }
                    else
                    {
                        compressStream = new DeflateStream(stream, CompressionMode.Decompress);
                    }
                    using (compressStream)
                    {

                        while (true)
                        {
                            count = compressStream.Read(buffer, 0, buffer.Length);
                            if (count == 0)
                            {
                                break;
                            }
                            this.memoryStream.Write(buffer, 0, count);
                        }
                    }
                }//如果服务器输出流，没有进行压缩，则仅将输出流复制到内存流
                else
                {
                    while (true)
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        if (count == 0)
                        {
                            break;
                        }
                        this.memoryStream.Write(buffer, 0, count);
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                this.memoryStream.Close();
                stream.Close();
                return;
            }
            catch (ObjectDisposedException ex)
            {
                this.memoryStream.Close();
                stream.Close();
                return;
            }
            catch (InvalidDataException ex)
            {
                this.memoryStream.Close();
                stream.Close();
                return;
            }
            //将流的可读位置设置到起始值
            this.memoryStream.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Close()
        {
            if (null != this.target)
            {
                this.target.Close();
            }
            if (null != this.memoryStream)
            {
                this.memoryStream.Close();
            }
        }

        /// <summary>
        /// 实现IDisposable接口
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        #region 获取当前页面编码的一些方法
        /// <summary>
        /// 获取当前实例所用的编码，优先级为：HttpWebResponse.Headers["Content-Type"] -> html中的meta中的Content-Type -> 默认配置编码
        /// </summary>    
        /// <returns>返回一个Encoding实例，如果获取失败，则返回默认配置的ResponseEncode</returns>
        public Encoding GetEncoding()
        {
            //获取当前实例
            HttpWebResponse response = this.target;
            //用于作返回值的Encoding实例
            Encoding encode = null;
            //当前实例的指定的contentType，以及使用utf8解码的内容
            string contentType = "";
            if (null == this.encode)
            {
                if (null != response)
                {
                    //1.首先从HttpWebResponse的Content-Type这个Header中寻找
                    contentType = this.GetContentTypeByHeader();
                    if (null == contentType)
                    {
                        this.SetUndecodeContent();
                        //2.如果是css文件，则从css文件内容中寻找
                        if (response.ContentType.ToLower().Contains("text/css"))
                        {
                            var mat = RegexCollection.RegCssContentType.Match(this.undecodeContent);
                            if (mat.Groups[2].Success)
                            {
                                contentType = mat.Groups[2].Value;
                            }
                        }
                        //3.如果http头里的Content-Type没有指定编码格式，则在Meta的Content-Type中寻找
                        if (contentType == null)
                        {
                            contentType = this.GetContentTypeByMeta();
                            //4.如果在html中的meta中也没有找到编码，则使用HttpWebResponse.CharacterSet
                            if (contentType == null)
                            {
                                contentType = this.GetContentTypeByCharacterSet();
                            }
                        }
                    }
                }
                //将得到的编码字符串实例化为一个编码，如果实例化失败，则返回默认编码
                try
                {
                    encode = Encoding.GetEncoding(contentType);
                }
                catch (ArgumentException ex)
                {
                    encode = CommonConfig.GetResponseEncode();
                }
                this.encode = encode;
            }
            return this.encode;
        }

        /// <summary>
        /// 从HttpResponse.CharacterSet获取Content-Type
        /// </summary>
        /// <returns></returns>
        private string GetContentTypeByCharacterSet()
        {
            if (null != this.target)
            {
                return this.target.CharacterSet;
            }
            return null;
        }

        /// <summary>
        /// 从一个html字符串中，获取meta中的Content-Type(例如gb2312),如果没有在meta中搜索到Content-Type，则返回null
        /// </summary>        
        /// <returns>返回一个编码类型的字符串，例如gb2312，如果没有在meta中搜索到Content-Type，则返回null</returns>
        private string GetContentTypeByMeta()
        {
            this.SetUndecodeContent();
            var match = RegexCollection.RegMetaContentType.Match(this.undecodeContent);
            if (match.Groups[1].Success)
            {
                string contentType = match.Groups[1].Value;
                contentType = contentType.Substring(contentType.IndexOf("=") + 1).Trim();
                return contentType;
            }
            return null;
        }

        /// <summary>
        /// 从一个HttpWebResponseHeaders中获取Content-Type(例如gb2312),如果没有在HttpResponse.Headers中搜索到Content-Type，则返回null
        /// </summary>
        /// <returns>返回一个编码类型的字符串，例如gb2312，如果没有在HttpResponse.Headers中搜索到Content-Type，则返回null</returns>
        private string GetContentTypeByHeader()
        {
            if (null != this.target)
            {
                if (string.IsNullOrEmpty(this.target.ContentType))
                {
                    return null;
                }
                if (this.target.ContentType.IndexOf("=") != -1)
                {
                    string contentType = this.target.ContentType;
                    contentType = contentType.Substring(contentType.IndexOf("=") + 1).Trim();
                    return contentType;
                }
            }
            return null;
        }

        /// <summary>
        /// 使用默认编码对服务器输出的内容进行解码，设置到this.undecodeContent
        /// </summary>
        private void SetUndecodeContent()
        {
            if (null == this.undecodeContent)
            {
                this.SetMemoryStream();
                //将流的可读位置设置到起始值
                this.memoryStream.Seek(0, SeekOrigin.Begin);
                //此处使用utf-8读取后，即使中文是乱码，也能正确读取到meta中的Content-Type,然后再使用正确的编码类型重读一次
                this.undecodeContent = Encoding.UTF8.GetString(this.memoryStream.GetBuffer(), 0, (int)this.memoryStream.Length);
            }
        }
        #endregion
    }
}
