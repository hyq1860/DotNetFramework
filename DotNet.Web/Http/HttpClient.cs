using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DotNet.Common;
using DotNet.Common.Logging;
using DotNet.Web.Http;
using Mozilla.NUniversalCharDet;

namespace DotNet.Web
{
    #region 使用实例
    /*
    对HttpWebResponse获取的HTML进行文字编码转换,使之不会出现乱码;
    自动在Session间保持Cookie,Referer等相关信息;
    模拟HTML表单提交;
    向服务器上传文件;
    对二进制的资源,直接获取返回的字节数组(byte[]),或者保存为文件


    获取编码转换后的字符串

    HttpClient client=new HttpClient(url);
    string html=client.GetString();

    GetString()函数内部会查找Http Headers, 以及HTML的Meta标签,试图找出获取的内容的编码信息.如果都找不到,它会使用client.DefaultEncoding, 这个属性默认为utf-8, 也可以手动设置.
    自动保持Cookie, Referer

    HttpClient client=new HttpClient(url1, null, true);
    string html1=client.GetString();
    client.Url=url2;
    string html2=client.GetString();

    这里HttpClient的第三个参数,keepContext设置为真时,HttpClient会自动记录每次交互时服务器对Cookies进行的操作,同时会以前一次请求的Url为Referer.在这个例子里,获取html2时,会把url1作为Referer, 同时会向服务器传递在获取html1时服务器设置的Cookies. 当然,你也可以在构造HttpClient时直接提供第一次请求要发出的Cookies与Referer:

    HttpClient client=new HttpClient(url, new WebContext(cookies, referer), true);

    或者,在使用过程中随时修改这些信息:

    client.Context.Cookies=cookies;
    client.Context.referer=referer;
    模拟HTML表单提交

    HttpClient client=new HttpClient(url);
    client.PostingData.Add(fieldName1, filedValue1);
    client.PostingData.Add(fieldName2, fieldValue2);
    string html=client.GetString();

    上面的代码相当于提交了一个有两个input的表单. 在PostingData非空,或者附加了要上传的文件时(请看下面的上传和文件), HttpClient会自动把HttpVerb改成POST, 并将相应的信息附加到Request上.
    向服务器上传文件

    HttpClient client=new HttpClient(url);
    client.AttachFile(fileName, fieldName);
    client.AttachFile(byteArray, fileName, fieldName);
    string html=client.GetString();

    这里面的fieldName相当于<input type="file" name="fieldName" />里的fieldName. fileName当然就是你想要上传的文件路径了. 你也可以直接提供一个byte[] 作为文件内容, 但即使如此,你也必须提供一个文件名,以满足HTTP规范的要求.
    不同的返回形式

    字符串: string html = client.GetString();
    流: Stream stream = client.GetStream();
    字节数组: byte[] data = client.GetBytes();
    保存到文件:  client.SaveAsFile(fileName);
    或者,你也可以直接操作HttpWebResponse: HttpWebResponse res = client.GetResponse();

    每调用一次上述任何一个方法,都会导致发出一个HTTP Request, 也就是说,你不能同时得到某个Response的两种返回形式.
    另外,调用后它们任意一个之后,你可以通过client.ResponseHeaders来获取服务器返回的HTTP头.
    下载资源的指定部分(用于断点续传,多线程下载)

    HttpClient client=new HttpClient(url);
    //发出HEAD请求,获取资源长度
    int length=client.HeadContentLength();

    //只获取后一半内容
    client.StartPoint=length/2;
    byte[] data=client.GetBytes();

    HeadContentLength()只会发出HTTP HEAD请求.根据HTTP协议, HEAD与GET的作用等同, 但是,只返回HTTP头,而不返回资源主体内容. 也就是说,用这个方法,你没法获取一个需要通过POST才能得到的资源的长度,如果你确实有这样的需求,建议你可以通过GetResponse(),然后从ResponseHeader里获取Content-Length.

     */

    #endregion

    /// <summary>
    /// http请求客户端
    /// </summary>
    public class HttpClient:IDisposable
    {
        #region fields

        private static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(60.0);

        private const string Gzip = "gzip";

        private const string Deflate = "deflate";

        private const int DefaultCopyBufferLength = 8192;

        private const string AcceptLanguage = "en-us,en;q=0.5";

        private const string AcceptGZipEncoding = "gzip,deflate";

        private const string AcceptCharset = "utf-8,*";

        private bool keepContext;

        private string defaultLanguage = "zh-CN";

        public CultureInfo Language { get; set; }

        public IWebProxy proxy;

        private Encoding defaultEncoding = Encoding.UTF8;

        private string accept = "*/*";

        private string userAgent = FakeUserAgents.InternetExplorer8.UserAgent;

        private HttpMethod _method = HttpMethod.Get;

        private HttpClientContext context;

        private readonly List<HttpUploadingFile> files = new List<HttpUploadingFile>();

        private readonly Dictionary<string, string> _postData = new Dictionary<string, string>();

        private string url;

        private WebHeaderCollection headers;

        private WebResponse response;

        private WebRequest webRequest;

        private int startPoint;

        private int endPoint;

        private long contentLength = -1;
        
        #endregion

        #region events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnStatusUpdate(StatusUpdateEventArgs e)
        {
            EventHandler<StatusUpdateEventArgs> temp = StatusUpdate;
            if (temp != null)
                temp(this, e);
        }
        #endregion

        #region properties

        public double Timeout { get; set; }

        /// <summary>
        /// 当前HttpWebResponse实例输出流转换后的内存流
        /// </summary>
        public MemoryStream MemoryStream { get; set; }

        /// <summary>
        /// 是否与 Internet 资源建立持久性连接
        /// Internet 资源的请求所包含的 Connection HTTP 标头带有 Keep-alive 这一值，则为 true；否则为 false。默认为 true。
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 是否自动在不同的请求间保留Cookie, Referer
        /// 是否保持上下文
        /// </summary>
        public bool KeepContext
        {
            get { return this.keepContext; }
            set { this.keepContext = value; }
        }

        /// <summary>
        /// 期望的回应的语言
        /// </summary>
        public string DefaultLanguage
        {
            get { return this.defaultLanguage; }
            set { this.defaultLanguage = value; }
        }

        /// <summary>
        /// GetString()如果不能从HTTP头或Meta标签中获取编码信息,则使用此编码来获取字符串
        /// </summary>
        public Encoding DefaultEncoding
        {
            get { return this.defaultEncoding; }
            set { this.defaultEncoding = value; }
        }

        /// <summary>
        /// 指示发出Get请求还是Post请求
        /// </summary>
        public HttpMethod Method
        {
            get { return this._method; }
            set { this._method = value; }
        }

        /// <summary>
        /// 要上传的文件.如果不为空则自动转为Post请求
        /// </summary>
        public List<HttpUploadingFile> Files
        {
            get { return this.files; }
        }

        /// <summary>
        /// 要发送的Form表单信息
        /// </summary>
        public Dictionary<string, string> PostData
        {
            get { return this._postData; }
        }

        /// <summary>
        /// 获取或设置请求资源的地址
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        /// <summary>
        /// 用于在获取回应后,暂时记录回应的HTTP头
        /// </summary>
        public WebHeaderCollection Headers
        {
            get
            {
                return headers;
            }
        }

        /// <summary>
        /// 获取或设置期望的资源类型
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        /// <summary>
        /// 获取或设置请求中的Http头User-Agent的值
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        /// <summary>
        /// 获取或设置Cookie及Referer
        /// </summary>
        public HttpClientContext Context
        {
            get { return context; }
            set { context = value; }
        }

        /// <summary>
        /// 获取或设置获取内容的起始点,用于断点续传,多线程下载等
        /// </summary>
        public int StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        /// <summary>
        /// 获取或设置获取内容的结束点,用于断点续传,多下程下载等.
        /// 如果为0,表示获取资源从StartPoint开始的剩余内容
        /// </summary>
        public int EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        /// <summary>
        /// 证书文件X509Certificate objx509 = new X509Certificate(Application.StartupPath + "\\123.cer");
        /// </summary>
        public X509Certificate Certificate { get; set; }

        #endregion

        #region constructors
        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        public HttpClient()
            : this(null)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        public HttpClient(string url)
            : this(url, null)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        /// <param name="context">Cookie及Referer</param>
        public HttpClient(string url, HttpClientContext context)
            : this(url, context, false)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        /// <param name="context">Cookie及Referer</param>
        /// <param name="keepContext">是否自动在不同的请求间保留Cookie, Referer</param>
        public HttpClient(string url, HttpClientContext context, bool keepContext)
        {
            this.url = url;
            this.context = context;
            this.keepContext = keepContext;
            this.Language = CultureInfo.CreateSpecificCulture("zh-CN");//EN-US
            if (this.context == null)
                this.context = new HttpClientContext();
        }
        #endregion

        #region AttachFile
        /// <summary>
        /// 在请求中添加要上传的文件
        /// </summary>
        /// <param name="fileName">要上传的文件路径</param>
        /// <param name="fieldName">文件字段的名称(相当于&lt;input type=file name=fieldName&gt;)里的fieldName)</param>
        public void AttachFile(string fileName, string fieldName)
        {
            HttpUploadingFile file = new HttpUploadingFile(fileName, fieldName);
            files.Add(file);
        }

        /// <summary>
        /// 在请求中添加要上传的文件
        /// </summary>
        /// <param name="data">要上传的文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fieldName">文件字段的名称(相当于&lt;input type=file name=fieldName&gt;)里的fieldName)</param>
        public void AttachFile(byte[] data, string fileName, string fieldName)
        {
            HttpUploadingFile file = new HttpUploadingFile(data, fileName, fieldName);
            files.Add(file);
        }
        #endregion

        /// <summary>
        /// 清空PostingData, Files, StartPoint, EndPoint, headers, 并把Verb设置为Get.
        /// 在发出一个包含上述信息的请求后,必须调用此方法或手工设置相应属性以使下一次请求不会受到影响.
        /// </summary>
        public void ClearHttpClientState()
        {
            contentLength = -1;
            if(MemoryStream!=null)
            {
                MemoryStream.Close();
                MemoryStream = null;
            }
            _method = HttpMethod.Get;
            files.Clear();
            _postData.Clear();
            this.headers = null;
            startPoint = 0;
            endPoint = 0;
        }

        /// <summary>
        /// 使用web代理
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public void UseHttpProxy(string ip, string port, string userName, string password)
        {
            proxy = new WebProxy
                {
                    Address = new Uri("http://" + ip + ":" + port),
                    Credentials = new NetworkCredential(userName, password)
                };
        }

        /// <summary>
        /// 将当前HttpWebResponse实例中的服务器返回流复制到一个MemoryStream的实例
        /// </summary>
        /// <param name="webResponse"></param>
        private void ConvertResponseStreamToMemoryStream(HttpWebResponse webResponse)
        {
            if (webResponse == null || MemoryStream != null)
            {
                return;
            }

            string responseEncoding = webResponse.ContentEncoding.ToLower();

            // 获取当前实例的网络输出流
            Stream stream = webResponse.GetResponseStream();

            // 将网络输出流读取到内存流的中转字节数组
            byte[] buffer;

            this.MemoryStream = new MemoryStream(webResponse.ContentLength > 0 ? (int)webResponse.ContentLength : 3000);

            // 每次从服务器返回流中读取5000个字节
            buffer = new byte[5000];

            // 一个整型变量，记录每次从GZip压缩流中读取的字节数
            int count = 0;
            try
            {
                // 如果服务器输出流使用了GZip或者Deflate压缩，如果是则进行解压，然后复制到内存流
                if (responseEncoding.Contains(Gzip) || responseEncoding.Contains(Deflate))
                {
                    Stream compressStream;
                    if (responseEncoding.Contains(Gzip))
                    {
                        compressStream = new GZipStream(stream, CompressionMode.Decompress);
                    }
                    else
                    {
                        compressStream = new DeflateStream(stream, CompressionMode.Decompress);
                    }

                    using (compressStream)
                    {
                        while ((count = compressStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            this.MemoryStream.Write(buffer, 0, count);
                            this.OnStatusUpdate(
                                new StatusUpdateEventArgs(this.url, true, (int)this.MemoryStream.Length, buffer.Length));
                        }
                    }
                }
                else
                {
                    // 如果服务器输出流，没有进行压缩，则仅将输出流复制到内存流
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        this.MemoryStream.Write(buffer, 0, count);
                        this.OnStatusUpdate(
                            new StatusUpdateEventArgs(this.url, false, (int)this.MemoryStream.Length, buffer.Length));
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                this.MemoryStream.Close();
                stream.Close();
                return;
            }
            catch (ObjectDisposedException ex)
            {
                this.MemoryStream.Close();
                stream.Close();
                return;
            }
            catch (InvalidDataException ex)
            {
                this.MemoryStream.Close();
                stream.Close();
                return;
            }

            // 将流的可读位置设置到起始值
            this.MemoryStream.Seek(0, SeekOrigin.Begin);
        }

        public void SaveFile(string filePath)
        {
            var webResponse = this.GetResponse();
            try
            {
                if (webResponse != null)
                {
                    this.ConvertResponseStreamToMemoryStream(webResponse);
                }

                FileUtility.SaveFile(filePath, MemoryStream);
            }
            finally
            {
                if (webResponse != null)
                {
                   webResponse.Close();
                }

                if (MemoryStream != null)
                {
                    MemoryStream.Close();
                }
            }
        }

        //回调验证证书问题
        //证书相关，安全策略模式
        private bool CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受   
            return true;
        }

        private HttpWebRequest CreateRequest()
        {
            if (Certificate != null)
            {
                // 这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(CertificateValidation);
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(this.url);

            this.InitHttpWebRequest(webRequest);

            if (this._method == HttpMethod.Head)
            {
                webRequest.Method = "Head";
                return webRequest;
            }

            if (this._postData.Count > 0 || this.files.Count > 0)
            {
                this._method = HttpMethod.Post;
            }
                
            if (_method == HttpMethod.Post)
            {
                webRequest.Method = "Post";
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    using(StreamWriter writer = new StreamWriter(memoryStream))
                    {
                        if (files.Count > 0)
                        {
                            string newLine = "\r\n";
                            string boundary = Guid.NewGuid().ToString().Replace("-", "");
                            webRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                            //组织表单数据
                            foreach (string key in _postData.Keys)
                            {
                                writer.Write("--" + boundary + newLine);
                                writer.Write("Content-Disposition: form-data; name=\"{0}\"{1}{1}", key, newLine);
                                writer.Write(_postData[key] + newLine);
                            }

                            //组织上传文件数据
                            foreach (HttpUploadingFile file in files)
                            {
                                writer.Write("--" + boundary + newLine);
                                writer.Write(
                                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}",
                                    file.FieldName,
                                    file.FileName,
                                    newLine);
                                writer.Write("Content-Type: application/octet-stream" + newLine + newLine);
                                writer.Flush();
                                memoryStream.Write(file.Data, 0, file.Data.Length);
                                writer.Write(newLine);
                                writer.Write("--" + boundary + newLine);
                            }
                        }
                        else
                        {
                            webRequest.ContentType = "application/x-www-form-urlencoded";
                            StringBuilder sb = new StringBuilder();
                            foreach (string key in _postData.Keys)
                            {
                                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key),
                                                HttpUtility.UrlEncode(_postData[key]));
                            }

                            if (sb.Length > 0)
                            {
                                sb.Length--;
                            }

                            writer.Write(sb.ToString());
                        }

                        writer.Flush();

                        using (Stream stream = webRequest.GetRequestStream())
                        {
                            memoryStream.WriteTo(stream);
                        }
                }
            }
        }

            if (this.startPoint != 0 && this.endPoint != 0)
            {
                webRequest.AddRange(startPoint, endPoint);
            }
            else if (this.startPoint != 0 && this.endPoint == 0)
            {
                webRequest.AddRange(startPoint);
            }
                
            return webRequest;
        }

        private void InitHttpWebRequest(HttpWebRequest webRequest)
        {
            if (webRequest == null)
            {
                return;
            }

            // 指示请求是否应跟随重定向响应
            webRequest.AllowAutoRedirect = true;
            webRequest.CookieContainer = new CookieContainer();

            if (this.proxy != null)
            {
                webRequest.Proxy = proxy;
            }

            webRequest.Headers["Accept-Language"] = Language.Name;

            // webRequest.Headers.Add("Accept-Language", Language.Name);
            webRequest.Accept = accept;
            webRequest.UserAgent = userAgent;
            webRequest.KeepAlive = false;
            if (this.Timeout > 0)
            {
                webRequest.Timeout = (int)Timeout*1000;
            }

            if (context.Cookies != null)
            {
                webRequest.CookieContainer.Add(this.context.Cookies);
            }

            if (!string.IsNullOrEmpty(this.context.Referer))
            {
                webRequest.Referer = context.Referer;
            }
        }

        /// <summary>
        /// 发出一次新的请求,并返回获得的回应
        /// 调用此方法永远不会触发StatusUpdate事件.
        /// </summary>
        /// <returns>相应的HttpWebResponse</returns>
        public HttpWebResponse GetResponse()
        {
            HttpWebRequest req = this.CreateRequest();
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                this.headers = res.Headers;
                if (keepContext)
                {
                    context.Cookies = res.Cookies;
                    context.Referer = url;
                }
                return res;
            }
            catch (WebException webException)
            {
                Logger.Log("WebException:"+this.url);
                req.Abort();
                return null;
            }
        }

        /// <summary>
        /// 发出一次新的请求,并以字节数组形式返回回应的内容
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <returns>包含回应主体内容的字节数组</returns>
        public byte[] GetBytes()
        {
            HttpWebResponse webResponse = this.GetResponse();
            return GetBytes(webResponse);
        }

        public byte[] GetBytes(HttpWebResponse webResponse)
        {
            if (webResponse == null)
            {
                return null;
            }

            int length = (int)webResponse.ContentLength;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = new byte[0x100];
                using (Stream rs = webResponse.GetResponseStream())
                {
                    if (rs != null)
                    {
                        for (int i = rs.Read(buffer, 0, buffer.Length); i > 0; i = rs.Read(buffer, 0, buffer.Length))
                        {
                            memoryStream.Write(buffer, 0, i);
                            OnStatusUpdate(new StatusUpdateEventArgs(url,false,(int)memoryStream.Length, length));
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 发出一次新的请求,以Http头,或Html Meta标签,或DefaultEncoding指示的编码信息对回应主体解码 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <returns></returns>
        public string Request()
        {
            using (HttpWebResponse webResponse = this.GetResponse())
            {
                if (webResponse == null)
                {
                    return string.Empty;
                }

                // 将流复制到属性MemoryStream上
                this.ConvertResponseStreamToMemoryStream(webResponse);

                // 解码并返回
                return this.DecodeData(webResponse);
            }
        }

        /// <summary>
        /// 是否包含乱码
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private bool IsContainUnreadableCode(string txt)
        {
            return false;
        }

        /// <summary>
        /// UniversalCharDet算法识别编码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private Encoding GetEncodingByUniversalCharDet(byte[] bytes)
        {
            var detector = new UniversalDetector(null);
            var detectBuffer = new byte[4096];
            while (this.MemoryStream.Read(detectBuffer, 0, detectBuffer.Length) > 0 && !detector.IsDone())
            {
                detector.HandleData(detectBuffer, 0, detectBuffer.Length);
            }

            detector.DataEnd();

            if (!string.IsNullOrEmpty(detector.GetDetectedCharset()))
            {
                return Encoding.GetEncoding(detector.GetDetectedCharset());
            }

            return null;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="webResponse"></param>
        /// <returns></returns>
        public string DecodeData(HttpWebResponse webResponse)
        {
            if (MemoryStream == null || MemoryStream.Length == 0)
            {
                return string.Empty;
            }

            if (webResponse != null && webResponse.StatusCode == HttpStatusCode.OK)
            {
                byte[] pageBytes = MemoryStream.ToBytes();

                // 基于火狐的统计学算法
                Encoding encoding = GetEncodingByUniversalCharDet(pageBytes);

                // headers meta BOM的查找方式
                Encoding secondEncoding = GetStringUsingEncoding(webResponse, pageBytes);

                if (encoding != null && encoding.EncodingName != secondEncoding.EncodingName)
                {
                    encoding = secondEncoding;
                }

                return encoding.GetString(pageBytes);
            }

            return string.Empty;
        }


        /// <summary>
        /// 发出一次新的请求,以Http头,或Html Meta标签,或DefaultEncoding指示的编码信息对回应主体解码
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <returns>解码后的字符串</returns>
        public string Request(byte[] response)
        {
            byte[] data = response;
            string encodingName = GetEncodingFromHeaders(); //?? GetEncodingFromBody(this.response,data);

            Encoding encoding;
            if (encodingName == null)
            {
                encoding = defaultEncoding;
            }
            else
            {
                try
                {
                    encoding = Encoding.GetEncoding(encodingName);
                }
                catch (ArgumentException)
                {
                    encoding = defaultEncoding;
                }
            }

            return encoding.GetString(data);
        }


        /// <summary>
        /// 发出一次新的请求,对回应的主体内容以指定的编码进行解码
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <param name="encoding">指定的编码</param>
        /// <returns>解码后的字符串</returns>
        public string Request(Encoding encoding)
        {
            byte[] data = GetBytes();
            return encoding.GetString(data);
        }

        private Encoding GetStringUsingEncoding(HttpWebResponse webResponse, byte[] data)
        {
            Encoding enc = null;
            int bomLengthInData = -1;

            // 尝试从head上找
            // Figure out encoding by first checking for encoding string in Content-Type HTTP header 
            // This can throw NotImplementedException if the derived class of WebRequest doesn't support it.
            string contentType;
            try
            {
                contentType = this.headers["Content-Type"];
            }
            catch (NotImplementedException)
            {
                contentType = null;
            }
            catch (NotSupportedException)  // need this since our FtpWebRequest class mistakenly does this 
            {
                contentType = null;
            }
            // Unexpected exceptions are thrown back to caller 

            if (contentType != null)
            {
                contentType = contentType.ToLower(CultureInfo.InvariantCulture);
                string[] parsedList = contentType.Split(new char[] { ';', '=', ' ' });
                bool nextItem = false;
                foreach (string item in parsedList)
                {
                    if (item == "charset")
                    {
                        nextItem = true;
                    }
                    else if (nextItem)
                    {
                        try
                        {
                            enc = Encoding.GetEncoding(item);
                        }
                        catch (ArgumentException)
                        {
                            // Eat ArgumentException here. 
                            // We'll assume that Content-Type encoding might have been garbled and wasn't present at all.
                            break;
                        }
                        // Unexpected exceptions are thrown back to caller
                    }
                }
            }

            // 尝试BOM方法判断
            // If no content encoding listed in the ContentType HTTP header, or no Content-Type header present, then 
            // check for a byte-order-mark (BOM) in the data to figure out encoding.
            if (enc == null)
            {
                byte[] preamble;
                // UTF32 must be tested before Unicode because it's BOM is the same but longer.
                Encoding[] encodings = { Encoding.UTF8, Encoding.UTF32, Encoding.Unicode, Encoding.BigEndianUnicode };
                for (int i = 0; i < encodings.Length; i++)
                {
                    preamble = encodings[i].GetPreamble();
                    if (ByteArrayHasPrefix(preamble, data))
                    {
                        enc = encodings[i];
                        bomLengthInData = preamble.Length;
                        break;
                    }
                }
            }

            // 尝试从meta中查找
            if (enc == null)
            {
                enc = GetEncodingFromBody(webResponse, data);
            }

            // Do we have an encoding guess?  If not, use default.
            if (enc == null)
            {
                enc = this.Encoding;
            }
                
            // Calculate BOM length based on encoding guess.  Then check for it in the data.
            //if (bomLengthInData == -1)
            //{
            //    byte[] preamble = enc.GetPreamble();
            //    if (ByteArrayHasPrefix(preamble, data))
            //        bomLengthInData = preamble.Length;
            //    else
            //        bomLengthInData = 0;
            //}

            return enc;
            // Convert byte array to string stripping off any BOM before calling GetString(). 
            // This is required since GetString() doesn't handle stripping off BOM.
            //return enc.GetString(data, bomLengthInData, data.Length - bomLengthInData);
        }

        private bool ByteArrayHasPrefix(byte[] prefix, byte[] byteArray)
        {
            if (prefix == null || byteArray == null || prefix.Length > byteArray.Length)
            {
                return false;
            }
                
            for (int i = 0; i < prefix.Length; i++)
            {
                if (prefix[i] != byteArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// charset parameter in HTTP Content-type header. 
        /// </summary>
        /// <returns></returns>
        private string GetEncodingFromHeaders()
        {
            string encoding = null;
            string contentType = this.headers["Content-Type"];
            if (contentType != null)
            {
                int i = contentType.IndexOf("charset=", StringComparison.Ordinal);
                if (i != -1)
                {
                    encoding = contentType.Substring(i + 8);
                }
            }

            return encoding;
        }

        private Encoding GetEncodingFromBody(HttpWebResponse webResponse, byte[] data)
        {
            string encodingName = null;
            Encoding enc = null;
            string dataAsDefault = Encoding.Default.GetString(data);

            if (!string.IsNullOrEmpty(dataAsDefault))
            {
                int i = dataAsDefault.IndexOf("charset=");
                if (i != -1)
                {
                    int j = dataAsDefault.IndexOf("\"", i);
                    if (j != -1)
                    {
                        int k = i + 8;
                        encodingName = dataAsDefault.Substring(k, (j - k) + 1);
                        char[] chArray = new char[2] { '>', '"' };
                        encodingName = encodingName.TrimEnd(chArray);
                        enc = Encoding.GetEncoding(encodingName);
                    }
                }
                else
                {
                    Match meta = Regex.Match(dataAsDefault, "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    encodingName = (meta.Groups.Count > 2) ? meta.Groups[2].Value : string.Empty;
                    encodingName = encodingName.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty);
                    if (encodingName.Length > 0)
                    {
                        encodingName = encodingName.ToLower().Replace("iso-8859-1", "gbk");
                        //encoding = Encoding.GetEncoding(charter);
                    }
                    else
                    {
                        if (webResponse.CharacterSet.ToLower().Trim() == "iso-8859-1")
                        {
                            enc = Encoding.GetEncoding("gbk");
                        }
                        else
                        {
                            enc = string.IsNullOrEmpty(webResponse.CharacterSet.Trim()) ? Encoding.UTF8 : Encoding.GetEncoding(webResponse.CharacterSet);
                        }
                    }
                }
            }

            return enc;
        }

        /// <summary>
        /// 发出一次新的Head请求,获取资源的长度
        /// 此请求会忽略PostingData, Files, StartPoint, EndPoint, _method
        /// </summary>
        /// <returns>返回的资源长度</returns>
        public int HeadContentLength()
        {
            this.ClearHttpClientState();
            HttpMethod lastMethod = _method;
            _method = HttpMethod.Head;
            using (HttpWebResponse res = GetResponse())
            {
                _method = lastMethod;
                return res == null ? 0 : (int) res.ContentLength;
            }
        }

        /// <summary>
        /// 发出一次新的请求,把回应的主体内容保存到文件
        /// 调用此方法会触发StatusUpdate事件
        /// 如果指定的文件存在,它会被覆盖
        /// </summary>
        /// <param name="fileName">要保存的文件路径</param>
        public void SaveAsFile(string fileName)
        {
            SaveAsFile(fileName, FileExistsAction.Overwrite);
        }

        /// <summary>
        /// 发出一次新的请求,把回应的主体内容保存到文件
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <param name="fileName">要保存的文件路径</param>
        /// <param name="existsAction">指定的文件存在时的选项</param>
        /// <returns>是否向目标文件写入了数据</returns>
        public bool SaveAsFile(string fileName, FileExistsAction existsAction)
        {
            byte[] data = GetBytes();
            switch (existsAction)
            {
                case FileExistsAction.Overwrite:
                    using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)))
                        writer.Write(data);
                    return true;

                case FileExistsAction.Append:
                    using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write)))
                        writer.Write(data);
                    return true;

                default:
                    if (!File.Exists(fileName))
                    {
                        using (
                            BinaryWriter writer =
                                new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                            writer.Write(data);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
      
        public delegate void HandleResult(string result);
        private HandleResult handle;

        public void BeginRequest(HandleResult handle)
        {
            if (Certificate != null)
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(CertificateValidation);
            }

            this.handle = handle;
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);
            InitHttpWebRequest(webRequest);
            try
            {
                webRequest.BeginGetResponse(new AsyncCallback(HandleResponse), webRequest);
                //ThreadPool.QueueUserWorkItem(obj => webRequest.BeginGetResponse(new AsyncCallback(HandleResponse), webRequest));
            }
            catch(ProtocolViolationException protocolViolationException)
            {
                throw protocolViolationException;
            }
            catch(WebException webException)
            {
                throw webException;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }
        }

        public void HandleResponse(IAsyncResult asyncResult)
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            string result = string.Empty;
            try
            {
                httpRequest = (HttpWebRequest)asyncResult.AsyncState;
                httpResponse = (HttpWebResponse)httpRequest.EndGetResponse(asyncResult);
                this.headers = httpResponse.Headers;
                if (this.keepContext)
                {
                    context.Cookies = httpResponse.Cookies;
                    context.Referer = url;
                }

                var bytes = GetBytes(httpResponse);
                result = Request(bytes);
            }
            catch (WebException webException)
            {
                throw webException;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (httpRequest != null)
                {
                    httpRequest.Abort();
                }

                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }

            handle(result);
        }

        #region 来自webclient

        Encoding m_Encoding = Encoding.UTF8; 

        public Encoding Encoding
        {
            get
            {
                return m_Encoding;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Encoding");
                }

                m_Encoding = value;
            }
        }


        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class CharsetListener : ICharsetListener
    {
        public string Charset;
        public void Report(string charset)
        {
            this.Charset = charset;
        }
    }

    /// <summary>
    /// http上下文
    /// </summary>
    public class HttpClientContext
    {
        /// <summary>
        /// 
        /// </summary>
        public CookieCollection Cookies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Referer { get; set; }
    }

    /// <summary>
    /// 伪装的UserAgent
    /// </summary>
    public class FakeUserAgent
    {
        private string name;
        private string userAgent;

        public FakeUserAgent(string name, string userAgent)
        {
            this.name = name;
            this.userAgent = userAgent;
        }

        public string Name
        {
            get { return name; }
        }

        public string UserAgent
        {
            get { return userAgent; }
        }
    }

    /// <summary>
    /// 浏览器的UserAgent
    /// </summary>
    public class FakeUserAgents
    {
        public static readonly FakeUserAgent Chrome = new FakeUserAgent("Chrome", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.13 (KHTML, like Gecko) Chrome/9.0.597.98 Safari/534.13");
        public static readonly FakeUserAgent InternetExplorer8 = new FakeUserAgent("Internet Explorer 8", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CMDTDF; .NET4.0C; .NET4.0E)");
    }

    /// <summary>
    /// 请求的方法
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// 
        /// </summary>
        Get,

        /// <summary>
        /// 
        /// </summary>
        Post,

        /// <summary>
        /// 
        /// </summary>
        Head,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileExistsAction
    {
        Overwrite,
        Append,
        Cancel,
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpUploadingFile
    {
        private string fileName;
        private string fieldName;
        private byte[] data;

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fieldName"></param>
        public HttpUploadingFile(string fileName, string fieldName)
        {
            this.fileName = fileName;
            this.fieldName = fieldName;
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                byte[] inBytes = new byte[stream.Length];
                stream.Read(inBytes, 0, inBytes.Length);
                data = inBytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        /// <param name="fieldName"></param>
        public HttpUploadingFile(byte[] data, string fileName, string fieldName)
        {
            this.data = data;
            this.fileName = fileName;
            this.fieldName = fieldName;
        }
    }

    /// <summary>
    /// </summary>
    public class StatusUpdateEventArgs : EventArgs
    {
        private readonly int _bytesGot;

        private readonly int _bytesTotal;

        private readonly string _url;

        private readonly bool _compress;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="got"></param>
        /// <param name="total"></param>
        public StatusUpdateEventArgs(string url,bool isCompressed,int got, int total)
        {
            _url = url;
            _bytesGot = got;
            _bytesTotal = total;
            _compress = isCompressed;
        }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string Url
        {
            get
            {
                return this._url;
            }
        }

        public bool IsCompressed
        {
            get
            {
                return this._compress;
            }
        }

        /// <summary>
        /// 已经下载的字节数
        /// </summary>
        public int BytesGot
        {
            get { return this._bytesGot; }
        }

        /// <summary>
        /// 资源的总字节数
        /// </summary>
        public int BytesTotal
        {
            get { return this._bytesTotal; }
        }
    }
}
