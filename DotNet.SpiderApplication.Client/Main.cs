using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Amib.Threading;
using csExWB;
using DotNet.Base.Service;
using DotNet.BasicSpider;
using DotNet.Common;
using DotNet.Common.Configuration;
using DotNet.Common.Core;
using DotNet.Common.Logging;
using DotNet.Common.Utility;
using DotNet.Data;
using DotNet.IoC;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;
using DotNet.SpiderApplication.Contract.WCF;
using DotNet.SpiderApplication.Service;
using DotNet.SpiderApplication.Service.Implemention.Service;
using DotNet.Web.Http;
using HtmlAgilityPack;

namespace DotNet.SpiderApplication.Client
{
    using System.Xml;

    using ScrapySharp.Extensions;

    using ICalculator = DotNet.SpiderApplication.Contract.ICalculator;

    //class MyProcess : Process
    //{
    //    public void Stop()
    //    {
    //        this.CloseMainWindow();
    //        this.Close();
    //        OnExited();
    //    }
    //}
    //http://www.cnblogs.com/stanley107/archive/2012/12/18/2823096.html 无配置wcf
    //http://www.cnblogs.com/mecity/archive/2012/01/17/WCF.html wcf证书的问题
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class Main : Form, IServerToClient
    {
        //private static void myProcess_HasExited(object sender, System.EventArgs e)
        //{
        //    MessageBox.Show("Process has exited.");
        //}

        private ServiceHost host;
        
        private void SpiderWCFNetPipe()
        {
            this.host = new ServiceHost(typeof(SpiderServer), new Uri[] { new Uri("net.pipe://127.0.0.1") });
            this.host.AddServiceEndpoint(typeof(ISpiderServer), new NetNamedPipeBinding(), "GetSpiderTask");
            this.host.Open();
        }

        private ServiceHost serverHost;

        private void Report()
        {
            this.serverHost = new ServiceHost(this);
            this.serverHost.AddServiceEndpoint(typeof(IServerToClient), new NetNamedPipeBinding() { MaxReceivedMessageSize = int.MaxValue, ReaderQuotas=new XmlDictionaryReaderQuotas(){MaxStringContentLength = int.MaxValue} }, "net.pipe://127.0.0.1/Server");
            this.serverHost.Open();
        }

        private static SpiderTaskManager spiderTaskManager;

        private ToolStripStatusLabelWithBar toolStripProgressBar;

        private ToolStripStatusLabel toolStripStatusLabel;

        private ToolStripStatusLabel toolStripStatusIELabel;

        private void InitControls()
        {
            // 窗体大小锁定
            this.MaximumSize=new Size(640,400);
            this.MinimumSize = new Size(640, 400);
            this.MaximizeBox = false;

            // 窗口启动居中
            this.StartPosition = FormStartPosition.CenterScreen;

            // 初始化状态栏进度条
            this.toolStripProgressBar = new ToolStripStatusLabelWithBar();
            this.toolStripProgressBar.Visible = true;
            this.toolStripProgressBar.BarColor = Color.DeepSkyBlue;

            // label
            this.toolStripStatusLabel = new ToolStripStatusLabel();
            this.toolStripStatusLabel.Spring = true;

            this.toolStripStatusLabel.TextAlign = ContentAlignment.BottomCenter;

            // IEVersion
            this.toolStripStatusIELabel = new ToolStripStatusLabel();

            // this.toolStripStatusIELabel.Spring = true;
            this.toolStripStatusIELabel.TextAlign = ContentAlignment.BottomCenter;
            this.toolStripStatusIELabel.Text = string.Format("IE核心：{0}", "未知");

            // 分隔栏
            var toolStripSeparator = new ToolStripSeparator();

            this.statusStrip.Items.Add(this.toolStripStatusIELabel);
            this.statusStrip.Items.Add(toolStripSeparator);
            this.statusStrip.Items.Add(this.toolStripStatusLabel);
            this.statusStrip.Items.Add(this.toolStripProgressBar);
            this.statusStrip.Height = 18;
        }

        private void StartWebBrower()
        {
            Process p = new Process();
            p.StartInfo.FileName = Environment.CurrentDirectory + "\\SpiderInstance\\" + "DotNet.SpiderApplication.WebBrowerInstance.exe";
            p.EnableRaisingEvents = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.ErrorDialog = true;
            //p.StartInfo.Arguments = jsonData;
            //p.Exited += new EventHandler(myProcess_HasExited);
            p.Start();
            //p.WaitForInputIdle();
            //p.Stop();
        }

        public Main()
        {
            InitializeComponent();

            //Init();
            //AutoUpdaterHelper.AutoUpdater();

            InitControls();

            SpiderWCFNetPipe();

            Report();

            // 初始化采集任务管理类
            spiderTaskManager = new SpiderTaskManager();

            StartWebBrower();

            return;

            #region 注释
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
            #endregion
        }

        private DuplexChannelFactory<ICalculator> ChannelFactory;

        private CalculatorCallbackService CallbackService;

        private ICalculator Calculator;


        private void Main_Load(object sender, EventArgs e)
        {
            var p1 = SingletonProvider<ProcessWatcher>.UniqueInstance;
            p1.StartWatch();
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
                //Thread t1 = new Thread(new ParameterizedThreadStart(DoSomethingWithParameterV2));
                //t1.SetApartmentState(ApartmentState.STA);
                //t1.Start(data);
            }
        }

        private void cb_Quit(object sender, EventArgs e)
        {
            //Thread t1 = new Thread(new ParameterizedThreadStart(DoSomethingWithParameterV2));

            //t1.SetApartmentState(ApartmentState.STA);
            //t1.Start(CalculatorCallbackService.Data);
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

        private void button1_Click(object sender, EventArgs e)
        {
            IList<SessionInfo> activeSessions = SessionUtility.GetActiveSessions();
            MessageBox.Show(activeSessions.Count.ToString());
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 杀掉浏览器客户端进程
            this.KillProcess("DotNet.SpiderApplication.WebBrowerInstance");
            Environment.Exit(0);
        }

        /// <summary>
        /// 杀进程
        /// </summary>
        /// <param name="processName">
        /// The process name.
        /// </param>
        private void KillProcess(string processName)
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.ProcessName != processName)
                {
                    continue;
                }

                process.Kill();
            }
        }

        private void btnGenerateAutoUpdateXml_Click(object sender, EventArgs e)
        {
            var fileLists = new List<AutoUpdateFileInfo>();
            var dirRootInfo = new DirectoryInfo(System.Environment.CurrentDirectory);
            GetFileList(dirRootInfo, fileLists);
            StringBuilder sb = new StringBuilder();
            if (fileLists.Count > 0)
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

            if (File.Exists(System.Environment.CurrentDirectory + "\\AutoUpdateService.xml"))
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

        private int totalTask;

        private int currentTaskCount;

        private long totalTime;

        public void TransferData(SpiderResult result)
        {
            //MessageBox.Show(result.Url);
            if (currentTaskCount==0)
            {
                totalTask = result.TaskCount;
            }
            totalTime += result.Elapse;

            currentTaskCount++;
            toolStripStatusLabel.Text = result.Url;
            //this.Text = result.Url;
            if (this.toolStripProgressBar.Maximum != result.TaskCount)
            {
                this.toolStripProgressBar.Maximum = result.TaskCount;
                //this.toolStripProgressBar.Step = result.TaskCount;
                this.toolStripProgressBar.Minimum = 0;
                this.toolStripProgressBar.Width = 200;
            }

            if (currentTaskCount%result.TaskCount==0)
            {
               totalTask += result.TaskCount; 
            }
            this.Text = result.Title;
            this.toolStripProgressBar.Text = result.Current + "/" + result.TaskCount + "/" + this.totalTask + "/平均：" + totalTime / currentTaskCount+"/当前"+result.Elapse+"";
            this.toolStripProgressBar.Value = result.Current;
        }

        /// <summary>
        /// ie版本设置
        /// </summary>
        /// <param name="ieVersion"></param>
        public void ReportIEVersion(string ieVersion)
        {
            toolStripStatusIELabel.Text = string.Format("IE核心：{0}", ieVersion);
        }

        public List<SpiderProductInfo> GetSpiderTask(int count)
        {
            return spiderTaskManager.Dequeue(count);
        }
    }
}
