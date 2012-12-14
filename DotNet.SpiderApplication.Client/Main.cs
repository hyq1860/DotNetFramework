using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DotNet.Common.Core;
using DotNet.Common.Logging;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;
using DotNet.SpiderApplication.Contract.WCF;
using DotNet.SpiderApplication.Service.Implemention.Service;
using IfacesEnumsStructsClasses;
using csExWB;
using HtmlAgilityPack;

namespace DotNet.SpiderApplication.Client
{
    using Amib.Threading;

    using DotNet.Base.Service;
    using DotNet.BasicSpider;
    using DotNet.Common;
    using DotNet.Common.Configuration;
    using DotNet.Common.Utility;
    using DotNet.Data;
    using DotNet.IoC;
    using DotNet.SpiderApplication.Service;
    using DotNet.Web.Http;

    using ScrapySharp.Extensions;

    using ICalculator = DotNet.SpiderApplication.Contract.ICalculator;

    class MyProcess : Process
    {
        public void Stop()
        {
            this.CloseMainWindow();
            this.Close();
            OnExited();
        }
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class Main : Form, IServerToClient
    {
        private static void myProcess_HasExited(object sender, System.EventArgs e)
        {
            MessageBox.Show("Process has exited.");
        }
        private ServiceHost host;
        ServiceHost _serverHost;
        private void SpiderWCFNetPipe()
        {
            string l_serviceAddress = "net.pipe://127.0.0.1";
            host = new ServiceHost(typeof(SpiderServer), new Uri[] { new Uri(l_serviceAddress) });
            host.AddServiceEndpoint(typeof(ISpiderServer), new NetNamedPipeBinding(), "GetSpiderTask");
            host.Open();
        }

        private void Report()
        {
            _serverHost = new ServiceHost(this);
            _serverHost.AddServiceEndpoint((typeof(IServerToClient)), new NetNamedPipeBinding(), "net.pipe://127.0.0.1/Server");
            _serverHost.Open();
        }

        public Main()
        {
            InitializeComponent();
            //Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
            //skin.SkinFile = System.Environment.CurrentDirectory + "\\skins\\" + "DeepCyan.ssk";
            //skin.Active = true;
            //Init();
            //AutoUpdaterHelper.AutoUpdater();

            //var productService = CommonBootStrapper.ServiceLocator.GetInstance<IProductService>();
            //var data = productService.GetProducts(string.Empty);

            // csexwb组建注册
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();
            //WebBrowerManager.Instance.Register();
            //WebBrowerManager.Instance.RegisterCsExwb();
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();
            //WebBrowerManager.Instance.UnregisterCsExwb();
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();

            //WebBrowerManager.Instance.Setup(new cEXWB());
            //WebBrowerManager.Instance.TimeOut = 15;
            //var states = new List<string>();
            //states.Add("http://www.360buy.com/product/222701.html");
            //states.Add("http://www.360buy.com/product/682747.html");
            //states.Add("http://www.360buy.com/product/222704.html");
            //states.Add("http://www.360buy.com/product/222701.html");
            //states.Add("http://www.360buy.com/product/354444.html");
            //states.Add("http://www.360buy.com/product/352655.html");
            //states.Add("http://www.360buy.com/product/481284.html");
            //states.Add("http://www.360buy.com/product/563181.html");
            //states.Add("http://www.360buy.com/product/481245.html");
            //states.Add("http://www.360buy.com/product/673975.html");
            //int i = 0;
            //foreach (var state1 in states)
            //{
            //    var html1 = WebBrowerManager.Instance.Run(state1);
            //    File.WriteAllText("z:\\"+i+".html",html1,Encoding.UTF8);
            //    i++;
            //}
            
            //MessageBox.Show(WebBrowerManager.Instance.IEVersion);
            //Console.Read();

            //var data = CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().GetProducts(" where Supplier=1 limit 0,10");

            //var jsonData = data.ToJson();
            SpiderWCFNetPipe();
            Report();
            MyProcess p = new MyProcess();
            p.StartInfo.FileName = Environment.CurrentDirectory + "\\SpiderInstance\\" + "DotNet.SpiderApplication.WebBrowerInstance.exe";
            p.EnableRaisingEvents = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.ErrorDialog = true;
            //p.StartInfo.Arguments = jsonData;
            p.Exited += new EventHandler(myProcess_HasExited);
            //p.Start();
            //p.WaitForInputIdle();
            //p.Stop();
            Thread.Sleep(100);
            var p1 = SingletonProvider<ProcessWatcher>.UniqueInstance;
            //p1.StartWatch();
            return;

            //MessageBox.Show(WebBrowerManager.Instance.IEVersion);
            //foreach (ProductInfo productInfo in data)
            //{
            //    //using (var webbrower = new cEXWB())
            //    //{
            //        //WebBrowerManager.Instance.Setup(webbrower);
            //        //WebBrowerManager.Instance.TimeOut = 15;
            //        var ver = SpiderManager.SpiderProductDetail(new SpiderProductInfo() { ECPlatformId = productInfo.ECPlatformId, Url = productInfo.Url, ProductId = productInfo.ProductId });
            //        CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().Update(ver);
            //        //MessageBox.Show(WebBrowerManager.Instance.IEVersion);
            //        WebBrowerManager.Instance.Clear();
            //    //}
                
            //}

            return;

            // 亚马逊
            //Spider.AmazonSpider(string.Empty);
            Spider.AmazonProductList("http://www.amazon.cn/电脑及配件/b/ref=sd_allcat_pc_?ie=UTF8&node=888465051");
            return;

            // 图片价格识别
            ImageProcess.Recognize("http://jprice.360buyimg.com/price/gp1004750985-1-1-3.png");
            return;

            // csexwb组建注册
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();
            //WebBrowerManager.Instance.Register();
            //WebBrowerManager.Instance.RegisterCsExwb();
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();
            //WebBrowerManager.Instance.UnregisterCsExwb();
            //WebBrowerManager.Instance.CheckCsExwbIsRegistered();

            //一号店
            Spider.YiHaoDianSpider("http://www.yihaodian.com/product/listAll.do");
            //Spider.YiHaoDianList();


            //易讯
            //Spider.WuYiBuySpider("http://www.51buy.com/portal.html");
            //Spider.WuYiBuyProductList("http://list.51buy.com/308--------.html");
            Spider.WuYiBuyProductList("http://list.51buy.com/998-0-6-11-24-0-1-5191e20841-.html");
        
            return;

            //当当
            //Spider.DangDangSpider("http://category.dangdang.com/");

            var dt = DataAccess.GetProductCategory(" ECPlatformId=5 limit 48,100");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Url"))
                    {
                        var url = Convert.ToString(dr["Url"]);
                        Spider.DangDangProductList(url);
                    }
                }
            }


            // 苏宁
            //Spider.SuNingSpider("www.suning.com/emall/SNProductCatgroupView?storeId=10052&catalogId=10051&flag=1");

            

            //var dt=DataAccess.GetProductCategory(" ECPlatformId=4 limit 48,100");
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (!dr.IsNull("Url"))
            //        {
            //            var url = Convert.ToString(dr["Url"]);
            //            Spider.SuNingProductList(url);
            //        }
            //    }
            //}

            

            //淘宝
            //Spider.TaoBaoDetail();

            return;

            //Spider.IndexTest();

            WebBrowerManager.Instance.Setup(new cEXWB());
            var html = WebBrowerManager.Instance.Run("http://www.51buy.com/portal.html");


            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            var firstCategoryContainer = htmlDocument.DocumentNode.CssSelect("div.item");
            foreach (HtmlNode htmlNode in firstCategoryContainer)
            {
                var firstNCategoryNode = htmlNode.CssSelect("div.item_hd");
                firstNCategoryNode.CssSelectAncestors("h3 > a");
                var secondCategoryContainer = htmlNode.CssSelect("dl");

                foreach (HtmlNode node in secondCategoryContainer)
                {
                    var secondCategoryNode = node.CssSelect("dt");

                    var threeCategoryContainer = node.CssSelect("dd");

                    foreach (HtmlNode htmlNode1 in threeCategoryContainer)
                    {
                        var threeCategoryNodes = htmlNode1.CssSelect("a");
                        foreach (HtmlNode threeCategoryNode in threeCategoryNodes)
                        {

                        }
                    }
                }
            }
        }

        public static void HtmlParse(string html)
        {
            if (string.IsNullOrEmpty(html))
                return;
            MessageBox.Show(html);
        }
        private bool IsDocumentFinish;

        private DuplexChannelFactory<ICalculator> ChannelFactory;

        private CalculatorCallbackService CallbackService;

        private ICalculator Calculator;


        private void Main_Load(object sender, EventArgs e)
        {
            return;
            //TestSqlite();
            var dt = DataAccess.GetProductCategory(" ECPlatformId=4 limit 48,60");
            var urls = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Url"))
                    {
                        var url = Convert.ToString(dr["Url"]);
                        //Spider.SuNingProductList(url);
                        urls.Add(url);
                    }
                }
            }
            System.Net.ServicePointManager.DefaultConnectionLimit = 768;

            SmartThreadPool smartThreadPool = new SmartThreadPool(1000 * 1000, 10);

            foreach (string state in urls)
            {
                smartThreadPool.QueueWorkItem(new WorkItemCallback(Spider.SuNingProductList), state);
            }

            // Wait for the completion of all work items
            smartThreadPool.WaitForIdle();

            smartThreadPool.Shutdown();

            MessageBox.Show("采集成功");
        }

        private void TestSqlite()
        {
            var rd = new Random();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProductPrice"))
            {
                for (int i = 1; i < 100000; i++)
                {
                    cmd.SetParameterValue("@ProductId", PrimaryKeyGenerator.NewComb().ToString().Replace("-", ""));
                    cmd.SetParameterValue("@Name", "仅售255元，限时抢购！！抢购时间10.29-10.30 24：00");
                    cmd.SetParameterValue("@Price", rd.Next(1, 50000));
                    cmd.SetParameterValue("@InDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Init()
        {
            CallbackService = new CalculatorCallbackService();
            //CallbackService.Quit += new EventHandler(cb_Quit);
            CallbackService.Processed += cb_Process;

            InstanceContext callback = new InstanceContext(CallbackService);
            ChannelFactory = new DuplexChannelFactory<ICalculator>(callback, "calculatorservice");

            Calculator = ChannelFactory.CreateChannel();
            SessionUtility.Callback = CallbackService as ISessionCallback;
            SessionUtility.Channel = Calculator as ISessionManagement;
            //calculator.Add(1, 2);
            //请求服务器端数据
            SessionClientInfo clientInfo = new SessionClientInfo() { IPAddress = "127.0.0.1", HostName = "1", UserName = "1" };

            TimeSpan timeout;
            Calculator.StartSession(clientInfo, out timeout);

            //var client = new CalculatorClient(callback);
            //var d1 = client.GetProcessData("aa");
            var data = Calculator.GetProcessData("client1");
            if (data != null && data.Count > 0)
            {
                Thread t1 = new Thread(new ParameterizedThreadStart(DoSomethingWithParameterV2));
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start(data);
                SetMessage("客户端线程已启动");
            }
        }

        private void cb_Quit(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(DoSomethingWithParameterV2));

            t1.SetApartmentState(ApartmentState.STA);
            t1.Start(CalculatorCallbackService.Data);
        }

        private void cb_Process(object sender, EventArgs e)
        {
            //System.Diagnostics.Process tt = System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id);
            //tt.Kill();
            //System.Diagnostics.Process.Start(Application.ExecutablePath); 
            ////Application.Restart();
            /// 
            //ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
            //startInfo.FileName = Application.ExecutablePath;

            //Application.ExitThread();
            //Process.Start(startInfo);
            //Application.ExitThread();
            //Application.Exit();
            //System.Environment.Exit(0);

            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }

        /// <summary>
        /// 初始化浏览器组件
        /// </summary>
        /// <param name="wb"></param>
        private void SetupWebBrower(cEXWB wb)
        {
            //wb.WBDOCDOWNLOADCTLFLAG = (int)(
            //    DOCDOWNLOADCTLFLAG.NO_SCRIPTS | 
            //    DOCDOWNLOADCTLFLAG.NO_DLACTIVEXCTLS |
            //    DOCDOWNLOADCTLFLAG.NO_JAVA | 
            //    DOCDOWNLOADCTLFLAG.NO_RUNACTIVEXCTLS | 
            //    DOCDOWNLOADCTLFLAG.PRAGMA_NO_CACHE | 
            //    DOCDOWNLOADCTLFLAG.SILENT);
            wb.DownloadSounds = false;
            wb.DownloadVideo = false;
            wb.DownloadActiveX = false;
            wb.DownloadFrames = false;
            wb.DownloadImages = false;
            wb.DownloadScripts = true;
            wb.Border3DEnabled = false;
            wb.ScrollBarsEnabled = false;
            wb.Silent = false;//whether the Webbrowser control can show dialog boxes 
            //wb.FileDownloadDirectory = "C:\\Documents and Settings\\Mike\\My Documents\\";
            
        }

        private void InitWBEvent(cEXWB wb)
        {
            //
            //wb.NavigateError += new NavigateErrorEventHandler(wb_NavigateError);

            wb.DocumentComplete += new csExWB.DocumentCompleteEventHandler(wb_DocumentComplete);

            // 脚本错误
            wb.ScriptError += new ScriptErrorEventHandler(wb_ScriptError);

            wb.NewWindow2 += new NewWindow2EventHandler(wb_NewWindow2);

            wb.NewWindow3 += new NewWindow3EventHandler(wb_NewWindow3);

        }

        private void wb_DocumentComplete(object sender,DocumentCompleteEventArgs e)
        {
            //var wb = sender as cEXWB;
            if (e.url.ToLower() == "about:blank")
            {
                Debug.Print("DocumentComplete::about:blank = istoplevel===>" + e.istoplevel.ToString());
                return;
            }
            if (e.istoplevel)
            {
                CalculatorCallbackService.done = true;
                //Debug.Print("DocumentComplete::TopLevel is TRUE===>" + e.url);

                //if(wb!=null)
                //{
                //    wb.SaveBrowserImage("C:\\aac.png",System.Drawing.Imaging.PixelFormat.Format24bppRgb,System.Drawing.Imaging.ImageFormat.Png); 
                //}
            }
            else
            {
                //Debug.Print("DocumentComplete::TopLevel is FALSE===>" + e.url);
            }
        }

        private void wb_ScriptError(object sender,ScriptErrorEventArgs e)
        {
            e.continueScripts = true;
        }

        private void wb_NewWindow2(object sender, NewWindow2EventArgs e)
        {
            e.Cancel = true;
        }

        private void wb_NewWindow3(object sender, NewWindow3EventArgs e)
        {
            e.Cancel = true;
        }

        delegate void D(object obj);

        void DelegateSetValue(object obj)
        {
            this.label1.Text = obj.ToString();
        }

        #region 浏览器控件的一些辅助方法

        /// <summary>
        /// 浏览器组件事件绑定
        /// </summary>
        /// <param name="wb"></param>
        private void RegiserWebBrowerHandler(cEXWB wb)
        {
            if (wb != null)
            {
                //注册事件处理方法
                wb.ProtocolHandlerBeginTransaction += new ProtocolHandlerBeginTransactionEventHandler(WebBrower_ProtocolHandlerBeginTransaction);
                wb.ProtocolHandlerOnResponse += new ProtocolHandlerOnResponseEventHandler(WebBrower_ProtocolHandlerOnResponse);
                wb.ProtocolHandlerDataFullyAvailable += new ProtocolHandlerDataFullyAvailableEventHandler(WebBrower_ProtocolHandlerDataFullyAvailable);
                wb.ProtocolHandlerDataFullyRead += new ProtocolHandlerDataFullyReadEventHandler(WebBrower_ProtocolHandlerDataFullyRead);
                wb.ProtocolHandlerOperationFailed += new ProtocolHandlerOperationFailedEventHandler(WebBrower_ProtocolHandlerOperationFailed);

                wb.ScriptError += new ScriptErrorEventHandler(WebBrower_ScriptError);
                wb.WBOnDocumentChanged += new EventHandler(WebBrower_WBOnDocumentChanged);
                wb.DocumentComplete += new DocumentCompleteEventHandler(WebBrower_DocumentComplete);
                wb.NavigateError += new NavigateErrorEventHandler(WebBrower_NavigateError);
                wb.WBSecurityProblem += new SecurityProblemEventHandler(WebBrower_WBSecurityProblem);
                //wb.BeforeNavigate2 += new BeforeNavigate2EventHandler(VBACsEXWB_BeforeNavigate2);
                wb.WBDocHostShowUIShowMessage += new DocHostShowUIShowMessageEventHandler(WebBrower_WBDocHostShowUIShowMessage);
                wb.ProcessUrlAction += new ProcessUrlActionEventHandler(WebBrower_ProcessUrlAction);
                //wb.WBEvaluteNewWindow += new EvaluateNewWindowEventHandler(WebBrower_WBEvaluteNewWindow);
            }
        }

        /// <summary>
        /// 解除浏览器绑定的事件
        /// </summary>
        /// <param name="wb"></param>
        private void UnregiserWebBrowerHandler(cEXWB wb)
        {
            if (wb != null)
            {
                //注销事件处理方法
                wb.ProtocolHandlerBeginTransaction -= new csExWB.ProtocolHandlerBeginTransactionEventHandler(WebBrower_ProtocolHandlerBeginTransaction);
                wb.ProtocolHandlerOnResponse -= new csExWB.ProtocolHandlerOnResponseEventHandler(WebBrower_ProtocolHandlerOnResponse);
                wb.ProtocolHandlerDataFullyAvailable -= new csExWB.ProtocolHandlerDataFullyAvailableEventHandler(WebBrower_ProtocolHandlerDataFullyAvailable);
                wb.ProtocolHandlerDataFullyRead -= new csExWB.ProtocolHandlerDataFullyReadEventHandler(WebBrower_ProtocolHandlerDataFullyRead);
                wb.ProtocolHandlerOperationFailed -= new csExWB.ProtocolHandlerOperationFailedEventHandler(WebBrower_ProtocolHandlerOperationFailed);

                wb.ScriptError -= new ScriptErrorEventHandler(WebBrower_ScriptError);
                wb.WBOnDocumentChanged -= new EventHandler(WebBrower_WBOnDocumentChanged);

                wb.DocumentComplete -= new DocumentCompleteEventHandler(WebBrower_DocumentComplete);
                wb.NavigateError -= new NavigateErrorEventHandler(WebBrower_NavigateError);
                wb.WBSecurityProblem -= new SecurityProblemEventHandler(WebBrower_WBSecurityProblem);
                //wb.BeforeNavigate2 -= new BeforeNavigate2EventHandler(VBACsEXWB_BeforeNavigate2);
                wb.WBDocHostShowUIShowMessage -= new DocHostShowUIShowMessageEventHandler(WebBrower_WBDocHostShowUIShowMessage);
                wb.ProcessUrlAction -= new ProcessUrlActionEventHandler(WebBrower_ProcessUrlAction);
                //wb.WBEvaluteNewWindow -= new EvaluateNewWindowEventHandler(WebBrower_WBEvaluteNewWindow);
            }
        }

        //Fired to indicate when a response from a server has been received
        void WebBrower_ProtocolHandlerOnResponse(object sender, ProtocolHandlerOnResponseEventArgs e)
        {
            Debug.Print(">>>>>>ProtocolHandlerOnResponse=> " + e.URL);
            //+ "\r\nResponseHeaders >>\r\n" + e.headers);
        }

        //Fired to indicate when a request for a resource is about to be initiated
        void WebBrower_ProtocolHandlerBeginTransaction(object sender, ProtocolHandlerBeginTransactionEventArgs e)
        {
            Debug.Print(">>>>>>ProtocolHandlerBeginTransaction=> " + e.URL);
            //+ "\r\nRequestHeaders >>\r\n" + e.RequestHeaders);
        }

        //Fired to indicate when a resource has been fully read by the MSHTML
        void WebBrower_ProtocolHandlerDataFullyRead(object sender, ProtocolHandlerDataFullyReadEventArgs e)
        {
            Debug.Print(">>>>>>ProtocolHandlerDataFullyRead=> " + e.URL);
        }

        //Fired to indicate when a resource has been fully downloaded and ready to be read by MSHTML
        void WebBrower_ProtocolHandlerDataFullyAvailable(object sender, ProtocolHandlerDataFullyAvailableEventArgs e)
        {
            Debug.Print(">>>>>>ProtocolHandlerDataFullyAvailable=> " + e.URL);
        }

        //Fired to indicate when download of a resource has failed
        void WebBrower_ProtocolHandlerOperationFailed(object sender, ProtocolHandlerOperationFailedEventArgs e)
        {
            Debug.Print(">>>>>>ProtocolHandlerOperationFailed=> " + e.URL);
        }

        //脚本发生错误
        private void WebBrower_ScriptError(object sender, ScriptErrorEventArgs e)
        {
            Logger.Log("\r\ncEXWB1_ScriptError=====" + e.errorMessage + "=" + e.lineNumber.ToString());
        }

        private void WebBrower_WBOnDocumentChanged(object sender, EventArgs e)
        {
            try
            {
                cEXWB pWB = sender as cEXWB;
                if(pWB!=null)
                {
                   //Logger.Log(LogLevel.Info, ">>>>>>WBOnDocumentChanged=>"+pWB.SendSourceOnDocumentCompleteWBEx); 
                }
                //Debug.Print(">>>>>>WBOnDocumentChanged=>");
            }
            catch (System.ObjectDisposedException)
            {
                Logger.Log(LogLevel.Info, "WBOnDocumentChanged::System.ObjectDisposedException");
                //Debug.Print("WBOnDocumentChanged::System.ObjectDisposedException");
            }
        }

        private void WebBrower_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            if (e.url.ToLower() == "about:blank")
            {
                return;
            }
            cEXWB pWB = sender as cEXWB;
            if (e.istoplevel)
            {
                IsDocumentFinish = true;
                //Logger.Log(LogLevel.Info, string.Format("DocumentComplete,Url:{0},时间：{1}", e.url,DateTime.Now));
            }
            else  if (pWB!=null&&pWB.MainDocumentFullyLoaded) // a frame naviagtion within a frameset
            {
                IsDocumentFinish = true;
                //Logger.Log(LogLevel.Info, string.Format("MainDocumentFullyLoaded,Url:{0},时间：{1}", e.url, DateTime.Now));
            }
            else
            {
                //log.Debug("DocumentComplete::TopLevel is FALSE===>" + e.url);
            }
        }

        private void WebBrower_NavigateError(object sender, NavigateErrorEventArgs e)
        {
            e.Cancel = true;
            Logger.Log(string.Format("Url:{0},StatusCode:{1}",e.url,e.statuscode));
        }

        private void WebBrower_WBSecurityProblem(object sender, SecurityProblemEventArgs e)
        {
            //处理一般安全提示
            if ((e.problem == WinInetErrors.HTTP_REDIRECT_NEEDS_CONFIRMATION) ||
                (e.problem == WinInetErrors.ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR) ||
                (e.problem == WinInetErrors.ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR) ||
                (e.problem == WinInetErrors.ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR) ||
                (e.problem == WinInetErrors.ERROR_INTERNET_MIXED_SECURITY))
            {
                e.handled = true;
                e.retvalue = Hresults.S_FALSE;
                return;
            }

            //非正常安全提示
            Logger.Log(e.problem.ToString());
            //log.Debug("WBSecurityProblem==>" + e.problem.ToString());
            //m_IsNavigationError = true;
            //m_IEErrorCode = e.problem;
            //lock (navigatelocker)
            //{
            //    Monitor.Pulse(navigatelocker);
            //}
        }

        private void WebBrower_WBDocHostShowUIShowMessage(object sender, DocHostShowUIShowMessageEventArgs e)
        {
            //To stop messageboxs
            e.handled = true;
            //Default
            e.result = (int)DialogResult.Cancel;
            Logger.Log(string.Format("WBDocHostShowUIShowMessage,Caption:{0},Helpfile:{1},Text:{2},Type:{3}", e.caption, e.helpfile, e.text, e.type));
        }

        private static Guid ms_guidFlash = new Guid("D27CDB6E-AE6D-11cf-96B8-444553540000");

        private void WebBrower_ProcessUrlAction(object sender, ProcessUrlActionEventArgs e)
        {
            if (e.urlAction == URLACTION.SCRIPT_RUN)
            {
                //m_ScriptStartTimepoint = DateTime.Now;
                //Logger.Log(string.Format("ProcessUrlAction脚本开始执行,Url:{0},UrlAction{1},UrlPolicy:{2},Context:{3}", e.url, e.urlAction, e.urlPolicy, e.context));
            }
            else if (e.urlAction == URLACTION.ACTIVEX_RUN)
            {
                if (e.context == ms_guidFlash)
                {
                    e.handled = true;
                    e.urlPolicy = URLPOLICY.DISALLOW;
                    //Logger.Log(string.Format("ProcessUrlAction,Url:{0},UrlAction{1},UrlPolicy:{2},Context:{3}", e.url, e.urlAction, e.urlPolicy, e.context));
                }
            }
        }

        private bool IsWebFilled(cEXWB wb)
        {
            try
            {
                string src = wb.DocumentTitle;
                if (!string.IsNullOrEmpty(src))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        private void DoSomethingWithParameterV2(object x)
        {
            using (cEXWB wb = new cEXWB())
            {
                //https://code.google.com/p/slowandsteadyparser/source/search?q=DocumentComplete&origq=DocumentComplete&btnG=Search+Trunk
                this.SetupWebBrower(wb);
                var data = new List<SpiderResult>();
                // 浏览器事件绑定
                this.RegiserWebBrowerHandler(wb);
                //wb.DocumentComplete += new DocumentCompleteEventHandler(pWb_DocumentComplete);
                var toProcessUrl = x as IList<SpiderParameter>;
                int i = 0;
                //HiPerfTimer q;
                foreach (var spider in toProcessUrl)
                {
                    i++;
                    //wb.NavToBlank();
                    //q = new HiPerfTimer();

                    //q.Start();
                    
                    wb.Navigate(spider.Url);
                    var flage = false;
                    long Start = DateTime.Now.Ticks;

                    while (!this.IsDocumentFinish)
                    {
                        
                        Thread.Sleep(10);
                        Application.DoEvents();
                        if ((DateTime.Now.Ticks - Start) / 10000 > 8000)
                        {
                            flage = true;
                            break;
                        }
                    }
                    if(flage)
                    {
                        continue;
                    }
                    this.IsDocumentFinish = false;
                    var Elapse = (DateTime.Now.Ticks - Start) / 10000;
                    //q.Stop();

                    if (label1.InvokeRequired)
                    {
                        D d = new D(DelegateSetValue);
                        label1.Invoke(d, spider.Url + ":" + string.Format("{0}/{1}/网络采集时间：{2}毫秒", i, toProcessUrl.Count, Elapse));

                    }
                    else
                    {
                        this.label1.Text = spider.Url + ":" + string.Format("{0}/{1}", i, toProcessUrl.Count);
                    }
                    if(!string.IsNullOrEmpty(wb.DocumentSource))
                    {
                        var htmlDocument= HtmlAgilityPackHelper.GetHtmlDocument(wb.DocumentSource);
                        //标题
                        var title=htmlDocument.DocumentNode.CssSelect("div#name > h1").FirstOrDefault();
                        //var title = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[6]/div[1]/div[1]/h1[1]");
                        var price = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='p-price']/img");
                        // 文字价格
                        var priceText = htmlDocument.DocumentNode.SelectSingleNode("/html/head/script[3]");

                        // 产品图片
                        var defaultImage = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[5]/div[1]/div[2]/div[1]");

                        var beginIndex = priceText.InnerText.IndexOf("price:'");
                        if(beginIndex>-1)
                        {
                            var length = "price:'".Length;

                            var endIndex = priceText.InnerText.IndexOf("'", beginIndex);
                            var readPrice = priceText.InnerText.Substring(beginIndex + length, endIndex - beginIndex-1);
                            decimal decimalRealPrice = 0;
                            if (decimal.TryParse(readPrice, out decimalRealPrice))
                            {
                                data.Add(new SpiderResult() { ProductId = spider.ProductId, Price = decimalRealPrice, Name = title.InnerText, Elapse = Elapse, LastModify = DateTime.Now, IsSucceed = true });
                            }
                            else
                            {
                                Logger.Log(readPrice);
                            }
                        }
                        else
                        {
                            Logger.Log(priceText.InnerText);
                        }
                    }
                    else
                    {
                        data.Add(new SpiderResult() { ProductId = spider.ProductId, Elapse = Elapse, LastModify = DateTime.Now, IsSucceed = false });
                        DataAccess.SaveHtmlSource(spider.ProductId, wb.DocumentSource);
                    }
                    
                }

                //解除事件绑定 
                this.UnregiserWebBrowerHandler(wb);
                Logger.Log(data.Count.ToString());
                //请求服务器端数据
                Calculator.ServerProcess(data);
                
                SetMessage(string.Format("{0}条数据已上传",data.Count));
            }

        }
        private void SetMessage(string message)
        {
            if (label1.InvokeRequired)
            {
                D d = new D(DelegateSetValue);
                label1.Invoke(d, message);

            }
            else
            {
                this.label1.Text = message;
            }
        }

        static void pWb_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            if (e.url.ToLower() == "about:blank")
            {
                Debug.Print("DocumentComplete::about:blank = istoplevel===>" + e.istoplevel.ToString());
                return;
            }
            if (e.istoplevel)
            {
                CalculatorCallbackService.done = true;
                Debug.Print("DocumentComplete::TopLevel is TRUE===>" + e.url);
            }
            else
            {
                Debug.Print("DocumentComplete::TopLevel is FALSE===>" + e.url);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<SessionInfo> activeSessions = SessionUtility.GetActiveSessions();
            MessageBox.Show(activeSessions.Count.ToString());
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnGenerateAutoUpdateXml_Click(object sender, EventArgs e)
        {
            var fileLists = new List<AutoUpdateFileInfo>();
            var dirRootInfo = new DirectoryInfo(System.Environment.CurrentDirectory);
            GetFileList(dirRootInfo, fileLists);
            StringBuilder sb = new StringBuilder();
            if(fileLists.Count>0)
            {
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sb.AppendLine("<updateFiles>");
                foreach (AutoUpdateFileInfo autoUpdateFileInfo in fileLists)
                {
                    sb.AppendFormat(
                        "<file path=\"{0}\" url=\"{1}\" lastver=\"{2}\" size=\"{3}\" needRestart=\"{4}\" />\r\n", autoUpdateFileInfo.Path, autoUpdateFileInfo.Url, autoUpdateFileInfo.LastVer, autoUpdateFileInfo.Size, autoUpdateFileInfo.NeedRestart);
                }
                sb.AppendLine("</updateFiles>");
            }
            if(File.Exists(System.Environment.CurrentDirectory+"\\AutoUpdateService.xml"))
            {
                File.Delete(System.Environment.CurrentDirectory+"\\AutoUpdateService.xml");
                File.WriteAllText(System.Environment.CurrentDirectory + "\\AutoUpdateService.xml",sb.ToString(),new UTF8Encoding());
            }
        }

        private void GetFileList(DirectoryInfo diroot, List<AutoUpdateFileInfo> fileLists)
        {
            foreach (FileInfo file in diroot.GetFiles())
            {
                if (file.Extension == ".dll" || file.Extension == ".exe"|| file.Extension == ".pdb")
                {
                    var autoUpdateFile = new AutoUpdateFileInfo();
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(file.FullName);
                    autoUpdateFile.LastVer = fileVersionInfo.FileVersion;
                    autoUpdateFile.Path = file.Name;
                    autoUpdateFile.NeedRestart = true;
                    autoUpdateFile.Size = file.Length;
                    autoUpdateFile.Url = ConfigHelper.ParamsConfig.GetParamValue("BaseUrl") + file.Name;
                    
                    fileLists.Add(autoUpdateFile);
                }
                
            }
            foreach (DirectoryInfo dirSub in diroot.GetDirectories())
            {
                GetFileList(dirSub, fileLists);
            }
        }

        List<Guid> _registeredClients = new List<Guid>();

        public void Register(Guid clientID)
        {
            if (!_registeredClients.Contains(clientID))
                _registeredClients.Add(clientID);
        }

        public void ReportStatus(SpiderState state)
        {
            this.label1.Text = state.Url;
            this.Text = state.Url;
        }
    }
}
