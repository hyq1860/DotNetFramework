using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.Web;
using System.Diagnostics;
using System.IO.Compression;
using EasySpider.Utility;

namespace EasySpider
{
    /// <summary>
    /// 自写的专用于网络爬取类
    /// </summary>
    public class Spider
    {
        #region 同步方法
        /// <summary>
        /// 传入一个url地址，向服务器发送请求，获取其返回的HttpWebResponse实例，如果过程中出现错误，则返回null
        /// </summary>
        /// <param name="url">一个url地址</param>
        /// <returns>返回一个请求后得到的HttpWebResponse实例</returns>
        public static CHttpWebResponse Get(string url)
        {
            return Get(new CHttpWebRequest(url));
        }

        /// <summary>
        /// 传入一个CHttpWebRequest的实例，向服务器发送请求，获取其返回的HttpWebResponse实例，如果过程中出现错误，则返回null
        /// </summary>
        /// <param name="cRequest">一个CHttpWebRequest实例</param>
        /// <returns>返回一个请求后得到的HttpWebResponse实例</returns>
        public static CHttpWebResponse Get(CHttpWebRequest cRequest)
        {
            HttpWebResponse response = null;
            try
            {
                response = cRequest.Target.GetResponse() as HttpWebResponse;
            }
            catch (WebException wex)
            {
                cRequest.Target.Abort();
            }
            return new CHttpWebResponse(response);
        }

        /// <summary>
        /// 使用指定编码（若为null，则使用UTF-8）将一个键值对进行编码，然后发送http的post请求到指定的url，最后返回一个HttpWebResponse（如果出现错误，则返回null）
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="keyValues">需要发送到服务器的键值对</param>
        /// <param name="files">需要提交到服务器的文件列表</param>
        /// <param name="encoding">指定编码，如果为null则使用默认编码</param>
        /// <returns>返回一个请求后得到的HttpWebResponse实例</returns>
        public static CHttpWebResponse Post(string url, Dictionary<string, string> keyValues, List<HttpPostFile> files, Encoding encoding)
        {
            return Post(CreateRequest(url), keyValues, files, encoding);
        }

        /// <summary>
        /// 传入一个CHttpWebRequest实例，使用指定编码（若为null，则使用UTF-8）向服务器发送http请求，并返回一个HttpWebResponse（如果出现错误，则返回null）
        /// </summary>
        /// <param name="cRequest">一个HttpWebRequest，通常会设置一些标头用以欺骗服务器</param>
        /// <param name="keyValueList">需要发送到服务器的键值对</param>
        /// <param name="encoding">指定请求编码，如果为null则使用默认编码</param>
        /// <returns>返回一个请求后得到的HttpWebResponse实例</returns>
        public static CHttpWebResponse Post(CHttpWebRequest cRequest, Dictionary<string, string> keyValues, List<HttpPostFile> files, Encoding encoding)
        {
            HttpWebResponse response = null;
            try
            {
                //设置请求方式为post
                cRequest.Target.Method = "POST";
                WriteRequestData(cRequest.Target, cRequest.Target.GetRequestStream(), keyValues, files, encoding);
                response = cRequest.Target.GetResponse() as HttpWebResponse;
            }
            catch (ProtocolViolationException ex)
            {
                cRequest.Target.Abort();
            }
            catch (WebException ex)
            {
                cRequest.Target.Abort();
            }
            return new CHttpWebResponse(response);

        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 使用get方式发送一个异步http请求，并传入请求完成后调用的委托
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="callback">请求完成后调用的委托</param>
        public static void AsyncGet(string url, ResponseCallback callback)
        {
            AsyncGet(CreateRequest(url), callback);
        }

        /// <summary>
        /// 使用get方式发送一个异步http请求，并传入请求完成后调用的委托（使用request.BeginGetResponse(responseCallback, request)开始异步请求，request为此方法的第一个参数），之所以有这个重载方法是因为很多时候我们需要手动设置HTTP请求的标头
        /// </summary>
        /// <param name="cRequest">一个HttpWebRequest，这个HttpWebRequest通常会根据不同的url地址来设置一些http标头用以骗过服务器</param>
        /// <param name="callback">请求完成后调用的委托</param>
        public static void AsyncGet(CHttpWebRequest cRequest, ResponseCallback callback)
        {
            cRequest.Target.BeginGetResponse(InternalCallback, new object[] { cRequest, callback });
        }

        /// <summary>
        /// 使用post方式发送一个异步http请求，并传入请求完成后调用的委托
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="keyValues">需要发送到服务器的键值对</param>
        /// <param name="files">需要提交到服务器的文件列表</param>
        /// <param name="encoding">指定请求编码，如果为null则使用默认编码</param>
        /// <param name="callback">异步获取发送流的委托，在这个委托里写入要发送给服务器的内容</param>
        public static void AsyncPost(string url, Dictionary<string, string> keyValues, List<HttpPostFile> files, Encoding encoding, ResponseCallback callback)
        {
            AsyncPost(CreateRequest(url), keyValues, files, encoding, callback);
        }

        /// <summary>
        /// 使用post方式发送一个异步http请求，并传入请求完成后调用的委托（使用request.BeginGetResponse(responseCallback, request)开始异步请求，request为此方法的第一个参数），之所以有这个重载方法是因为很多时候我们需要手动设置HTTP请求的标头
        /// </summary>
        /// <param name="cRequest">一个HttpWebRequest，这个HttpWebRequest通常会根据不同的url地址来设置一些http标头用以骗过服务器</param>
        /// <param name="keyValues">需要发送到服务器的键值对</param>
        /// <param name="files">需要提交到服务器的文件列表</param>
        /// <param name="encoding">指定请求编码，如果为null则使用默认编码</param>
        /// <param name="callback">异步获取发送流的委托，在这个委托里写入要发送给服务器的内容</param>
        public static void AsyncPost(CHttpWebRequest cRequest, Dictionary<string, string> keyValues, List<HttpPostFile> files, Encoding encoding, ResponseCallback callback)
        {
            cRequest.Target.Method = "POST";
            //异步POST需要使用BeginGetRequestStream才能实现异步效果
            cRequest.Target.BeginGetRequestStream(delegate(IAsyncResult asyncResult)
            {
                CHttpWebRequest asyncCRequest = (CHttpWebRequest)asyncResult.AsyncState;
                WriteRequestData(asyncCRequest.Target, asyncCRequest.Target.EndGetRequestStream(asyncResult), keyValues, files, encoding);
                //在委托 AsyncGet 里关闭 request 的RequesStream，才会开始向服务器发送请求
                asyncCRequest.Target.BeginGetResponse(InternalCallback, new object[] { cRequest, callback });
            }, cRequest);
        }
        #endregion

        #region 其他方法

        #region 异步请求时公用的委托，在此委托中调用用户自定义的委托
        /// <summary>
        /// 异步请求时公用的委托，在此委托中调用用户自定义的委托
        /// </summary>
        private static AsyncCallback InternalCallback = new AsyncCallback(delegate(IAsyncResult ar)
        {
            object[] objs = (object[])ar.AsyncState;
            CHttpWebRequest cRequest = (CHttpWebRequest)objs[0];
            ResponseCallback callback = (ResponseCallback)objs[1];
            CHttpWebResponse cResponse;
            HttpWebResponse response = null;
            try
            {
                response = cRequest.Target.EndGetResponse(ar) as HttpWebResponse;
            }
            catch (WebException wex)
            {
                cRequest.Target.Abort();
            }
            cResponse = new CHttpWebResponse(response);
            callback(cResponse);
        });
        #endregion

        /// <summary>
        /// 将需要post到服务器的键值对以及文件写入http请求流中
        /// </summary>
        /// <param name="request">一个HttpWebRequest的实例</param>
        /// <param name="postStream">当前实例的post流，通过HttpWebRequest.GetRerquestStream（同步）或者HttpWebRequest.GetRerquestStream（异步）得到</param>
        /// <param name="keyValues">键值对</param>
        /// <param name="files">文件列表</param>
        /// <param name="encoding">指定请求编码，如果为null则使用默认编码</param>
        /// <returns>如果写入成功返回true，否则返回false</returns>
        public static bool WriteRequestData(HttpWebRequest request, Stream postStream, Dictionary<string, string> keyValues, List<HttpPostFile> files, Encoding encoding)
        {
            string newline = CommonConfig.GetNewLine();
            byte[] bytes;
            int readBytes = 0;
            if (encoding == null)
            {
                encoding = CommonConfig.GetRequestEncode();
            }
            try
            {

                //如果有需要提交的文件
                if (files != null && files.Count > 0)
                {
                    //生成边界线
                    string boundary = "------" + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N").Substring(0, 2);

                    request.ContentType = "multipart/form-data; boundary=" + boundary.Substring(2);
                    using (postStream)
                    {
                        //先写入文本内容
                        foreach (KeyValuePair<string, string> pair in keyValues)
                        {
                            bytes = encoding.GetBytes(string.Format("{1}{0}Content-Disposition: form-data; name=\"{2}\"{0}{0}{3}{0}", newline, boundary, pair.Key, pair.Value));
                            postStream.Write(bytes, 0, bytes.Length);
                        }
                        foreach (HttpPostFile file in files)
                        {
                            using (file.Stream)
                            {
                                if (!file.Stream.CanRead)
                                {
                                    continue;
                                }
                                if (file.Stream.CanSeek)
                                {
                                    file.Stream.Seek(0, SeekOrigin.Begin);
                                }

                                //写入文件相关信息
                                bytes = encoding.GetBytes(string.Format("{1}{0}Content-Disposition: form-data; name=\"{2}\"; filename=\"{3}\"{0}Content-Type: {4}{0}{0}", newline, boundary, file.ClientName, file.FileName, file.ContentType));
                                postStream.Write(bytes, 0, bytes.Length);
                                //写入文件流
                                if (file.Stream.CanSeek)
                                {
                                    bytes = new byte[1024 * 1024];
                                    while ((readBytes = file.Stream.Read(bytes, 0, bytes.Length)) > 0)
                                    {
                                        postStream.Write(bytes, 0, readBytes);
                                    }
                                }
                                bytes = encoding.GetBytes(newline);
                                postStream.Write(bytes, 0, bytes.Length);
                            }
                        }
                        bytes = encoding.GetBytes(string.Format("{1}--{0}", newline, boundary));
                        postStream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    using (postStream)
                    {
                        int i = 0;
                        foreach (KeyValuePair<string, string> pair in keyValues)
                        {
                            bytes = encoding.GetBytes(string.Format("{0}={1}{2}", pair.Key, pair.Value, ++i != keyValues.Count ? "&" : ""));
                            postStream.Write(bytes, 0, bytes.Length);
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据一个url，创建一个CHttpWebRequest，url需要包含http://
        /// </summary>
        /// <param name="url">request的url</param>
        /// <returns></returns>
        public static CHttpWebRequest CreateRequest(string url)
        {
            return new CHttpWebRequest(url);
        }
        #endregion

        #region 资源下载
        /// <summary>
        /// 使用同步保存资源的函数，自动获取文件名，将css，js，flash，图片等资源文件保存到本地。
        /// </summary>
        /// <param name="fileUrl">资源在公网上的url路径</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>   
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        public static string SaveResource(string fileUrl, string dirPath)
        {
            return SaveResource(fileUrl, dirPath, null);

        }

        /// <summary>
        /// 使用同步保存资源的函数，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="fileUrl">资源在公网上的url路径</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>   
        /// <param name="fileName">文件名，如果为null或者String.Empty则自动获取（推荐设置设置为自动获取）</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        public static string SaveResource(string fileUrl, string dirPath, string fileName)
        {
            CHttpWebRequest request = CreateRequest(fileUrl);
            return SaveResource(request, dirPath, fileName);

        }

        /// <summary>
        ///  使用同步保存资源的函数，自动获取文件名，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="request">一个CHttpWebRequest的实例</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        public static string SaveResource(CHttpWebRequest request, string dirPath)
        {
            return SaveFile(Get(request), dirPath, null);
        }

        /// <summary>
        ///  使用同步保存资源的函数，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="request">一个CHttpWebRequest的实例</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <param name="fileName">文件名，如果为null或者String.Empty则自动获取（推荐设置设置为自动获取）</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        public static string SaveResource(CHttpWebRequest request, string dirPath, string fileName)
        {
            return SaveFile(Get(request), dirPath, fileName);
        }

        /// <summary>
        /// 使用异步保存资源的函数，自动获取文件名，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="fileUrl">资源在公网上的url路径</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <param name="completed">一个委托实例，下载完成后调用（委托只有一个string参数（如果保存成功此参数为文件在本地的绝对路径，保存失败则为null），无返回值。）</param>        
        public static void SaveResourceAsync(string fileUrl, string dirPath, Action<string> completed)
        {
            SaveResourceAsync(fileUrl, dirPath, completed, null);
        }

        /// <summary>
        /// 使用异步保存资源的函数，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="fileUrl">资源在公网上的url路径</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <param name="completed">一个委托实例，下载完成后调用（委托只有一个string参数（如果保存成功此参数为文件在本地的绝对路径，保存失败则为null），无返回值。）</param>
        /// <param name="fileName">文件名，如果为null或者String.Empty则自动获取（推荐设置设置为自动获取）</param>
        public static void SaveResourceAsync(string fileUrl, string dirPath, Action<string> completed, string fileName)
        {
            CHttpWebRequest cRequest = CreateRequest(fileUrl);
            SaveResourceAsync(cRequest, dirPath, completed, fileName);
        }

        /// <summary>
        /// 用异步保存资源的函数，自动获取文件名，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="cRequest">一个CHttpWebRequest的实例</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <param name="completed">一个委托实例，下载完成后调用（委托只有一个string参数（如果保存成功此参数为文件在本地的绝对路径，保存失败则为null），无返回值。）</param>
        public static void SaveResourceAsync(CHttpWebRequest cRequest, string dirPath, Action<string> completed)
        {
            SaveResourceAsync(cRequest, dirPath, completed, null);
        }

        /// <summary>
        /// 用异步保存资源的函数，将css，js，flash，图片等资源文件保存到本地
        /// </summary>
        /// <param name="cRequest">一个CHttpWebRequest的实例</param>
        /// <param name="dirPath">本地文件夹完整路径，资源文件将保存在此文件夹</param>
        /// <param name="completed">一个委托实例，下载完成后调用（委托只有一个string参数（如果保存成功此参数为文件在本地的绝对路径，保存失败则为null），无返回值。）</param>
        /// <param name="fileName">文件名，如果为null或者String.Empty则自动获取（推荐设置设置为自动获取）</param>
        public static void SaveResourceAsync(CHttpWebRequest cRequest, string dirPath, Action<string> completed, string fileName)
        {
            AsyncGet(cRequest, new ResponseCallback(delegate(CHttpWebResponse res)
            {                
                if (null == completed)
                {
                    SaveFile(res, dirPath, fileName);
                }
                else
                {
                    completed(SaveFile(res, dirPath, fileName));
                }

            }));
        }

        /// <summary>
        /// 从一个CHttpWebResponse实例保存文件到本地
        /// </summary>
        /// <param name="cResponse">一个CHttpWebResponse实例</param>
        /// <param name="dirPath">文件夹的绝对路径</param>
        /// <param name="fileName">文件名，如果为null或者String.Empty则自动获取（推荐设置设置为自动获取）</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        internal static string SaveFile(CHttpWebResponse cResponse, string dirPath, string fileName)
        {
            string filePath;
            //如果成功接收服务器输出的文件流
            if (cResponse.Success)
            {
                //如果没有指定文件名，则自动获取文件名
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = cResponse.GetFileName();
                }
                filePath = dirPath + "\\" + fileName;
                //filePath = Regex.Replace(filePath, @"\{2,}", @"\");
                filePath = Path.GetFullPath(filePath);
                return SaveFile(cResponse, filePath);
            }
            return null;
        }

        /// <summary>
        /// 从一个CHttpWebResponse实例保存文件到本地
        /// </summary>
        /// <param name="cResponse">一个CHttpWebResponse实例</param>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>如果保存成功，返回文件绝对路径，否则返回null</returns>
        static string SaveFile(CHttpWebResponse cResponse, string filePath)
        {
            try
            {
                if (null != cResponse.Target)
                {
                    cResponse.SetMemoryStream();
                    return FileUtility.SaveFile(filePath, cResponse.MemoryStream);
                }
            }
            finally
            {
                //释放相关资源
                cResponse.Close();
            }
            return null;
        }
        #endregion
    }
}