using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

using DotNet.Common.Logging;
using csExWB;
using IfacesEnumsStructsClasses;

namespace DotNet.BasicSpider
{
    /// <summary>
    /// 浏览器组件的管理类，入口
    /// 仅用于winform
    /// </summary>
    public class WebBrowerManager
    {
        public cEXWB WB { get; set; }

        private bool IsDocumentFinish = false;

        private WebBrowerManager()
        {
            TimeOut = 8;
        }

        #region 注册组建

        [DllImport("ComUtilities.dll")]
        private static extern int DllRegisterServer();//注册时用
        [DllImport("ComUtilities.dll")]
        private static extern int DllUnregisterServer();//取消注册时用

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
            WB = wb;
            if(WB!=null)
            {
                this.SetupWebBrower(WB);
                this.RegiserWebBrowerHandler(WB);
            }
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

            WB.Navigate(url);
            var flage = false;
            long Start = DateTime.Now.Ticks;

            while (!this.IsDocumentFinish)
            {

                Thread.Sleep(10);
                Application.DoEvents();
                if ((DateTime.Now.Ticks - Start) / 100000000 > TimeOut)
                {
                    flage = true;
                    break;
                }
            }
            if (flage)
            {
                return string.Empty;
            }
            this.IsDocumentFinish = false;
            var Elapse = (DateTime.Now.Ticks - Start) / 10000;
            return WB.DocumentSource;
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public void Clear()
        {
            if(WB!=null)
            {
                //解除事件绑定 
                this.UnregiserWebBrowerHandler(WB);
                WB.Dispose();
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
            //+ "\r\nResponseHeaders >>\r\n" + e.ResponseHeaders);
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

        #endregion
    }
}
