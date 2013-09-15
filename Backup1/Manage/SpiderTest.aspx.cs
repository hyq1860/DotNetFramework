using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

using DotNet.Web;
using HtmlAgilityPack;

namespace DotNet.EnterpriseWebSite.Manage
{
    using System.IO;
    using System.Net;
    using System.Text;

    using DotNet.Common;
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Service;

    public partial class SpiderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IProductService productService=new ProductService();

            var data=productService.GetProducts(20, 1, string.Empty);
            foreach (ProductInfo productInfo in data)
            {
                productInfo.Content = productInfo.Content.Replace("//upload/", "/upload/");
                productService.Update(productInfo);
            }
            //HtmlAgilityPack_Demo01();
        }

        public void HtmlAgilityPack_Demo01()
        {
            var list = new List<string>();
            list.Add("2");
            list.Add("3");
            list.Add("4");
            list.Add("5");
            list.Add("7");
            list.Add("8");

            list.Add("9");
            list.Add("10");
            list.Add("11");
            list.Add("12");
            list.Add("13");
            list.Add("14");

            list.Add("15");
            list.Add("16");
            list.Add("17");
            list.Add("18");
            list.Add("19");

            list.Add("20");
            list.Add("21");
            list.Add("22");
            list.Add("23");
            list.Add("24");
            list.Add("25");

            list.Add("26");
            list.Add("27");
            list.Add("28");
            list.Add("29");
            list.Add("31");

            foreach (string s in list)
            {
                var webGet = new HtmlWeb();
                HttpClient httpClient1 = new HttpClient("http://www.yihui-lighting.com/productshow.asp?id="+s);
                HtmlAgilityPack.HtmlDocument htmlDocument1 = new HtmlAgilityPack.HtmlDocument();
                htmlDocument1.LoadHtml(httpClient1.Request());
                var document = htmlDocument1;

                var data = document.DocumentNode.SelectNodes("//*[@id=\"mitte2\"]");
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (HtmlNode htmlNode in data)
                {
                    var images = document.DocumentNode.SelectNodes("//img");
                    if (images != null && images.Count > 0)
                    {
                        int j = 0;
                        foreach (HtmlNode imageNode in images)
                        {
                            j++;
                            WebClient webClient = new WebClient();
                            var dir = "images\\content";
                            string filepath = "z:\\upload\\" + dir;
                            if (!Directory.Exists(filepath))
                            {
                                Directory.CreateDirectory("z:\\upload\\" + dir);
                            }

                            if (imageNode.Attributes["src"].Value != null)
                            {
                                try
                                {
                                    string url = "http://www.yihui-lighting.com/UpProduct";
                                    downloadfile(url + "//" + imageNode.Attributes["src"].Value.Substring(imageNode.Attributes["src"].Value.LastIndexOf('/') + 1), imageNode.Attributes["src"].Value.Substring(imageNode.Attributes["src"].Value.LastIndexOf('/') + 1));
                                }
                                catch
                                {

                                }

                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(htmlNode.InnerText))
                    {
                        //
                        var titleNode = document.DocumentNode.SelectSingleNode(
                            "/html/body/div/div[3]/div[3]/div[2]/div/h2/font");
                        var title = titleNode.InnerText??"";
                        var body = htmlNode.InnerHtml.Replace("UpProduct/", "upload/images/content/")??"";

                        var model = new ProductInfo();
                        model.Title = title;
                        model.CategoryId = 2;

                        model.Content = body;

                        model.InDate = DateTime.Now;
                        model.DisplayOrder = OrderGenerator.NewOrder();
                        IProductService productService = new ProductService();
                        try
                        {
                            productService.Add(model);
                        }
                        catch
                        {

                        }
                        
                    }
                }
            }

        }

        public static string GetWebPageHtmlFromUrl(string url)
        {
            var hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(url);
            return doc.DocumentNode.OuterHtml;
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">要下载文件网址</param>
        public void downloadfile(string URL,string fileName)
        {
            WebClient client = new WebClient();
            int n = URL.LastIndexOf('/');
            string URLAddress = URL.Substring(0, n);  //取得网址
            //string fileName = URL.Substring(n + 1, URL.Length - n - 1);  //取得文件名
            string Dir = @"z:\upload\images\\content";  //下载文件存放路径

            string Path = Dir + '\\' + fileName; //下载文件存放完整路径

            Stream stream = client.OpenRead(URL);

            StreamReader reader = new StreamReader(stream);
            byte[] mbyte = new byte[100000];
            int allbyte = (int)mbyte.Length;
            int startbyte = 0;
            while (allbyte > 0)  //循环读取
            {
                int m = stream.Read(mbyte, startbyte, allbyte);
                if (m == 0)
                    break;
                startbyte += m;
                allbyte -= m;
            }

            FileStream fstr = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
            fstr.Write(mbyte, 0, startbyte);  //写文件
            stream.Close();
            fstr.Close();
        }
    }
}