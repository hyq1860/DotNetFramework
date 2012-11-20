using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Interop;
using DotNet.Base;
using DotNet.Common;
using DotNet.Data;
using DotNet.Web;
using DotNet.Web.Http;
using ExtendedWebBrowser2;
using IfacesEnumsStructsClasses;
using InteropConsts;


using ScrapySharp.Extensions;
using csExWB;
using mshtml;
using IHTMLDocument2 = mshtml.IHTMLDocument2;
using IHTMLDocument4 = mshtml.IHTMLDocument4;
using IHTMLElement = mshtml.IHTMLElement;
using IHTMLFrameBase2 = mshtml.IHTMLFrameBase2;

namespace DotNet.SpiderApplication
{
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;
    using System.Threading;

    using DotNet.Common.Logging;

    using HtmlAgilityPack;

    using InteropInterfaces;

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class Form1 : Form
    {

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();

        public Form1()
        {
            InitializeComponent();
            //using (DataCommand cmd = DataCommandManager.GetDataCommand("GetModules"))
            //{
            //    DataTable dt = cmd.ExecuteDataTable();
            //}
            
            //Process();
        }

        public void Process()
        {
           
            string url = "http://www.360buy.com/products/670-671-672-0-0-0-0-0-0-0-1-1-{0}.html";

            HttpClient client=new HttpClient("http://www.360buy.com/products/670-671-672-0-0-0-0-0-0-0-1-1-1.html");
            var html = client.Request();

            //通过第一页找到一共有多少页面
            var htmlDocument= HtmlAgilityPackHelper.GetHtmlDocument(html);

            //var productNodes2 = htmlDocument.GetElementbyId("plist").SelectNodes("//a[@href]");

            var maxPager = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='pagin fr']/a[4]");

            var urls = new List<string>();

            if (maxPager != null)
            {
                int maxPageNumber = 0;
                if (int.TryParse(maxPager.InnerText, out maxPageNumber) && maxPageNumber>0)
                {
                    for (int i = 1; i <= maxPageNumber; i++)
                    {
                        string newUrl = string.Format(url, i);
                        HttpClient hc=new HttpClient(newUrl);
                        var newHtml = hc.Request();
                        var doc = HtmlAgilityPackHelper.GetHtmlDocument(newHtml);

                        var productContainer = doc.GetElementbyId("plist");
                        if(productContainer==null)
                        {
                            //Logger.Log(url+"数据获取有误");
                            continue;
                        }
                        var productNodes = productContainer.SelectNodes("//a");
                        
                        if(productNodes==null||productNodes.Count==0)
                        {
                            //Logger.Log(url + "数据获取有误");
                            continue;
                        }
                        foreach (var productNode in productNodes)
                        {
                            if (productNode.Attributes["href"] != null && productNode.Attributes["href"].Value != null && productNode.Attributes["href"].Value.Contains("product/"))
                            {
                                
                                var href = productNode.Attributes["href"].Value;
                                if (!IsExistUrl(href))
                                {
                                    using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertUrls"))
                                    {
                                        cmd.SetParameterValue("@Guid", System.Guid.NewGuid().ToString());
                                        cmd.SetParameterValue("@Url", href);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            
                        }
 
                        Thread.Sleep(1000);
                    }
                }
            }
            MessageBox.Show("笔记本数据采集完了");
        }

        public void GatherPrice(string productId,string url)
        {
            HttpClient client = new HttpClient(url);
            var html = client.Request();

            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);
            
            //标题
            var title = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[5]/div/div/h1");
            var price = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='p-price']/img");
            // 文字价格
            var priceText = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[5]/div[1]/div[2]/ul[1]/script[1]");

            // 产品图片
            var defaultImage = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[5]/div[1]/div[2]/div[1]");

            // 促销信息是ajax
            if(title!=null&&price!=null&&priceText!=null)
            {
                var beginIndex= priceText.InnerText.IndexOf("京东价：￥");

                var endIndex = priceText.InnerText.IndexOf("。", beginIndex);
                var readPrice = priceText.InnerText.Substring(beginIndex + "京东价：￥".Length, endIndex - beginIndex - "京东价：￥".Length);
            }
        }

        public void GatherPriceV2(string productId, string url,string html)
        {
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            //标题
            var title = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[6]/div[1]/div[1]/h1[1]");
            var price = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='p-price']/img");
            // 文字价格
            var priceText = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[6]/div[1]/div[2]/ul[1]/li[2]/script[1]");

            // 产品图片
            var defaultImage = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[5]/div[1]/div[2]/div[1]");
            
            // 促销信息是ajax
            if (title != null && price != null && priceText != null)
            {
                var beginIndex = priceText.InnerText.IndexOf("京东价：￥");

                var endIndex = priceText.InnerText.IndexOf("。", beginIndex);
                var readPrice = priceText.InnerText.Substring(beginIndex + "京东价：￥".Length, endIndex - beginIndex - "京东价：￥".Length);
                decimal decimalRealPrice = 0;
                if (decimal.TryParse(readPrice,out decimalRealPrice))
                {
                  UpdateProduct(productId, title.InnerText, decimal.Parse(readPrice) );  
                }
                
            }
        }

        public bool IsExistUrl(string url)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("IsExistUrl"))
            {
                cmd.SetParameterValue("@Url", url);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void GatherAll()
        {
            //var d1 = Math.Round(3.346, 2);
            //var d2 = Math.Round(3.341, 2);
            //var d3 = Math.Round(3.34, 2);
            //var d4 = Math.Round(3.346, 2, MidpointRounding.ToEven);
            //var d5 = Math.Round(3.349, 2, MidpointRounding.AwayFromZero);
            //var d6 = Math.Round(3.345, 2, MidpointRounding.AwayFromZero);
            //var d7 = Math.Round(3.345, 2, MidpointRounding.ToEven);
            //var d8 = Math.Floor(3.345);
            //var d9 = Math.Floor(3.347);
            //var d10 = Math.Floor(3.341);
            //var a = Math.Round(2.346 - 0.005, 2);
            var url = "http://www.360buy.com/allSort.aspx";
            HttpClient hc=new HttpClient(url);
            hc.Timeout = 30000;
            var allSortHtml= hc.Request();
            

            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(allSortHtml);

            var categoryContainer = htmlDocument.GetElementbyId("allsort");

            if(categoryContainer==null)
            {
                return;
            }

            var firstCategory = htmlDocument.DocumentNode.SelectNodes("/html/body/div[5]/div/div/div/h2");


            foreach (var first in firstCategory)
            {
                //插入一级分类
                var firstKey = KeyGenerator.Instance.GetNextValue("ProductCategory");
                Insert(firstKey.ToString(), first.InnerText,"","0");
                // 二级分类
                var secondCategorys = first.ParentNode.ParentNode.CssSelect("dt");
                foreach (var secondCategory in secondCategorys)
                {
                    // 插入二级分类
                    var secondKey = KeyGenerator.Instance.GetNextValue("ProductCategory");
                    Insert(secondKey.ToString(), secondCategory.InnerText, "", firstKey.ToString());
                    // 三级分类
                    var thirdCategorys= secondCategory.ParentNode.CssSelect("dd>em>a");
                    foreach (var thirdCategory in thirdCategorys)
                    {
                        // 插入三级分类
                        var thirdKey = KeyGenerator.Instance.GetNextValue("ProductCategory");
                        if(thirdCategory.Attributes["href"]!=null&&!string.IsNullOrEmpty(thirdCategory.Attributes["href"].Value))
                        {
                            Insert(thirdKey.ToString(), thirdCategory.InnerText, thirdCategory.Attributes["href"].Value, secondKey.ToString());
                        }
                        
                    }
                }
            }



        }

        delegate void D(object obj);

        void DelegateSetValue(object obj)
        {
            this.label1.Text = obj.ToString();
        }

        void Message(object src, StatusUpdateEventArgs e)
        {
            var str = e.Url + "[" + e.BytesTotal + ":" + e.BytesGot + "]";
            if (label1.InvokeRequired)
            {
                D d = new D(DelegateSetValue);
                label1.Invoke(d, str);

            }
            else
            {
                this.label1.Text = str;
            }
        }

        public void ProcessCategory(string url,string categoryId)
        {
            var link = string.Format(url, 1);
            HttpClient client = new HttpClient(link);
            client.Timeout = 1000000;
            client.StatusUpdate += new EventHandler<StatusUpdateEventArgs>(Message);
            var html = client.Request();
            

            //通过第一页找到一共有多少页面
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            //var productNodes2 = htmlDocument.GetElementbyId("plist").SelectNodes("//a[@href]");

            var maxPager = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='pagin fr']/a[4]");

            if (maxPager != null)
            {
                int maxPageNumber = 0;
                if (int.TryParse(maxPager.InnerText, out maxPageNumber) && maxPageNumber > 0)
                {
                    for (int i = 1; i <= maxPageNumber; i++)
                    {
                        string newUrl = string.Format(url, i);
                        HttpClient hc = new HttpClient(newUrl);
                        hc.Timeout = 1000000;
                        hc.StatusUpdate += new EventHandler<StatusUpdateEventArgs>(Message);
                        var newHtml = hc.Request();
                        var doc = HtmlAgilityPackHelper.GetHtmlDocument(newHtml);

                        var productContainer = doc.GetElementbyId("plist");
                        if (productContainer == null)
                        {
                            //Logger.Log(url+"数据获取有误");
                            continue;
                        }
                        var productNodes = productContainer.CssSelect("div.p-name>a");//productContainer.SelectNodes("//a");
                        if (productNodes == null || productNodes.Count() == 0)
                        {
                            //Logger.Log(url + "数据获取有误");
                            continue;
                        }
                        foreach (var productNode in productNodes)
                        {
                            if (productNode.Attributes["href"] != null && productNode.Attributes["href"].Value != null && productNode.Attributes["href"].Value.Contains("product/"))
                            {

                                var href = productNode.Attributes["href"].Value;
                                if (!IsExistUrl(href))
                                {
                                    InsertProduct(productNode.InnerText, href, int.Parse(categoryId), 0);
                                }
                                
                                
                            }

                        }

                        Thread.Sleep(5000);
                    }
                }
            }
        }

        public void ProcessProduct(object d)
        {
            //var dt = GetProductCategory();
            var p = d as SpiderParameter;
            if (p != null)
            {
                var dt = p.DT as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var url = Convert.ToString(dr["Url"]);
                        var categoryId = Convert.ToString(dr["cid"]);
                        if (!string.IsNullOrEmpty(url) && url.Contains("products/") && !url.EndsWith("000.html"))
                        {
                            var categoryUrl = "http://www.360buy.com/" +
                                              url.Replace(".html", "-0-0-0-0-0-0-0-1-1-{0}.html");

                            ProcessCategory(categoryUrl, categoryId);
                        }
                    }

                }
            }
            //// 组织url数据
            //var categoryUrl = link.Attributes["href"].Value.Replace(".html", "-0-0-0-0-0-0-0-1-1-{0}.html");

            //// 查找第一页面 确定最大页面数
            //ProcessCategory(categoryUrl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GatherAll();
            //ProcessProduct();
            DoWithParameter();
            
            //Test();

            //MulityThreadGatherProduct();
            //TestWebbrower();

        }
        bool loading = true;    // 该变量表示网页是否正在加载.
        private void TestWebbrower()
        {
            
            string html = string.Empty;

            var urls=new List<string>();
            urls.Add("http://www.baidu.com/");
            urls.Add("http://www.sohu.com/");
            urls.Add("http://jd2008.360buy.com/jdhome/orderinfo.aspx?orderid=311585738&PassKey=0F6957ED08310AD9AB6C0CFF317363F3");

            

            foreach (string url in urls)
            {
                using (ExtendedWebBrowser wwb = new ExtendedWebBrowser())
                {
                    loading = true; // 表示正在加载
                    wwb.DocumentCompleted += wwb_DocumentCompleted;
                    wwb.Navigate(url);
                    while (loading)
                    {
                        Application.DoEvents(); // 等待本次加载完毕才执行下次循环.
                    }
                }
            }
            
        }

        /// <summary>
        /// 脚本错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scriptWindow_Error(object sender, HtmlElementErrorEventArgs e)
        {
            //var wb = sender as ExtendedWebBrowser;
            // We got a script error, record it
            ScriptErrorManager.Instance.RegisterScriptError(e.Url,e.Description, e.LineNumber);
            // Let the browser know we handled this error.
            e.Handled = true;
        }

        private void wwb_DownloadComplete(object sender, EventArgs e)
        {
            // 页面完全载入
            var ewb = sender as ExtendedWebBrowser;
            // Check wheter the document is available (it should be)
            if (ewb!=null&&ewb.Document != null)
            {
                // Subscribe to the Error event
                //ewb.Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
                //ewb.DownloadComplete -= new EventHandler(wwb_DownloadComplete);
            }
        }

        private void wwb_DocumentCompleted(object sender, BrowserExtendedNavigatingEventArgs e)
        {
            // 页面完全载入
            var ewb = sender as ExtendedWebBrowser;

            if (ewb != null && ewb.Document != null)
            {
                // Subscribe to the Error event
                ewb.Document.Window.Error += new HtmlElementErrorEventHandler(scriptWindow_Error);
            }

            if (ewb!=null && e.AutomationObject == ((ExtendedWebBrowser)sender).Application)
            {
                if (ewb.Document != null)
                {
                    GatherPriceV2(ewb.BusinessData, e.Url.ToString(), ewb.Document.Body.OuterHtml);
                    //using(StreamReader sr = new StreamReader(ewb.DocumentStream, Encoding.GetEncoding(ewb.Document.Encoding)))
                    //{
                    //    // 处理业务
                    //    var html = sr.ReadToEnd();

                    //    //hc.documentElement.innerHTML;
                        
                    //}
                    ewb.DocumentCompleted -= wwb_DocumentCompleted;
                    //ewb.Dispose();
                }
                
            }
            //Loading[ewb.ThreadId] = false;
        }

        


        private void Insert(string categoryId,string name,string url,string parentCategoryId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProductCategory"))
            {
                cmd.CommandText=cmd.CommandText.Replace("@CategoryId", categoryId).Replace("@Name", name).Replace("@Url", url).Replace("@ParentCategoryId", parentCategoryId);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertProduct(string name,string url,int categoryId,int commentNumber)
        {
             using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProduct"))
             {
                 cmd.SetParameterValue("@ProductId", PrimaryKeyGenerator.NewComb().ToString().Replace("-",""));
                 cmd.SetParameterValue("@Name", name);
                 cmd.SetParameterValue("@Url", url);
                 cmd.SetParameterValue("@CategoryId", categoryId);
                 cmd.SetParameterValue("@Supplier", 1);
                 //cmd.SetParameterValue("@Inventory", );
                 cmd.SetParameterValue("@CommentNumber", commentNumber);
                 cmd.SetParameterValue("@InDate", DateTime.Now);
                 cmd.SetParameterValue("@EditDate", DateTime.Now);
                 cmd.ExecuteNonQuery();
             }

        }

        private DataTable GetProductCategory(string sqlWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductCategory"))
            {
                if(!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText + " and " + sqlWhere;
                }
                return cmd.ExecuteDataTable();
            }
        }

        private DataTable GetProduct(string sqlWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                if(!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText += sqlWhere;
                }
                return cmd.ExecuteDataTable();
            }
        }

        public void UpdateProduct(string productId,string name,decimal price)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateProduct"))
            {
                cmd.SetParameterValue("@ProductId", productId);
                cmd.SetParameterValue("@Name", name);
                cmd.SetParameterValue("@Price", price);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetProductsCount()
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductsCount"))
            {
                return Convert.ToInt32(cmd.ExecuteScalar()) ;
            }
        }

        private void ProcessProductInfo()
        {
            var dt = GetProduct("order by indate desc limit 0,100");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var url = Convert.ToString(dr["Url"]);
                    var productId = Convert.ToString(dr["CategoryId"]);
                    this.GatherPrice(productId,url);
                }
            }
        }

        private void MulityThreadGatherProduct()
        {

            var dt1 = this.GetProductCategory(" (cid  between 387 and 487)");
            var dt2 = this.GetProductCategory(" (cid  between 488 and 587)");
            var dt3 = this.GetProductCategory(" (cid  between 588 and 687)");

            Thread t1 = new Thread(new ParameterizedThreadStart(this.ProcessProduct));
            Thread t2 = new Thread(new ParameterizedThreadStart(this.ProcessProduct));
            Thread t3 = new Thread(new ParameterizedThreadStart(this.ProcessProduct));
            t1.Start(new SpiderParameter { DT = dt1 });
            t2.Start(new SpiderParameter { DT = dt2 });
            t3.Start(new SpiderParameter { DT = dt3 });
        }

        public static Dictionary<int, bool> Loading { get; set; } 

        private void DoWithParameter()
        {
            //Loading=new Dictionary<int, bool>();
            //Loading.Add(1,true);
            //Loading.Add(2, true);
            //Loading.Add(3, true);
            //Loading.Add(4, true);

            var recordCount = GetProductsCount();

            var dt1 = GetProduct(" limit 0," + recordCount / 4);
            //var dt2 = GetProduct(" limit " + recordCount / 4 + "," + recordCount / 2);

            //var dt3 = GetProduct(" limit " + recordCount / 2 + "," + recordCount / 2 + recordCount / 4);
            //var dt4 = GetProduct(" limit " + recordCount / 2 + recordCount / 4 + "," + recordCount);
            //DoSomethingWithParameterV2(new SpiderParameter { DT = dt1, ThreadId = 1 });
            //return;
            Thread t1 = new Thread(new ParameterizedThreadStart(this.DoSomethingWithParameterV2));
            //Thread t2 = new Thread(new ParameterizedThreadStart(this.DoSomethingWithParameterV2));
            //Thread t3 = new Thread(new ParameterizedThreadStart(this.DoSomethingWithParameterV2));
            //Thread t4 = new Thread(new ParameterizedThreadStart(this.DoSomethingWithParameterV2));
            t1.SetApartmentState(ApartmentState.STA);
            //t2.SetApartmentState(ApartmentState.STA);
            //t3.SetApartmentState(ApartmentState.STA);
            //t4.SetApartmentState(ApartmentState.STA);
            t1.Start(new SpiderParameter { DT = dt1, ThreadId = 1 });
            //t2.Start(new SpiderParameter { DT = dt2, ThreadId = 2 });
            //t3.Start(new SpiderParameter { DT = dt3, ThreadId = 3 });
            //t4.Start(new SpiderParameter { DT = dt4, ThreadId = 4 });

        }
        private void DoSomethingWithParameter(object x)
        {
            var parameter = x as SpiderParameter;
            if (parameter!=null)
            {
                foreach (DataRow dr in parameter.DT.Rows)
                {

                    HTMLDocumentClass hc = new HTMLDocumentClass();
                    //hc.designMode = "on";//这一句
                    IHTMLDocument2 doc2 = hc;
                    doc2.write("");
                    doc2.close();
                    IHTMLDocument4 doc4 = hc;
                    
                
                    var rowId = Convert.ToInt32(dr["RowId"]);
                    var url = Convert.ToString(dr["Url"]);
                    var productId = Convert.ToString(dr["ProductId"]);

                    IHTMLDocument2 doc = doc4.createDocumentFromUrl(url, "null");
                    int start = Environment.TickCount;
                    while (doc.readyState != "complete")
                    {
                        if (Environment.TickCount - start > 1000000)
                        {
                            throw new Exception("The document timed out while loading");
                        }
                    }
                    //hc.documentElement.innerHTML;
                    GatherPriceV2(productId, url, doc.body.outerHTML);
                    doc.clear();
                    doc.close();
                }
            }
        }

        private static readonly object LockHelper = new object();

        private void wwb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var wb = sender as WebBrowser;
            if(wb!=null)
            {
                IHTMLDocument2 vDocument = (IHTMLDocument2)wb.Document.DomDocument;
                foreach (IHTMLElement vElement in vDocument.all)
                {
                    if (vElement.tagName.ToUpper() == "FRAME")
                    {
                        IHTMLFrameBase2 vFrameBase2 = vElement as IHTMLFrameBase2;
                        if (vFrameBase2 != null)
                        {
                            vFrameBase2.contentWindow.execScript("function alert(str){confirm('[' + str + ']');}", "javaScript");
                        }
                    }
                }
            }
        }

        //private void wb_NavigateError(object sender, WebBrowserNavigateErrorEventArgs e)
        //{
            
        //    // Display an error message to the user.
        //    //MessageBox.Show("Cannot navigate to " + e.Url);
        //}

        private void wb_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            //var wb = sender as WebBrowser;
            //if(wb!=null)
            //{
            //    Uri uri = new Uri(((WebBrowser)sender).StatusText);
            //    wb.Url = uri;
            //}
        }

        private bool done = false;
        private void DoSomethingWithParameterV2(object x)
        {
            var parameter = x as SpiderParameter;
            if (parameter != null)
            {
                foreach (DataRow dr in parameter.DT.Rows)
                {
                    var rowId = Convert.ToInt32(dr["RowId"]);
                    var url = Convert.ToString(dr["Url"]);
                    var productId = Convert.ToString(dr["ProductId"]);

                    //lock(LockHelper)
                    //{
                        #region
                        //using (Control c = new Control())
                        //{

                        //    ExtendedWebBrowser wwb = new ExtendedWebBrowser();
                        //    wwb.ScriptErrorsSuppressed = true;
                        //    wwb.IsWebBrowserContextMenuEnabled = false;
                        //    wwb.AllowWebBrowserDrop = false;
                        //    wwb.ScrollBarsEnabled = false;
                        //    c.Controls.Add(wwb);

                        //    wwb.BusinessData = productId;
                        //    wwb.ThreadId = parameter.ThreadId;
                        //    //Loading[parameter.ThreadId] = true; // 表示正在加载
                        //    //wwb.DownloadComplete += new EventHandler(wwb_DownloadComplete);
                        //    //wwb.Navigated += wwb_Navigated;
                        //    wwb.NewWindow += wb_NewWindow;
                        //    wwb.DocumentCompleted += wwb_DocumentCompleted;
                            
                        //    wwb.NavigateError += new ExtendedWebBrowser.WebBrowserNavigateErrorEventHandler(wb_NavigateError);
                        //    wwb.DownloadControlFlags = (int)WebBrowserDownloadControlFlags.DOWNLOADONLY;
                        //    wwb.Navigate(url);
                        //    //var tempLoading = Loading[parameter.ThreadId];
                        //    // !wwb.IsDisposed &&
                        //    while ( wwb.ReadyState != WebBrowserReadyState.Complete)
                        //    {
                        //        Application.DoEvents(); // 等待本次加载完毕才执行下次循环.
                        //    }
                        //    if (wwb.ActiveXInstance != null)
                        //    {
                        //        Marshal.ReleaseComObject(wwb.ActiveXInstance);
                        //    }

                        //    wwb.Dispose();
                        //    wwb = null;
                        //}
                        #endregion
                    using (csExWB.cEXWB pWb = new cEXWB())
                    {
                        //pWb.Size = new System.Drawing.Size(800, 600);
                        pWb.WBDOCDOWNLOADCTLFLAG = (int)
                            (DOCDOWNLOADCTLFLAG.NO_SCRIPTS |
                             DOCDOWNLOADCTLFLAG.NO_DLACTIVEXCTLS |
                             DOCDOWNLOADCTLFLAG.NO_JAVA |
                             DOCDOWNLOADCTLFLAG.NO_RUNACTIVEXCTLS |
                             DOCDOWNLOADCTLFLAG.PRAGMA_NO_CACHE |
                             DOCDOWNLOADCTLFLAG.SILENT);
                        pWb.DownloadSounds = false;
                        pWb.DownloadVideo = false;
                        pWb.DownloadActiveX = false;
                        pWb.DownloadImages = false;
                        pWb.DownloadScripts = true;

                        pWb.DocumentComplete += new DocumentCompleteEventHandler(pWb_DocumentComplete);
                        pWb.NavigateError += new NavigateErrorEventHandler(pWb_NavigateError);
                        pWb.WBSecurityProblem += new SecurityProblemEventHandler(pWb_WBSecurityProblem);
                        pWb.BeforeNavigate2 += new BeforeNavigate2EventHandler(pWb_BeforeNavigate2);
                        //pWb.DisableScriptDebugger();
                        pWb.NavToBlank();

                        pWb.Navigate(url);
                        while (!done)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(100);
                        }
                        GatherPriceV2(productId, url, pWb.DocumentSource);
                        //pWb.Dispose();
                        done = false;
                    }
                        
                    //}
                }
            }
        }

        #region
        void pWb_BeforeNavigate2(object sender, BeforeNavigate2EventArgs e)
        {
            if (label1.InvokeRequired)
            {
                D d = new D(DelegateSetValue);
                label1.Invoke(d, e.url);

            }
            else
            {
                this.label1.Text = e.url;
            }
            Debug.Print("BeforeNavigate2==>" + e.url + " =istoplevel===> " + e.istoplevel.ToString());
        }

         void pWb_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            if (e.url.ToLower() == "about:blank")
            {
                Debug.Print("DocumentComplete::about:blank = istoplevel===>" + e.istoplevel.ToString());
                return;
            }
            if (e.istoplevel)
            {
                done = true;
                Debug.Print("DocumentComplete::TopLevel is TRUE===>" + e.url);
            }
            else
            {
                Debug.Print("DocumentComplete::TopLevel is FALSE===>" + e.url);
            }
        }

        void pWb_WBSecurityProblem(object sender, SecurityProblemEventArgs e)
        {
            Debug.Print("WBSecurityProblem==>" + e.problem.ToString());
        }

        void pWb_NavigateError(object sender, NavigateErrorEventArgs e)
        {
            Debug.Print("NavigateError==>" + e.statuscode.ToString());
            done = true;
        }
        #endregion

        public void Test()
        {
            HTMLDocumentClass hc = new HTMLDocumentClass();
            //hc.designMode = "on";//这一句
            IHTMLDocument2 doc2 = hc;
            doc2.write("");
            doc2.close();
            IHTMLDocument4 doc4 = hc;
        //http://social.msdn.microsoft.com/Forums/zh-CN/ieextensiondevelopment/thread/d2ce2000-580d-452a-950f-e29fcd11a35f
            //http://social.msdn.microsoft.com/Forums/zh-CN/ieextensiondevelopment/thread/d2ce2000-580d-452a-950f-e29fcd11a35f

            //https://code.google.com/p/csexwb2/
            //TryAllowCookies(new Uri("http://www.baidu.com"));
            //InternetSecurityManager.TryAllowCookies(new Uri("http://www.baidu.com"));
            IHTMLDocument2 doc = doc4.createDocumentFromUrl("http://www.baidu.com", "null");
            //IInternetSecurityManager 
            //var str= InternetSecurityManager.GetUrlZone("http://www.baidu.com");

            //this.ProcessUrlAction(
            //    "http://www.baidu.com", InteropConsts.URLACTION.COOKIES_ENABLED, URLPOLICY.ALLOW, , ,0x00000001 , 0);
            MapUrlToZone(new Uri("http://www.baidu.com"));
            //InternetSecurityManager.GetUrlZone("http://www.baidu.com");
            
            int start = Environment.TickCount;
            while (doc.readyState != "complete")
            {
                Application.DoEvents();
                if (Environment.TickCount - start > 1000000)
                {
                    throw new Exception("The document timed out while loading");
                }
            }
            //hc.documentElement.innerHTML;
            doc.clear();
            doc.close();
        }

        public class SpiderParameter
        {
            public string Url { get; set; }

            public string ProductId { get; set; }

            public DataTable DT { get; set; }

            public Label Label { get; set; }

            public int ThreadId { get; set; }
        }

        [ComImport, Guid("7b8a2d94-0ac9-11d1-896c-00c04Fb6bfc4")]
        private class InternetSecurityManager
        {
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("79eac9ee-baf9-11ce-8c82-00aa004ba90b")]
        private interface IInternetSecurityManager
        {
            void Unused1();
            void Unused2();
            [PreserveSig]
            int MapUrlToZone([In, MarshalAs(UnmanagedType.BStr)] string pwszUrl, out int pdwZone, [In] int dwFlags);
            void Unused3();
            [PreserveSig]
            int ProcessUrlAction(string pwszUrl, int dwAction, ref int pPolicy, int cbPolicy, ref Guid pContext, int cbContext, int dwFlags, int dwReserved);
            // left undefined
        }

        public static SecurityZone MapUrlToZone(Uri uri)
        {
            IInternetSecurityManager securityManager = (IInternetSecurityManager)new InternetSecurityManager();
            int zoneId;
            if (securityManager.MapUrlToZone(uri.ToString(), out zoneId, 0) < 0)
                return SecurityZone.NoZone;

            return (SecurityZone)zoneId;
        }

        private const int URLACTION_COOKIES = 0x00001A02;
        private const int URLACTION_COOKIES_ENABLED = 0x00001A10;
        private const int URLPOLICY_ALLOW = 0x00;
        private const int URLPOLICY_DISALLOW = 0x03;
        private const int PUAF_DEFAULT = 0x00000000;


        private const int URLACTION_COOKIES_SESSION = 0x00001A03;

        private const int URLACTION_CLIENT_CERT_PROMPT = 0x00001A04;

        private const int URLACTION_COOKIES_THIRD_PARTY = 0x00001A05;

        private const int URLACTION_COOKIES_SESSION_THIRD_PARTY = 0x00001A06;


        public static bool TryAllowCookies(Uri uri)
        {
            IInternetSecurityManager securityManager = (IInternetSecurityManager)new InternetSecurityManager();
            int policy = 0;
            Guid context = Guid.Empty;
            int hr = securityManager.ProcessUrlAction(uri.ToString(), URLACTION_COOKIES, ref policy, Marshal.SizeOf(policy), ref context, Marshal.SizeOf(context), (int)PUAF.PUAF_NOUI, 0);
            return (hr == 0) && policy == URLPOLICY_ALLOW;
        }

        enum PUAF
        {
            PUAF_DEFAULT = 0x0000000,
            PUAF_NOUI = 0x00000001,
            PUAF_ISFILE = 0x00000002,
            PUAF_WARN_IF_DENIED = 0x00000004,
            PUAF_FORCEUI_FOREGROUND = 0x00000008,
            PUAF_CHECK_TIFS = 0x00000010,
            PUAF_DONTCHECKBOXINDIALOG = 0x00000020,
            PUAF_TRUSTED = 0x00000040,
            PUAF_ACCEPT_WILDCARD_SCHEME = 0x00000080,
            PUAF_ENFORCERESTRICTED = 0x00000100,
            PUAF_NOSAVEDFILECHECK = 0x00000200,
            PUAF_REQUIRESAVEDFILECHECK = 0x00000400,
            PUAF_DONT_USE_CACHE = 0x00001000,
            PUAF_LMZ_UNLOCKED = 0x00010000,
            PUAF_LMZ_LOCKED = 0x00020000,
            PUAF_DEFAULTZONEPOL = 0x00040000,
            PUAF_NPL_USE_LOCKED_IF_RESTRICTED = 0x00080000,
            PUAF_NOUIIFLOCKED = 0x00100000,
            PUAF_DRAGPROTOCOLCHECK = 0x00200000
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);
            if (label1.InvokeRequired)
            {
                D d = new D(DelegateSetValue);
                label1.Invoke(d, "释放内存");

            }
            else
            {
                this.label1.Text = "释放内存";
            }
        }

        //#region IInternetSecurityManager members

        //int IInternetSecurityManager.SetSecuritySite(IntPtr pSite)
        //{
        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //int IInternetSecurityManager.GetSecuritySite(out IntPtr pSite)
        //{
        //    pSite = IntPtr.Zero;
        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //int IInternetSecurityManager.MapUrlToZone(string pwszUrl, out uint pdwZone, uint dwFlags)
        //{
        //    pdwZone = (uint)tagURLZONE.URLZONE_LOCAL_MACHINE;
        //    return HResults.S_OK;
        //}

        //const int MAX_SIZE_SECURITY_ID = 512;
        //private byte[] m_SecurityID;

        //int IInternetSecurityManager.GetSecurityId(string pwszUrl, IntPtr pbSecurityId, ref uint pcbSecurityId, ref uint dwReserved)
        //{
        //    if (pcbSecurityId >= MAX_SIZE_SECURITY_ID)
        //    {
        //        Marshal.Copy(m_SecurityID, 0, pbSecurityId, MAX_SIZE_SECURITY_ID);

        //        pcbSecurityId = 9;

        //        return HResults.S_OK;
        //    }

        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //public int ProcessUrlAction(string pwszUrl, uint dwAction, IntPtr pPolicy, uint cbPolicy, IntPtr pContext, uint cbContext, uint dwFlags, uint dwReserved)
        //{
        //    try
        //    {
        //        URLACTION action = (URLACTION)dwAction;

        //        bool hasUrlPolicy = (cbPolicy >= unchecked((uint)Marshal.SizeOf(typeof(int))));
        //        URLPOLICY urlPolicy = (hasUrlPolicy) ? (URLPOLICY)Marshal.ReadInt32(pPolicy) : URLPOLICY.ALLOW;

        //        bool hasContext = (cbContext >= unchecked((uint)Marshal.SizeOf(typeof(Guid))));
        //        Guid context = (hasContext) ? (Guid)Marshal.PtrToStructure(pContext, typeof(Guid)) : Guid.Empty;

        //        if (hasUrlPolicy)
        //        {
        //            if ((action >= URLACTION.ACTIVEX_MIN) && (action <= URLACTION.ACTIVEX_MAX))
        //            {
        //                switch (action)
        //                {
        //                    case URLACTION.ACTIVEX_RUN:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_OVERRIDE_OBJECT_SAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_OVERRIDE_DATA_SAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_OVERRIDE_SCRIPT_SAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.SCRIPT_OVERRIDE_SAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_CONFIRM_NOOBJECTSAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_TREATASUNTRUSTED:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }

        //                    case URLACTION.ACTIVEX_NO_WEBOC_SCRIPT:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            break;
        //                        }
        //                }
        //            }
        //            else if ((action >= URLACTION.SCRIPT_MIN) && (action <= URLACTION.SCRIPT_MAX))
        //            {
        //                switch (action)
        //                {
        //                    case URLACTION.SCRIPT_RUN:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }

        //                    case URLACTION.SCRIPT_SAFE_ACTIVEX:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }

        //                    case URLACTION.SCRIPT_PASTE:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }

        //                    case URLACTION.SCRIPT_OVERRIDE_SAFETY:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }

        //                    case URLACTION.CROSS_DOMAIN_DATA:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }
        //                }
        //            }
        //            else if ((action >= URLACTION.HTML_MIN) && (action <= URLACTION.HTML_MAX))
        //            {
        //                switch (action)
        //                {
        //                    case URLACTION.HTML_SUBFRAME_NAVIGATE:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }

        //                    case URLACTION.HTML_INCLUDE_FILE_PATH:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }
        //                }
        //            }
        //            else if ((action >= URLACTION.BEHAVIOR_MIN) && (action <= URLACTION.BEHAVIOR_MIN + 0x0FF))
        //            {
        //                switch (action)
        //                {
        //                    case URLACTION.BEHAVIOR_RUN:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }
        //                }
        //            }
        //            else if ((action >= URLACTION.FEATURE_MIN) && (action <= URLACTION.FEATURE_MIN + 0x0FF))
        //            {
        //                switch (action)
        //                {
        //                    case URLACTION.FEATURE_DATA_BINDING:
        //                        {
        //                            urlPolicy = URLPOLICY.ALLOW;
        //                            Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                            return HResults.S_OK;
        //                        }
        //                }
        //            }
        //            else if (action == URLACTION.INPRIVATE_BLOCKING)
        //            {
        //                urlPolicy = URLPOLICY.ALLOW;
        //                Marshal.WriteInt32(pPolicy, (int)urlPolicy);
        //                return HResults.S_OK;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //    }

        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //int IInternetSecurityManager.QueryCustomPolicy(string pwszUrl, ref Guid guidKey, out IntPtr ppPolicy, out uint pcbPolicy, IntPtr pContext, uint cbContext, uint dwReserved)
        //{
        //    ppPolicy = IntPtr.Zero;
        //    pcbPolicy = 0;
        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //int IInternetSecurityManager.SetZoneMapping(uint dwZone, string lpszPattern, uint dwFlags)
        //{
        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //int IInternetSecurityManager.GetZoneMappings(uint dwZone, out IEnumString ppenumString, uint dwFlags)
        //{
        //    ppenumString = null;
        //    return (int)WinInetErrors.INET_E_DEFAULT_ACTION;
        //}

        //#endregion
    }

    //http://stackoverflow.com/questions/6284997/when-deploying-net-applications-how-do-you-find-out-what-zone-a-share-is-in-r
    //http://stackoverflow.com/questions/4278821/persistent-cookies-in-wpf-webbrowser-control
    //http://msdn.microsoft.com/en-us/library/ms537110%28v=vs.85%29.aspx
    //http://msdn.microsoft.com/en-us/library/ms537179.aspx
    //http://topic.csdn.net/t/20060111/23/4511754.html
    //public static class InternetSecurityManager
    //{
    //    private static Guid _CLSID_SecurityManager = new Guid("7b8a2d94-0ac9-11d1-896c-00c04fb6bfc4");
    //    private static string[] ZoneNames = new[] { "Local", "Intranet", "Trusted", "Internet", "Restricted" };

    //    private const int URLACTION_COOKIES = 0x00001A02;
    //    private const int URLACTION_COOKIES_ENABLED = 0x00001A10;
    //    private const int URLPOLICY_ALLOW = 0x00;
    //    private const int URLPOLICY_DISALLOW = 0x03;
    //    private const int PUAF_DEFAULT = 0x00000000;


    //    public static bool TryAllowCookies(Uri uri)
    //    {
    //        Type t = System.Type.GetTypeFromCLSID(_CLSID_SecurityManager);
    //        IInternetSecurityManager securityManager = (IInternetSecurityManager)System.Activator.CreateInstance(t);
    //        byte policy = 0;
    //        byte pContext = 0;
    //        Guid context = Guid.Empty;
    //        int hr = securityManager.ProcessUrlAction(uri.ToString(), URLACTION_COOKIES_ENABLED, out policy, (uint)Marshal.SizeOf(policy), pContext, (uint)Marshal.SizeOf(context), PUAF_DEFAULT, 0);
    //        return (hr == 0) && policy == URLPOLICY_ALLOW;
    //    }


    //    public static string GetUrlZone(string url)
    //    {
    //        Type t = System.Type.GetTypeFromCLSID(_CLSID_SecurityManager);
    //        IInternetSecurityManager securityManager = (IInternetSecurityManager)System.Activator.CreateInstance(t);
    //        try
    //        {
    //            uint zone = 0;
    //            int hResult = securityManager.MapUrlToZone(url, ref zone, 0);
    //            if (hResult != 0)
    //                throw new COMException("Error calling MapUrlToZone, HRESULT = " + hResult.ToString("x"), hResult);

    //            if (zone < ZoneNames.Length)
    //                return ZoneNames[zone];
    //            return "Unknown - " + zone;
    //        }
    //        finally
    //        {
    //            Marshal.ReleaseComObject(securityManager);
    //        }
    //    }
    //}

    //[ComImport, GuidAttribute("79EAC9EE-BAF9-11CE-8C82-00AA004BA90B")]
    //[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    //public interface IInternetSecurityManager
    //{
    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int SetSecuritySite([In] IntPtr pSite);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int GetSecuritySite([Out] IntPtr pSite);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int MapUrlToZone([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
    //             ref UInt32 pdwZone, UInt32 dwFlags);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int GetSecurityId([MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
    //              [MarshalAs(UnmanagedType.LPArray)] byte[] pbSecurityId,
    //              ref UInt32 pcbSecurityId, uint dwReserved);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int ProcessUrlAction([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
    //             UInt32 dwAction, out byte pPolicy, UInt32 cbPolicy,
    //             byte pContext, UInt32 cbContext, UInt32 dwFlags,
    //             UInt32 dwReserved);

    //    //[PreserveSig]
    //    //int ProcessUrlAction(string pwszUrl, int dwAction, ref int pPolicy, int cbPolicy, ref Guid pContext, int cbContext, int dwFlags, int dwReserved);


    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int QueryCustomPolicy([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
    //              ref Guid guidKey, ref byte ppPolicy, ref UInt32 pcbPolicy,
    //              ref byte pContext, UInt32 cbContext, UInt32 dwReserved);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int SetZoneMapping(UInt32 dwZone,
    //               [In, MarshalAs(UnmanagedType.LPWStr)] string lpszPattern,
    //               UInt32 dwFlags);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int GetZoneMappings(UInt32 dwZone, out UCOMIEnumString ppenumString,
    //            UInt32 dwFlags);
    //}




}
