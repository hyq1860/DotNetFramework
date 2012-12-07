using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

using csExWB;
using IfacesEnumsStructsClasses;
using DotNet.Common.Logging;

namespace DotNet.BasicSpider
{
    /// <summary>
    /// 浏览器组件的管理类，入口
    /// 仅用于winform
    /// 浏览器性能测试 http://www.265g.com/news/201103/117285.html
    /// http://blog.163.com/xuhengxiao@126/blog/static/14768104120101112112611408/
    /// http://www.cnblogs.com/liuzhendong/archive/2012/03/21/2410107.html
    /// http://www.cnblogs.com/hhh/archive/2007/01/27/632251.html // 又一个编码识别
    /// </summary>
    public class WebBrowerManager
    {
        private cEXWB WB { get; set; }

        private bool IsDocumentFinish = false;

        private WebBrowerManager()
        {
            TimeOut = 8;
        }

        #region 注册组建

        /// <summary>
        /// 消耗的时间 一秒为单位
        /// </summary>
        public long Elapse { get; set; }

        // 注册时用
        [DllImport("ComUtilities.dll")]
        private static extern int DllRegisterServer();

        // 取消注册时用
        [DllImport("ComUtilities.dll")]
        private static extern int DllUnregisterServer();

        public void Register()
        {
            /*
              regsvr32.exe是32位系统下使用的DLL注册和反注册工具，使用它必须通过命令行的方式使用，格式是： 
　　          regsvr32 [/u] [/s] [/n] [/i[:cmdline]] DLL文件名 
　　          命令可以在“开始→运行”的文本框中，也可以事先在bat批处理文档中编写好命令。未带任何参数是注册DLL文件功能，其它参数对应功能如下： 
　　          /u：反注册DLL文件; 
　　          /s：安静模式(Silent)执行命令，即在成功注册/反注册DLL文件前提下不显示结果提示框。 
　　          /c：控制端口; 
　　          /i：在使用/u反注册时调用DllInstall; 
　　          /n：不调用DllRegisterServer，必须与/i连用。 
             */
            Process p = new Process();
            p.StartInfo.FileName = "Regsvr32.exe";
            string dllPath = Environment.CurrentDirectory + "\\ComUtilities.dll";
            p.StartInfo.Arguments = "/s " + dllPath;//路径中不能有空格
            p.Start();
        }

        /// <summary>
        /// 注册组件
        /// </summary>
        public bool RegisterCsExwb()
        {
            int resisterSucceed = DllRegisterServer();
            return resisterSucceed >= 0;
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <returns></returns>
        public bool UnregisterCsExwb()
        {
            //98EDB477-3064-4D0E-A09E-CC73F9AAB324
            int unregisterSucceed = DllUnregisterServer();
            return unregisterSucceed >= 0;
        }

        /// <summary>
        /// 设置csexwb调用的ie版本
        /// </summary>
        public void SetIEVersion()
        {
            var ieRegistryKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer", true);
            object ieVersion = ieRegistryKey.GetValue("Version");

            var versionValue = string.Empty;

            if (ieVersion != null)
            {
                string version = ieVersion.ToString();
                if (!string.IsNullOrEmpty(version))
                {
                    var arrays = version.Split('.');
                    switch (arrays[0])
                    {
                        case "9":
                            {
                                versionValue = "9999";
                            }

                            break;
                        case "8":
                            {
                                versionValue = "8888";
                            }

                            break;
                        case "7":
                            {
                                versionValue = "7000";
                            }

                            break;
                        default:
                            break;
                    }
                }

                using (RegistryKey currentKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true))
                {
                    if (currentKey != null)
                    {
                        string exeName = Application.ProductName + ".exe";
                        object keyValue = currentKey.GetValue(exeName);
                        if (keyValue == null)
                        {
                            currentKey.SetValue(exeName, int.Parse(versionValue), RegistryValueKind.DWord);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断当前程序是不是管理员权限
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// 检验组建是否已经注册
        /// </summary>
        /// <returns></returns>
        public bool CheckCsExwbIsRegistered()
        {
            RegistryKey rkTest = Registry.ClassesRoot.OpenSubKey("CLSID\\{98EDB477-3064-4D0E-A09E-CC73F9AAB324}\\");
            return rkTest == null;
        }
        #endregion

        public void Setup(cEXWB wb)
        {
            this.WB = wb;
            if (this.WB == null) return;
            this.SetupWebBrower(WB);
            this.RegiserWebBrowerHandler(WB);
        }

        /// <summary>
        /// 标注表明对于每个线程都是唯一
        /// </summary>i
        [ThreadStatic]
        public static readonly WebBrowerManager Instance=new WebBrowerManager();

        /// <summary>
        /// 每个页面访问的超时时间
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// ie浏览器的版本
        /// </summary>
        public string IEVersion { get; set; }

        /// <summary>
        /// 浏览器打开NavToBlank空白页的次数
        /// </summary>
        private static int navToBlankCount = 1;

        /// <summary>
        /// 采集页面，返回页面html
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Run(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            // need to initialize the webbrowser control by calling NavToBlank() at least once
            if (navToBlankCount == 1)
            {
                WB.NavToBlank();
                navToBlankCount++;
            }

            WB.Navigate(url);
            var flage = false;
            long start = DateTime.Now.Ticks;
            long elapse = 0;
            while (!this.IsDocumentFinish)
            {
                Application.DoEvents();
                Thread.Sleep(50);
                elapse = (DateTime.Now.Ticks - start) / 100000000;
                if (elapse > this.TimeOut)
                {
                    flage = true;
                    break;
                }
            }

            if (flage)
            {
                return string.Empty;
            }
            Elapse = elapse;
            this.IsDocumentFinish = false;

            // 获取ie浏览器版本
            IEVersion=WB.IEVersion();

            /* 自动登陆
            To automate login, navigate to login page, 
            in DocumentComplete check for isTopLevel which indiactes the page has fully loaded and a flag to indicate if you have not logged in. 
            Then use AutomationTask methods to login.
            WB.AutomationTask_PerformEnterData("UsernameTextBox_Name", "username");
            WB.AutomationTask_PerformEnterData("PasswordTextBox_Name", "password");
            WB.AutomationTask_PerformClickButton("SubmitButton_Name");
             */

            return WB.DocumentSource;
        }

        public object InvokeScript(IWebBrowser2 wb, string ScriptName, object[] Data)
        {
            object oRet = null;

            if (wb == null)
                return oRet;

            IHTMLDocument doc = wb.Document as IHTMLDocument;
            if (doc == null)
                return oRet;
            object oScript = doc.Script;
            if (oScript == null)
                return oRet;
            //Invoke script
            if (Data == null)
                Data = new object[] { };
            oRet = oScript.GetType().InvokeMember(ScriptName,
                System.Reflection.BindingFlags.InvokeMethod, null, oScript, Data);
            return oRet;
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public void Clear()
        {
            if(WB!=null)
            {
                //解除事件绑定 
                //this.UnregiserWebBrowerHandler(WB);
                //WB.Dispose();
                WB.Clear();
                WB.ClearHistory();
                WB.ClearSessionCookies();
                //WB = null;
                //GC.Collect();
            }
        }

        #region 浏览器辅助方法

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
                wb.NewWindow2 += new NewWindow2EventHandler(WebBrower_NewWindow2);
            }
        }

        //all new_window action is ignored.
        private void WebBrower_NewWindow2(object sender, csExWB.NewWindow2EventArgs e)
        {
            try
            {
                //AllForms.m_frmLog.AppendToLog("frmPopup_cEXWB1_NewWindow2");
                e.Cancel = true;
            }
            catch
            {
                throw;
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
                wb.ProtocolHandlerBeginTransaction -= new ProtocolHandlerBeginTransactionEventHandler(WebBrower_ProtocolHandlerBeginTransaction);
                wb.ProtocolHandlerOnResponse -= new ProtocolHandlerOnResponseEventHandler(WebBrower_ProtocolHandlerOnResponse);
                wb.ProtocolHandlerDataFullyAvailable -= new ProtocolHandlerDataFullyAvailableEventHandler(WebBrower_ProtocolHandlerDataFullyAvailable);
                wb.ProtocolHandlerDataFullyRead -= new ProtocolHandlerDataFullyReadEventHandler(WebBrower_ProtocolHandlerDataFullyRead);
                wb.ProtocolHandlerOperationFailed -= new ProtocolHandlerOperationFailedEventHandler(WebBrower_ProtocolHandlerOperationFailed);

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
            //Debug.Print(">>>>>>ProtocolHandlerOnResponse=> " + e.URL);
            //+ "\r\nResponseHeaders >>\r\n" + e.ResponseHeaders);
        }

        //Fired to indicate when a request for a resource is about to be initiated
        void WebBrower_ProtocolHandlerBeginTransaction(object sender, ProtocolHandlerBeginTransactionEventArgs e)
        {
            //Debug.Print(">>>>>>ProtocolHandlerBeginTransaction=> " + e.URL);
            //+ "\r\nRequestHeaders >>\r\n" + e.RequestHeaders);
        }

        //Fired to indicate when a resource has been fully read by the MSHTML
        void WebBrower_ProtocolHandlerDataFullyRead(object sender, ProtocolHandlerDataFullyReadEventArgs e)
        {
            //Debug.Print(">>>>>>ProtocolHandlerDataFullyRead=> " + e.URL);
        }

        //Fired to indicate when a resource has been fully downloaded and ready to be read by MSHTML
        void WebBrower_ProtocolHandlerDataFullyAvailable(object sender, ProtocolHandlerDataFullyAvailableEventArgs e)
        {
            //Debug.Print(">>>>>>ProtocolHandlerDataFullyAvailable=> " + e.URL);
        }

        //Fired to indicate when download of a resource has failed
        void WebBrower_ProtocolHandlerOperationFailed(object sender, ProtocolHandlerOperationFailedEventArgs e)
        {
            //Debug.Print(">>>>>>ProtocolHandlerOperationFailed=> " + e.URL);
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
                if (pWB != null)
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

        /*
        When e.isTopLevel parameter of DocumentComplete is true, it means that the main document has finished loading whether we have a frameset or not.
        In a frameset page, DocumentComplete is fired for every frame. In the case of a frame, e.isTopLevel is false, meanning that the document is still loading.
        When all frames have been loaded then the DocumentComplete is fired for the main document signaling that the document has finished loading which is recognized when isTopLevel is true. There is no need to use timers to find out when the page has fully loaded.
         */
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
            else if (pWB != null && pWB.MainDocumentFullyLoaded) // a frame naviagtion within a frameset
            {
                IsDocumentFinish = true;
                //Logger.Log(LogLevel.Info, string.Format("MainDocumentFullyLoaded,Url:{0},时间：{1}", e.url, DateTime.Now));
            }
            else
            {
                //log.Debug("DocumentComplete::TopLevel is FALSE===>" + e.url);
            }
            GC.Collect();
        }

        private void WebBrower_NavigateError(object sender, NavigateErrorEventArgs e)
        {
            e.Cancel = true;
            Logger.Log(string.Format("Url:{0},StatusCode:{1}", e.url, e.statuscode));
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

        private void GetIFrames()
        {
            IHTMLElementCollection col = WB.GetElementsByTagName(true, "IFRAME") as IHTMLElementCollection;
            if (col != null)
            {
                foreach (IHTMLElement elem in col)
                {
                    if (elem != null)
                    {
                        //subnode = root.Nodes.Add("IFrame");
                        //subnode.Tag = elem.outerHTML; //+ "\r\n" + elem.getAttribute("src", 0)
                    }
                }
            }
        }

        #endregion
    }
}
