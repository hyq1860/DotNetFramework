// -----------------------------------------------------------------------
// <copyright file="HtmlAgilityPackHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace DotNet.Web.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;



    /// <summary>
    /// TODO: Update summary.
    /// xpath一些语法
    /// 1. DocumentNode.SelectNodes("//div[@class='meta']/a");
    /// 2.doc.DocumentNode.SelectNodes("//a[contains(@href,'stackoverflow.com')]")
    /// 3.doc.DocumentNode.Descendants("a").Where(a => a.Attributes["href"] != null&& a.Attributes["href"].Value.ToLower().Contains("stackoverflow.com"));
    /// 4.("//a[starts-with(@id,'thread_title_')]")`  //http://stackoverflow.com/questions/5024503/htmlagilitypack-selecting-nodes-with-end-different
    /// 5.System.Uri u = new System.Uri(url);string LOC = System.Web.HttpUtility.ParseQueryString(u.Query).Get("LOC");
    /// 6.http://stackoverflow.com/questions/8720702/xpath-node-selection-how-to-select-2-different-elements-htmlagilitypack
    /// 7..http://hi.baidu.com/tewuapple/item/316889cd439ae7c1984aa0b0
    /// 8.http://stackoverflow.com/questions/2785092/c-htmlagilitypack-extract-inner-text
    /// 9.http://www.cnblogs.com/ziyunfei/archive/2012/10/05/2710631.html
    /// </summary>
    /*
     */

    public static class HtmlAgilityPackHelper
    {

        #region 扩展方法
        #endregion

        public static HtmlDocument GetHtmlDocument(string html)
        {
            // var webGet = new HtmlWeb();
            //var document = webGet.Load("http://www.cnblogs.com/shanyou/archive/2011/07/30/2122263.html");
            var htmlDocument = new HtmlDocument();
            //Defines if LI, TR, TH, TD tags must be partially fixed when nesting errors are detected. Default is false. 
            htmlDocument.OptionFixNestedTags = true;
            htmlDocument.LoadHtml(html);

            if (htmlDocument.ParseErrors != null && htmlDocument.ParseErrors.Any())
            {
                // Handle any parse errors as required
            }
            else
            {
                if (htmlDocument.DocumentNode != null)
                {
                    HtmlNode bodyNode = htmlDocument.DocumentNode.SelectSingleNode("//body");
                    if (bodyNode != null)
                    {
                        // Do something with bodyNode
                    }
                }
            }

            return htmlDocument;
        }

        /// <summary>
        /// 根据XPATH获取筛选的字符串
        /// </summary>
        /// <param name="content">需要提取HTML的内容</param>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="separ">分隔符</param>
        /// <returns>提取后的内容</returns>
        public static string GetStringByXPath(string content, string xpath, string separ)
        {
            HtmlDocument doc1 = new HtmlDocument();
            doc1.LoadHtml(content);
            HtmlNodeCollection repeatNodes = doc1.DocumentNode.SelectNodes(xpath);
            string text = "";
            //循环节点  
            foreach (HtmlNode node in repeatNodes)
            {
                text += node.InnerText + separ;
            }
            return text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public static void RemoveComments(HtmlNode node)
        {
            foreach (var n in node.ChildNodes.ToArray())
            {
                RemoveComments(n);
            }
            if (node.NodeType == HtmlNodeType.Comment)
            {
                node.Remove();
            }
        }

        /// <summary>
        /// 获取页面的图片路径集合
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<string> GetImageSrcs(string url)
        {
            var document = new HtmlWeb().Load(url);
            var urls = document.DocumentNode.Descendants("img")
                                            .Select(e => e.GetAttributeValue("src", null))
                                            .Where(s => !String.IsNullOrEmpty(s)).ToList();
            return urls;
        }

        public static XElement HtmlToXElement(string html)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            HtmlDocument doc = new HtmlDocument();
            doc.OptionOutputAsXml = true;
            doc.LoadHtml(html);
            using (StringWriter writer = new StringWriter())
            {
                doc.Save(writer);
                using (StringReader reader = new StringReader(writer.ToString()))
                {
                    return XElement.Load(reader);
                }
            }
        }

        /// <summary>
        /// HTML按指定长度截断
        /// </summary>
        /// <param name="htmlString">源HTML字符串</param>
        /// <param name="length">保留长度</param>
        /// <returns></returns>
        public static string TruncateHtml(string htmlString, int length)
        {
            if (string.IsNullOrEmpty(htmlString)) return string.Empty;

            var hdoc = new HtmlDocument() { OptionWriteEmptyNodes = true };
            hdoc.LoadHtml(htmlString);

            var nodes = hdoc.DocumentNode.SelectNodes("//*");

            var countLength = 0;
            var maxLength = length;

            var lastNode =
                    nodes
                        .Where(n => n.HasChildNodes && n.ChildNodes.Count == 1)
                        .TakeWhile(n =>
                        {
                            countLength += n.InnerText.Trim().Length;
                            return countLength <= maxLength;
                        })
                        .LastOrDefault();

            if (lastNode == null) return string.Empty;

            hdoc.LoadHtml(htmlString.Substring(0, lastNode.StreamPosition));
            return hdoc.DocumentNode.WriteTo();
        }

        //
        public static void GetHtmlMeta(string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            var list = doc.DocumentNode.SelectNodes("//meta");
            foreach (var node in list)
            {
                string content = node.GetAttributeValue("content", "");
            }

            doc.DocumentNode.SelectNodes("//meta/@content");
        }
    }
}
