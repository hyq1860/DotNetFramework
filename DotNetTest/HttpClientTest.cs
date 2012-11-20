// -----------------------------------------------------------------------
// <copyright file="HttpClientTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using DotNet.Web;

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HttpClientTest
    {
        public static void Test()
        {
            var urls = new List<string>();
            //urls.Add("http://blogz.sohu.com/index/c13981.shtml");
            //urls.Add("http://www.cnblogs.com/kwklover/archive/2007/01/22/627173.html");
            //urls.Add("http://www.soxuan.com");
            urls.Add("http://www.google.com.hk/");
            urls.Add("http://www.cnblogs.com");
            urls.Add("http://www.miniclip.com/games/en/");
            urls.Add("http://www.baidu.com");
            urls.Add("http://www.360buy.com");
            foreach (string url in urls)
            {
                HttpClient client=new HttpClient(url);
                client.Timeout = 30000;
                string html=client.Request();
                Console.Write(html);
            }
        }
    }
}
