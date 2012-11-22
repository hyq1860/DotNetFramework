// -----------------------------------------------------------------------
// <copyright file="Spider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml;

    using DotNet.Base;
    using DotNet.BasicSpider;
    using DotNet.Data;
    using DotNet.SpiderApplication.Service;
    using DotNet.Web;
    using DotNet.Web.Http;

    using HtmlAgilityPack;

    using ScrapySharp.Extensions;

    using csExWB;

    using UrlHelper = DotNet.Web.Http.UrlHelper;

    /// <summary>
    /// 采集方法
    /// </summary>
    public class Spider
    {
        /// <summary>
        /// 一号店商品采集方法
        /// </summary>
        /// <param name="url">全部分类url</param>
        public static void YiHaoDianSpider(string url)
        {
            //http://www.yihaodian.com/product/listAll.do
            HttpClient hc = new HttpClient(url);
            hc.Timeout = 30000;
            var allSortHtml = hc.Request();
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(allSortHtml);
            var firstCategoryContainer = htmlDocument.DocumentNode.SelectNodes("//div[@class='alonesort']");
            //var texts = new List<string>();
            foreach (HtmlNode firstCategoryNode in firstCategoryContainer)
            {
                
                var node = firstCategoryNode.CssSelect(".mt>h3>a");
                
                if (node != null && node.Any())
                {
                    //一级分类
                    var firstCategoryText = node.FirstOrDefault().InnerText;
                    var firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                    Insert(firstKey.ToString(), firstCategoryText, node.FirstOrDefault().Attributes["href"].Value, "0","2");


                    var secondCategoryContainer = firstCategoryNode.CssSelect(".mc>.fore");

                    foreach (HtmlNode htmlNode in secondCategoryContainer)
                    {
                        //二级分类
                        var secondCategoryNode = htmlNode.CssSelect("dt>a").FirstOrDefault();
                        var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                        
                        if(secondCategoryNode.Attributes["href"]!=null)
                        {
                            Insert(secondKey.ToString(), secondCategoryNode.InnerText, secondCategoryNode.Attributes["href"].Value, firstKey.ToString(),"2");
                        }

                        // 三级分类集合
                        var threeCategoryNodes = htmlNode.CssSelect("dd>em>span>a");
                        foreach (HtmlNode threeCategoryNode in threeCategoryNodes)
                        {
                            // 插入三级分类
                            var thirdKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                            if(threeCategoryNode.Attributes["href"]!=null)
                            {
                                Insert(thirdKey.ToString(), threeCategoryNode.InnerText, threeCategoryNode.Attributes["href"].Value, secondKey.ToString(),"2");
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 一号店商品列表页面
        /// </summary>
        public static void YiHaoDianProductList()
        {
            //
            HttpClient hc = new HttpClient("http://www.yihaodian.com/ctg/searchPage/c5484-%E5%A5%B6%E8%8C%B6/b0/a-s1-v0-p5-price-d0-f04-m1-rt0-pid-k/?callback=jsonp1352021900435");
            hc.Timeout = 30000;
            var allSortHtml = hc.Request();
        }

        public static void TaoBaoDetail()
        {
            //WebClient wb=new WebClient();
            //var html = wb.DownloadString(var allSortHtml = hc.Request(););
            //HttpClient hc = new HttpClient("http://detail.tmall.com/item.htm?id=14444803248");
            //hc.Timeout = 30000;
            //var allSortHtml = hc.Request();


            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.TimeOut = 8;
            var html = WebBrowerManager.Instance.Run("http://detail.tmall.com/item.htm?id=14444803248");

            var htmll = html.Length;
            WebBrowerManager.Instance.Clear();
        }

        private static void Insert(string categoryId, string name, string url, string parentCategoryId, string ECPlatformId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProductCategory2"))
            {
                cmd.CommandText = cmd.CommandText.Replace("@CategoryId", categoryId).Replace("@Name", name).Replace("@Url", url).Replace("@ParentCategoryId", parentCategoryId).Replace("@ECPlatformId",ECPlatformId);
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 易讯
        /// </summary>
        /// <param name="url"></param>
        public static void WuYiBuySpider(string url)
        {

            //CsQuery
            //Fizzler
            //http://code.google.com/p/sharp-query/downloads/list

            //http://www.51buy.com/portal.html
            //HttpClient hc = new HttpClient(url);
            //hc.Timeout = 30000;
            //var allSortHtml = hc.Request();
            WebBrowerManager.Instance.Setup(new cEXWB());
            var html = WebBrowerManager.Instance.Run(url);

            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var container = htmlDocument.DocumentNode.CssSelect("#protal_list");
            if (container == null || !container.Any() || container.FirstOrDefault() == null)
            {
                return;
            }

            var firstCategoryContainer = container.FirstOrDefault().CssSelect(".item");
            foreach (HtmlNode htmlNode in firstCategoryContainer)
            {
                var firstCategoryNode = htmlNode.CssSelect("div.item_hd>h3>a");
                var firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                Insert(firstKey.ToString(), firstCategoryNode.FirstOrDefault().InnerText, firstCategoryNode.FirstOrDefault().Attributes["href"].Value, "0", "3");


                var secondCategoryContainer = htmlNode.CssSelect("dl");

                foreach (HtmlNode node in secondCategoryContainer)
                {
                    var secondCategoryNode = node.CssSelect("dt");

                    var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");

                    if (secondCategoryNode.FirstOrDefault() != null)
                    {
                        Insert(secondKey.ToString(), secondCategoryNode.FirstOrDefault().InnerText, "", firstKey.ToString(), "3");
                    }


                    var threeCategoryContainer = node.CssSelect("dd");

                    foreach (HtmlNode htmlNode1 in threeCategoryContainer)
                    {
                        var threeCategoryNodes = htmlNode1.CssSelect("a");
                        
                        foreach (HtmlNode threeCategoryNode in threeCategoryNodes)
                        {
                            // 插入三级分类
                            var thirdKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                            Insert(thirdKey.ToString(), threeCategoryNode.InnerText, threeCategoryNode.Attributes["href"].Value, secondKey.ToString(), "3");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 易讯商品列表页面数据采集
        /// </summary>
        /// <param name="url"></param>
        public static void WuYiBuyProductList(string url)
        {
            var hcFirst = new HttpClient(url);
            hcFirst.Timeout = 30000;
            var htmlFirst = hcFirst.Request();
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(htmlFirst);

            // 寻找第二页面链接及最大页码

            var secondPageNode = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[2]/div[6]/div[1]/a[1]");
            var secondPageUrl = string.Empty;
            var urlTemplate = string.Empty;
            if(secondPageNode!=null)
            {
                if(secondPageNode.Attributes["href"]!=null&& !string.IsNullOrEmpty(secondPageNode.Attributes["href"].Value))
                {
                    secondPageUrl = secondPageNode.Attributes["href"].Value;
                    var spiltArray = secondPageUrl.Split('-');
                    spiltArray[6] = "{0}";
                    // 每一页面链接模板
                    
                    for (int i = 0; i < spiltArray.Length; i++)
                    {
                        if(i==spiltArray.Length-1)
                        {
                            urlTemplate += spiltArray[i];
                        }
                        else
                        {
                            urlTemplate += spiltArray[i] + "-";
                        }
                    }
                }
            }

            var maxPageNode = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[2]/div[6]/div[1]/a[last()-1]");

            var maxPageNumber = 1;

            //说明有多个页面
            if(maxPageNode!=null&&secondPageNode!=null&&!string.IsNullOrEmpty(urlTemplate))
            {
                int.TryParse(maxPageNode.InnerText, out maxPageNumber);
            }

            for (int i = 1; i <= maxPageNumber; i++)
            {
                if (i != 1)
                {
                    var hc = new HttpClient(string.Format(urlTemplate, i));
                    hc.Timeout = 50000;
                    var html = hcFirst.Request();
                    htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);
                }

                // 寻找当前商品的链接
                var productListNodes = htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[4]/div[2]/div[5]/ul[1]/li");
                if(productListNodes==null)
                {
                    return;
                }

                foreach (HtmlNode productNode in productListNodes)
                {
                    // 商品名称
                    var productNameNode = productNode.SelectSingleNode("./div[1]/h4[1]/a[1]");

                    if(productNameNode==null)
                    {
                        continue;
                    }

                    // 商品列表图
                    var productImageNode = productNode.SelectSingleNode("./a[1]/img[1]");

                    // 商品链接
                    var productHref = productNameNode.Attributes["href"].Value;

                    // 商品评论数量
                    var commentNode = productNode.SelectSingleNode("./div[1]/p[2]/a[1]");

                    // 商品价格
                    var productPriceNode = productNode.SelectSingleNode("./div[2]/p[2]/strong[1]");

                    // 商品原始id
                }
            }
        }

        public void GomeSpider(string url)
        {
            //http://www.gome.com.cn/ec/homeus/allcategory/allCategory.jsp
        }

        public static void DangDangSpider(string url)
        {
            //http://category.dangdang.com/
            WebBrowerManager.Instance.Setup(new cEXWB());
            var html = WebBrowerManager.Instance.Run(url);

            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var container =
                htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[4]/div");

            if (container == null || !container.Any())
                return;

            foreach (HtmlNode htmlNode in container)
            {
                if(htmlNode.HasAttributes&&htmlNode.Attributes["Id"]!=null)
                {
                    var liNodes=htmlNode.SelectNodes("div[2]/ul[1]/li");

                    foreach (HtmlNode liNode in liNodes)
                    {
                        var aNode= liNode.SelectNodes("a");
                        var chars = new char[6]{'&','n','b','s','p',';'};
                        
                        foreach (HtmlNode node in aNode)
                        {
                            var firstKey = 0;
                            if(node.HasAttributes&&node.Attributes["class"]!=null&&node.Attributes["class"].Value=="title")
                            {
                                // 一级分类
                                firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");

                                Insert(firstKey.ToString(), node.InnerText.Trim(chars), node.Attributes["href"].Value, "0", "5");

                            }
                            else
                            {
                                //二级分类
                                
                                    // 一级分类
                                    var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                                    Insert(secondKey.ToString(), node.InnerText, node.Attributes["href"].Value, firstKey.ToString(), "5");
                               
                            }
                        }
                    }
                }
            }
        }

        public static void DangDangProductList(string categoryUrl)
        {
            //
            Uri uri = new Uri(categoryUrl);
            NameValueCollection nameValue = UrlHelper.GetQueryString(uri.Query);
            // 根据url中抽取分类 
            string cid = nameValue["cat"];
            if(string.IsNullOrEmpty(cid))
            {
                return;
            }

            var hcFirst = new HttpClient(categoryUrl);
            hcFirst.Timeout = 30000;
            var htmlFirst = hcFirst.Request();
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(htmlFirst);

            // 查找第一页面
            var maxPageNode=htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[3]/div[5]/div[1]/div[2]/span[1]");
            if(maxPageNode==null||string.IsNullOrEmpty(maxPageNode.InnerText))
            {
                return;
            }
            var maxPageString = maxPageNode.InnerText.Substring("1&nbsp;/&nbsp;".Length);
            //		InnerText	"1&nbsp;/&nbsp;19"	string
            var maxPageNumber = 0;
            if (!int.TryParse(maxPageString, out maxPageNumber))
            {
                return;
            }
            //页面模板
            var tempUrl = "http://category.dangdang.com/all/?category_id={0}&page_index={1}";

            for (int j = 1; j <= maxPageNumber; j++)
            {
                if(j!=1)
                {
                    var hc = new HttpClient(string.Format(tempUrl,cid,j));
                    hc.Timeout = 30000;
                    var html = hc.Request();
                    if(!string.IsNullOrEmpty(html))
                    {
                        htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);
                    }
                }

                var productList =
                htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[3]/div[7]/div");
                if (productList == null || !productList.Any())
                {
                    return;
                }
                int i = 0;
                foreach (HtmlNode htmlNode in productList)
                {
                    if (htmlNode.Attributes["class"] != null && htmlNode.Attributes["class"].Value.Contains("listitem"))
                    {

                        var a = htmlNode.SelectSingleNode("p[1]/a[1]");
                        // 商品页面
                        if (a != null && a.Attributes["href"] != null && !string.IsNullOrEmpty(a.Attributes["href"].Value))
                        {
                            //
                            // 商品链接
                            var productUrl = a.Attributes["href"].Value;
                            // 商品列表页面图片
                            var image = a.SelectSingleNode("img[1]");
                            var productImageUrl = image.Attributes["src"].Value;

                            // 商品名称
                            var titleNode = htmlNode.SelectSingleNode("p[3]/a[1]");

                            // 评论数
                            var commentNode = htmlNode.CssSelect("p.starlevel"); //htmlNode.SelectSingleNode("p[4]/span[1]/a[1]");
                            var commentCount = 0;
                            if(commentNode!=null&&!string.IsNullOrEmpty(commentNode.FirstOrDefault().InnerText))
                            {
                                var index = commentNode.FirstOrDefault().InnerText.IndexOf("条");
                                if(index!=-1)
                                {
                                    var s = commentNode.FirstOrDefault().InnerText.Substring(1, index - 1);
                                    int.TryParse(s, out commentCount);
                                }
                            }
                            
                            if (!DataAccess.IsExistUrl(productUrl))
                            {
                                DataAccess.InsertProduct(titleNode.InnerText, productUrl, int.Parse(cid), commentCount, productImageUrl);
                            }
                        }
                    }
                }
            }

        }

        public static void SuNingSpider(string url)
        {
            //www.suning.com/emall/SNProductCatgroupView?storeId=10052&catalogId=10051&flag=1
            WebBrowerManager.Instance.Setup(new cEXWB());
            var html = WebBrowerManager.Instance.Run(url);

            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var container = htmlDocument.DocumentNode.CssSelect("div.sFloor.clearfix");

            foreach (HtmlNode htmlNode in container)
            {
                var firstNode=htmlNode.CssSelect("h3>a");
                var firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                Insert(firstKey.ToString(), firstNode.FirstOrDefault().InnerText, firstNode.FirstOrDefault().Attributes["href"].Value, "0", "4");

                var temp = new List<HtmlNode>();
                if (htmlNode.CssSelect(".listLeft>dl") != null && htmlNode.CssSelect(".listLeft>dl").Any())
                {
                    temp.AddRange(htmlNode.CssSelect(".listLeft>dl"));
                }
                if (htmlNode.CssSelect(".listRight>dl") != null && htmlNode.CssSelect(".listRight>dl").Any())
                {
                    temp.AddRange(htmlNode.CssSelect(".listRight>dl"));
                }

                foreach (HtmlNode node in temp)
                {
                    var secondNode= node.CssSelect("dt>a").FirstOrDefault();

                    var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");

                    if (secondNode != null)
                    {
                        Insert(secondKey.ToString(), secondNode.InnerText, "", firstKey.ToString(), "4");
                    }

                    var thridNodes = node.CssSelect("dd>span>a");
                    if (thridNodes != null && thridNodes.Any())
                    {
                        foreach (HtmlNode thridNode in thridNodes)
                        {
                            // 插入三级分类
                            var thirdKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                            Insert(thirdKey.ToString(), thridNode.InnerText, thridNode.Attributes["href"].Value, secondKey.ToString(), "4");
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 苏宁列表页面商品提取
        /// </summary>
        /// <param name="url"></param>
        public static object SuNingProductList(object obj)
        {
            var url = obj as string;
            if(string.IsNullOrEmpty(url))
            {
                return false;
            }
            Uri uri = new Uri(url);
            string queryString = uri.Query;
            NameValueCollection nameValue = UrlHelper.GetQueryString(queryString);
            // 根据url中抽取分类 
            string cid = nameValue["ci"];

            if(string.IsNullOrEmpty(cid))
            {
                return false;
            }

            string urlTemplate = "http://search.suning.com/emall/strd.do?ci={0}&cityId=9017&cp={1}&il=0&si=5&st=14&iy=-1";
            var firstPageUrl = string.Format("http://search.suning.com/emall/strd.do?ci={0}&cityId=9017&cp=0&il=0&si=5&st=14&iy=-1", cid);
            var hcFirst = new HttpClient(firstPageUrl);
            hcFirst.Timeout = 30000;
            var htmlFirst = hcFirst.Request();
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(htmlFirst);

            // 先找最大页面页面
            var pageContainer = htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[7]/div[2]/div[8]/a");
            if(pageContainer==null||!pageContainer.Any())
            {
                return false;
            }
            var lastPageNode = pageContainer[pageContainer.Count - 2];

            var lastPageNumber = int.Parse(lastPageNode.InnerText);

            for (int i = 0; i < lastPageNumber; i++)
            {
                if(i==0)
                {
                    // 解析商品
                    var productLis =htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[7]/div[2]/div[6]/ul[1]/li");
                    foreach (var htmlNode in productLis)
                    {
                        var aNode = htmlNode.SelectSingleNode("a");
                        if(aNode!=null)
                        {
                            // 商品名称
                            var name = aNode.Attributes["title"].Value;
                            // 商品链接
                            var href = aNode.Attributes["href"].Value;
                            // 图片
                            var imageNode=aNode.SelectSingleNode("img");
                            var picUrl = string.Empty;
                            if(imageNode!=null&&imageNode.Attributes["src2"]!=null&&!string.IsNullOrEmpty(imageNode.Attributes["src2"].Value))
                            {
                                // 图片url
                                picUrl= imageNode.Attributes["src2"].Value;
                            }

                            // 评论
                            var commentNode = htmlNode.SelectSingleNode("div[1]/div[1]/p[1]/a[1]/i[1]");
                            int commentNum = 0;
                            if(commentNode!=null)
                            {
                                // 评论数目
                                int.TryParse(commentNode.InnerText, out commentNum);
                            }
                            if (!DataAccess.IsExistUrl(href))
                            {
                                DataAccess.InsertProduct(name, href, int.Parse(cid), commentNum, picUrl);
                            }
                        }
                    }
                }
                else
                {
                    var categoryUrl = string.Format(urlTemplate, cid, i);
                    HttpClient hc = new HttpClient(categoryUrl);
                    hc.Timeout = 30000;
                    var html = hc.Request();
                    htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);
                    // 解析商品
                    // 解析商品
                    var productLis = htmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[7]/div[2]/div[6]/ul[1]/li");
                    foreach (var htmlNode in productLis)
                    {
                        var aNode = htmlNode.SelectSingleNode("a");
                        if (aNode != null)
                        {
                            // 商品名称
                            var name = aNode.Attributes["title"].Value;
                            // 商品链接
                            var href = aNode.Attributes["href"].Value;
                            // 图片
                            var imageNode = aNode.SelectSingleNode("img");
                            var picUrl = string.Empty;
                            if (imageNode != null && imageNode.Attributes["src2"] != null && !string.IsNullOrEmpty(imageNode.Attributes["src2"].Value))
                            {
                                // 图片url
                                picUrl = imageNode.Attributes["src2"].Value;
                            }

                            // 评论
                            var commentNode = htmlNode.SelectSingleNode("div[1]/div[1]/p[1]/a[1]/i[1]");
                            int commentNum = 0;
                            if (commentNode != null)
                            {
                                // 评论数目
                                int.TryParse(commentNode.InnerText, out commentNum);
                            }
                            if (!DataAccess.IsExistUrl(href))
                            {
                                DataAccess.InsertProduct(name, href, int.Parse(cid), commentNum, picUrl);
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 采取分类
        /// </summary>
        /// <param name="url"></param>
        public static void AmazonSpider(string url)
        {
            url = "http://www.amazon.cn/gp/site-directory/ref=sa_menu_fullstore";
            //http://www.amazon.cn/gp/site-directory/ref=sa_menu_fullstore
            //var hcFirst = new HttpClient(url);
            //hcFirst.Timeout = 30000;
            //var htmlFirst = hcFirst.Request();
            //var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(htmlFirst);

            WebBrowerManager.Instance.Setup(new cEXWB());
            var html = WebBrowerManager.Instance.Run(url);

            // 注意htmldecode
            html = HttpUtility.HtmlDecode(html);
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var firstCategoryContainer = htmlDocument.DocumentNode.SelectNodes("//div[@class='popover-grouping']");

            if (firstCategoryContainer == null || !firstCategoryContainer.Any())
            {
                return;
            }

            foreach (HtmlNode htmlNode in firstCategoryContainer)
            {
                var firstCategoryNode = htmlNode.SelectSingleNode("//div[@class='popover-category-name']/h2");

                if (firstCategoryNode == null)
                {
                    continue;
                }

                // 一级分类
                var firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                Insert(firstKey.ToString(), firstCategoryNode.InnerText, string.Empty, "0", "8");

                // 二级分类
                var secondCategoryNodes = htmlNode.SelectNodes("div");
                if (secondCategoryNodes == null || !secondCategoryNodes.Any())
                {
                    continue;
                }

                foreach (HtmlNode node in secondCategoryNodes)
                {
                    if (node.Attributes["class"] == null)
                    {
                        var secondCategoryNode = node.SelectSingleNode("a");
                        if (secondCategoryNode != null && secondCategoryNode.Attributes["href"] != null
                            && !string.IsNullOrEmpty(secondCategoryNode.Attributes["href"].Value))
                        {
                            var categoryUrl = "http://www.amazon.cn" + HttpUtility.UrlDecode(secondCategoryNode.Attributes["href"].Value);
                            var categoryName = secondCategoryNode.InnerText;
                            var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory2");
                            Insert(secondKey.ToString(), categoryName, categoryUrl, "0", "8");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 亚马逊产品列表
        /// </summary>
        public static void AmazonProductList(string url)
        {
            //http://www.amazon.cn/gp/site-directory/ref=sa_menu_fullstore
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            Uri uri = new Uri(url);
            string queryString = uri.Query;
            NameValueCollection nameValue = UrlHelper.GetQueryString(queryString);
            // 根据url中抽取分类 
            string node = nameValue["node"];

            if (string.IsNullOrEmpty(node))
            {
                return;
            }

            string urlTemplate = "http://www.amazon.cn/s/ref=?rh=n:{0}&page={1}";
            var firstPageUrl = string.Format(urlTemplate, node,1);
            var hcFirst = new HttpClient(firstPageUrl);
            hcFirst.Timeout = 30000;
            var html = HttpUtility.HtmlDecode(hcFirst.Request());
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var maxPageNode = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='pagnDisabled']");
            var maxPageNumber = 0;
            if(maxPageNode!=null&&int.TryParse(maxPageNode.InnerText,out maxPageNumber))
            {
                for (int i = 1; i <= maxPageNumber; i++)
                {
                    if (i != 1)
                    {
                        var pageUrl = string.Format(urlTemplate, node, i);
                        var hc = new HttpClient(pageUrl);
                        hc.Timeout = 30000;
                        html = HttpUtility.HtmlDecode(hc.Request());
                        htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);
                    }

                    var productNodes =
                        htmlDocument.DocumentNode.SelectNodes(
                            "//div[@class='result product'] | //div[@class='result lastRow product']");
                    if (productNodes == null)
                    {
                        return;
                    }

                    foreach (HtmlNode productNode in productNodes)
                    {
                        var imageNode = productNode.SelectSingleNode("div[@class='image']/a");
                        var titleNode = productNode.SelectSingleNode("div[@class='data']/h3[@class='title']/a");
                    }
                }
            }
        }


        public static void IndexTest()
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(openFileDialog.FileName);
            //XmlNodeList nodes = xmlDoc.SelectNodes(@"News/Item");

            Stopwatch watch = new Stopwatch();

            DateTime old = DateTime.Now;
            int count = 0;
            int MaxCount = 150000; //Math.Min(40000, nodes.Count);

            long totalChars = 0;
            Index.CreateIndex(Index.INDEX_DIR);
            Index.MaxMergeFactor = 301;
            Index.MinMergeDocs = 301;

            //progressBar.Value = 0;
            Application.DoEvents();

            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                //if (!string.IsNullOrEmpty(sqlWhere))
                //{
                //    cmd.CommandText += sqlWhere;
                //}
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        String title = dr["Name"].ToString();
                        DateTime time = DateTime.Parse(dr["InDate"].ToString());
                        String Url = dr["Url"].ToString();
                        String content = dr["Name"].ToString();

                        totalChars += title.Length + 8 + Url.Length + content.Length;


                        watch.Start();

                        Index.IndexString(Index.INDEX_DIR, Url, title, time, content);

                        watch.Stop();

                        count++;
                        //progressBar.Value = count * 100 / MaxCount;
                        //labelProgress.Text = progressBar.Value + "%";
                        Application.DoEvents();

                        if (count >= MaxCount)
                        {
                            break;
                        }

                        if (count % 300 == 0)
                        {
                            Index.CloseWithoutOptimize();
                            Index.CreateIndex(Index.INDEX_DIR);
                            Index.MaxMergeFactor = 301;
                            Index.MinMergeDocs = 301;
                        }
                    }
                }



                watch.Start();

                Index.Close();

                watch.Stop();

                TimeSpan s = DateTime.Now - old;
                MessageBox.Show(
                    String.Format(
                        "插入{0}行数据,共{1}字符,用时{2}秒",
                        MaxCount,
                        totalChars,
                        watch.ElapsedMilliseconds / 1000 + "." + watch.ElapsedMilliseconds % 1000),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }


    }
}
