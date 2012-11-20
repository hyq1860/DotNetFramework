using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace EasySpider
{
    /// <summary>
    /// 对HttpWebRequest做上一层封装
    /// </summary>
    public class CHttpWebRequest
    {
        /// <summary>
        /// 受限制的标头数组以及对于的设置方法。某些公共标头被视为受限制的，它们或者直接由 API（如 Content-Type）公开，或者受到系统保护，不能被更改。
        /// </summary>
        private static Dictionary<string, HttpHeaderSet> LimitedHeaders;
        /// <summary>
        /// 静态构造
        /// </summary>
        static CHttpWebRequest()
        {
            LimitedHeaders = new Dictionary<string, HttpHeaderSet>(15);
            LimitedHeaders.Add("Accept", delegate(HttpWebRequest request, string value)
            {
                request.Accept = value;
            });
            LimitedHeaders.Add("Connection", delegate(HttpWebRequest request, string value)
            {
                request.Connection = value;
            });           
            LimitedHeaders.Add("Content-Type", delegate(HttpWebRequest request, string value)
            {
                request.ContentType = value;
            });           
            LimitedHeaders.Add("Expect", delegate(HttpWebRequest request, string value)
            {
                request.Expect = value;
            });            
            LimitedHeaders.Add("If-Modified-Since", delegate(HttpWebRequest request, string value)
            {
                request.IfModifiedSince = DateTime.Parse(value);
            });
           
            LimitedHeaders.Add("Referer", delegate(HttpWebRequest request, string value)
            {
                request.Referer = value;
            });
           
            LimitedHeaders.Add("User-Agent", delegate(HttpWebRequest request, string value)
            {
                request.UserAgent = value;
            });

            //下面4个标头在.Net2.0中没有对应的方法
            LimitedHeaders.Add("Host", delegate(HttpWebRequest request, string value)
            {
                
            });
            LimitedHeaders.Add("Date", delegate(HttpWebRequest request, string value)
            {

            });
            LimitedHeaders.Add("Proxy-Connection", delegate(HttpWebRequest request, string value)
            {

            });
            LimitedHeaders.Add("Range", delegate(HttpWebRequest request, string value)
            {

            });

            //下面这两个标头需要设置为POST请求，且需要写入数据才不会报错
            LimitedHeaders.Add("Content-Length", delegate(HttpWebRequest request, string value)
            {

            });            
            LimitedHeaders.Add("Transfer-Encoding", delegate(HttpWebRequest request, string value)
            {

            });
            /*
                Accept	由 Accept 属性设置。
                Connection	由 Connection 属性和 KeepAlive 属性设置。
                Content-Length	由 ContentLength 属性设置。
                Content-Type	由 ContentType 属性设置。
                Expect	由 Expect 属性设置。
                Date	由系统设置为当前日期。
                Host	由系统设置为当前主机信息。
                If-Modified-Since	由 IfModifiedSince 属性设置。
                Range	由 AddRange 方法设置。
                Referer	由 Referer 属性设置。
                Transfer-Encoding	由 TransferEncoding 属性设置（SendChunked 属性必须为 true）。
                User-Agent	由 UserAgent 属性设置。
             */

        }

        private HttpWebRequest target;
        /// <summary>
        /// 创建当前对象时对应的HttpWebRequest实例
        /// </summary>
        public System.Net.HttpWebRequest Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// 构造一个CHttpWebRequest实例
        /// </summary>
        /// <param name="url">标识 Internet 资源的 URI</param>
        public CHttpWebRequest(string url)
        {
            //if (!RegexCollection.RegUrl.Match(url).Success)
            //{
            //    throw new SpiderException("url不合法");
            //}
            this.target = WebRequest.Create(url) as HttpWebRequest;
            if (null == this.target)
            {
                throw new SpiderException("url不合法");
            }
        }

        /// <summary>
        /// 设置当前HttpWebRequest实例的http标头
        /// </summary>
        /// <param name="name">http标头名称</param>
        /// <param name="value">http标头值</param>
        public void SetHeader(string name, string value)
        {
            name = name.Trim();
            foreach (KeyValuePair<string, HttpHeaderSet> item in LimitedHeaders)
            {
                if (0==string.Compare(name, item.Key, true))
                {
                    item.Value(this.target, value);
                    return;
                }
            }
            this.target.Headers.Set(name, value);
        }

        /// <summary>
        /// 移除请求标头中的Expect标头
        /// </summary>
        public void RemoveExpect()
        {
            this.target.ServicePoint.Expect100Continue = false;
        }

        /// <summary>
        /// 设置设置请求将跟随的重定向的最大数目，如果数目小等于0则关闭请求重定向（HttpWebRequest默认开启请求重定向，且最大数目为50）
        /// </summary>
        /// <param name="num">重定向数目</param>
        public void SetAutoRedirect(int num)
        {            
            if (num > 0)
            {
                this.target.AllowAutoRedirect=true;
                this.target.MaximumAutomaticRedirections = num;
            }
            else
            {
                this.target.AllowAutoRedirect = false;
            }
        }
    }
}
