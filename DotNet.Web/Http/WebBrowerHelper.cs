// -----------------------------------------------------------------------
// <copyright file="WebBrowerHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace DotNet.Web.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WebBrowerHelper
    {
        public void ThreadWebBrowser(string url)
        {
           Thread tread = new Thread(new ParameterizedThreadStart(BeginCatch));
           tread.SetApartmentState(ApartmentState.STA);
           tread.Start(url);
        }

        public void BeginCatch(object obj)
        {
            string url = obj.ToString();
            WebBrowser wb = new WebBrowser();
            wb.ScriptErrorsSuppressed = true;
            //在这里Navigate一个空白页面
            wb.Navigate("about:blank");
            HttpClient hc = new HttpClient(url);

            string htmlcode = hc.Request();
            wb.Document.Write(htmlcode);
            //执行分析操作   ……(略) 
        }

        //WebClient取网页源码
        public string GetHtmlSource(string Url)
        {
             string text1 = "";
             try
             {
                WebClient wc = new WebClient();
                text1 = wc.DownloadString(Url);
             }
             catch (Exception exception1)
             {}
             return text1;
        }

    }
}
