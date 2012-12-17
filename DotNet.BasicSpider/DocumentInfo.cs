// -----------------------------------------------------------------------
// <copyright file="DocumentInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.BasicSpider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 获取的document信息
    /// </summary>
    public class DocumentInfo
    {
        public DocumentInfo()
        {
            HttpRequestUrls=new List<string>();
        }
        public string Url { get; set; }

        public string Domain { get; set; }

        public string Protocol { get; set; }

        public string Cookie { get; set; }

        public string Referrer { get; set; }

        public string HtmlSource { get; set; }

        public string Title { get; set; }

        public long Elapse { get; set; }
        public List<string> HttpRequestUrls { get; set; } 
    }
}
