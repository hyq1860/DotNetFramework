using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace EasySpider
{
    /// <summary>
    /// 用于异步Http请求的委托类型
    /// </summary>
    /// <param name="response">一个封装好的CHttpWebResponse的实例</param>
    public delegate void ResponseCallback(CHttpWebResponse response);

    /// <summary>
    /// 用于设置HttpWebRequest标头的委托类型
    /// </summary>
    /// <param name="request">一个HttpWebRequest实例</param>
    /// <param name="value">标头值</param>
    public delegate void HttpHeaderSet(HttpWebRequest request, string value);

    /// <summary>
    /// 用于在异步下载资源时处理url
    /// </summary>
    /// <param name="url">资源的url</param>
    /// <returns>返回处理后的url</returns>
    public delegate string MatchCallback(string url);
}
