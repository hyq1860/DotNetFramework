// -----------------------------------------------------------------------
// <copyright file="GoldSpider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using DotNet.BasicSpider;
    using DotNet.Common.Utility;
    using DotNet.Web.Http;

    using HtmlAgilityPack;

    using csExWB;

    /// <summary>
    /// 黄金采集
    /// </summary>
    public class GoldSpider
    {
        static GoldSpider()
        {
            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.TimeOut = 30;
        }

        public static List<string> GetUrls()
        {
            var indexUrls = new List<string>();
            var indexUrl = "http://www.sge.sh/publish/sge/xqzx/jyxq/index.htm";

            var firstPage = WebBrowerManager.Instance.Brower(indexUrl);

            if(string.IsNullOrEmpty(firstPage.HtmlSource))
            {
                return indexUrls;
            }

            var firstDocument = HtmlAgilityPackHelper.GetHtmlDocument(firstPage.HtmlSource);
            var firstPageNode = firstDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[3]/div[1]/div[4]/a[1]");
            var lastPageNode = firstDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[3]/div[1]/div[4]/a[4]");

            var maxPageNumber = 0;

            if(lastPageNode.HasAttributes)
            {
                var attr = lastPageNode.Attributes["href"];
                if(attr!=null&& !string.IsNullOrEmpty(attr.Value))
                {
                    var index = attr.Value.IndexOf("index");
                    var pointIndex=attr.Value.IndexOf(".");
                    if(index>=0&&pointIndex>0)
                    {
                        var length= pointIndex - index-"index".Length;
                        var maxPageString = attr.Value.Substring(index + "index".Length, length);

                        int.TryParse(maxPageString, out maxPageNumber);
                    }
                }
            }

            if (maxPageNumber > 0)
            {
                string url = string.Empty;
                for (int i = 0; i <= maxPageNumber; i++)
                {
                    if(i==0)
                    {
                        url = "http://www.sge.sh/publish/sge/xqzx/jyxq/index.htm";
                    }
                    else
                    {
                        url = string.Format("http://www.sge.sh/publish/sge/xqzx/jyxq/index{0}.htm",i);
                    }
                    indexUrls.Add(url);
                }
            }

            return indexUrls;
        }

        public static List<string> Spider(List<string> urls)
        {
            var pageUrls = new List<string>();

            foreach (string indexUrl in urls)
            {
                var html=WebBrowerManager.Instance.Brower(indexUrl).HtmlSource;
                var document=HtmlAgilityPackHelper.GetHtmlDocument(html);
                var liNodes= document.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[3]/div[1]/div[3]/ul[1]/li");
                if (liNodes!=null&&liNodes.Any())
                {
                    foreach (HtmlNode liNode in liNodes)
                    {
                        var aNode = liNode.SelectSingleNode("a");
                        if (aNode != null && aNode.HasAttributes && aNode.Attributes["href"] != null && !string.IsNullOrEmpty(aNode.Attributes["href"].Value))
                        {
                            string pageUrl = string.Format("http://www.sge.sh/publish/sge/xqzx/jyxq/{0}", aNode.Attributes["href"].Value);
                            pageUrls.Add(pageUrl);
                        }
                    }
                }
            }
            return pageUrls;
        }

        public static List<Gold> SpiderPrice(List<string> urls)
        {
            var errorUrls = new List<string>();
            var data = new List<Gold>();
            var chars = new char[6] { '&', 'n', 'b', 's', 'p', ';' };
            foreach (string url in urls)
            {
                var html = WebBrowerManager.Instance.Brower(url).HtmlSource;
                if(string.IsNullOrEmpty(html))
                {
                    errorUrls.Add(url);
                    continue;
                }
                var document = HtmlAgilityPackHelper.GetHtmlDocument(html);
                var tableNode = document.DocumentNode.SelectSingleNode("//div[@class='newscontent']//table");
                if(tableNode!=null)
                {
                    var trNodes= tableNode.SelectNodes("tbody[1]/tr");
                    foreach (var trNode in trNodes)
                    {
                        var firstTdNode= trNode.SelectSingleNode("td");
                        if(firstTdNode!=null)
                        {
                            if (firstTdNode.InnerText.Contains("Au9995") || firstTdNode.InnerText.Contains("Au9999"))
                            {
                                var tdNodes = trNode.SelectNodes("td");
                                if(tdNodes!=null&&tdNodes.Any())
                                {
                                    var dataTdNodes=tdNodes.Take(5).ToList();
                                    try
                                    {
                                        Gold gold = new Gold();
                                        ;
                                        gold.Name = dataTdNodes[0].InnerText.TrimEnd(chars);
                                        gold.OpeningPrice = decimal.Parse(dataTdNodes[1].InnerText);
                                        gold.HighestPrice = decimal.Parse(dataTdNodes[2].InnerText);
                                        gold.LowestPrice = decimal.Parse(dataTdNodes[3].InnerText);
                                        gold.ClosingPrice = decimal.Parse(dataTdNodes[4].InnerText);
                                        gold.DateString =
                                            document.DocumentNode.SelectSingleNode(
                                                "/html[1]/body[1]/div[1]/div[3]/div[1]/div[3]/div[1]/p[1]").InnerText;
                                        data.Add(gold);
                                    }
                                    catch (Exception)
                                    {
                                        errorUrls.Add(url);
                                        continue;
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }

            File.WriteAllText(Environment.CurrentDirectory + "\\errorUrls.json", JsonHelper.ToJson(errorUrls));
            
            return data;
        }
    
    }

    public class Gold
    {
        public string DateString { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public decimal OpeningPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal ClosingPrice { get; set; }
    }
}
