using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using csExWB;
using HtmlAgilityPack;
namespace DotNet.SpiderApplication.XpathHelper
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;

    using DotNet.Common.Utility;
    using DotNet.Web.Http;

    using IfacesEnumsStructsClasses;

    public partial class frmMain : Form
    {
        private cEXWB WebBrower;

        private string Html;

        private bool IsDocumentFinish = false;

        private SplitContainer SplitContainer;

        private SplitContainer LeftRightSplitContainer;

        private SplitContainer CenterSplitContainer;

        private SplitContainer TopBottomSplitContainer;

        private TreeView HtmlTree;

        private RichTextBox richTextBox;

        private Panel UrlPannel;

        private Panel BrowerPannel;

        

        public frmMain()
        {
            InitializeComponent();
            InitWebBrower();
            RegisterEvent(WebBrower);
            InitControls();
            InitUrls();
            htmlDocument = new HtmlAgilityPack.HtmlDocument();
        }

        private HtmlAgilityPack.HtmlDocument htmlDocument;

        private void InitWebBrower()
        {
            WebBrower=new cEXWB();

            this.WebBrower.Border3DEnabled = false;
            //this.WebBrower.DocumentSource = "<HTML><HEAD></HEAD>\r\n<BODY></BODY></HTML>";
            this.WebBrower.DocumentTitle = "";
            //this.WebBrower.DownloadActiveX = false;
            //this.WebBrower.DownloadFrames = true;
            //this.WebBrower.DownloadImages = true;
            //this.WebBrower.DownloadJava = true;
            //this.WebBrower.DownloadScripts = true;
            //this.WebBrower.DownloadSounds = false;
            //this.WebBrower.DownloadVideo = false;
            this.WebBrower.FileDownloadDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + System.IO.Path.DirectorySeparatorChar.ToString();
            this.WebBrower.Location = new Point(5, 38);
            this.WebBrower.LocationUrl = "about:blank";
            this.WebBrower.Name = "WebBrower";
            this.WebBrower.ObjectForScripting = null;
            this.WebBrower.OffLine = false;
            this.WebBrower.RegisterAsBrowser = true;
            this.WebBrower.RegisterAsDropTarget = false;
            this.WebBrower.RegisterForInternalDragDrop = true;
            this.WebBrower.ScrollBarsEnabled = true;
            this.WebBrower.SendSourceOnDocumentCompleteWBEx = false;
            this.WebBrower.Silent = false;
            this.WebBrower.Size = new Size(749, 449);
            this.WebBrower.TabIndex = 0;
            this.WebBrower.Text = "WebBrower";
            this.WebBrower.TextSize = IfacesEnumsStructsClasses.TextSizeWB.Medium;
            this.WebBrower.UseInternalDownloadManager = true;
            this.WebBrower.WBDOCDOWNLOADCTLFLAG = 112;
            this.WebBrower.WBDOCHOSTUIDBLCLK = IfacesEnumsStructsClasses.DOCHOSTUIDBLCLK.DEFAULT;
            this.WebBrower.WBDOCHOSTUIFLAG = 262276;
            this.WebBrower.Anchor = AnchorStyles.Left | AnchorStyles.Right|AnchorStyles.Top|AnchorStyles.Bottom;
            this.WebBrower.Dock = DockStyle.Fill;
            this.WebBrower.NavToBlank();
            this.WebBrower.Navigate("http://finance.qq.com/a/20121223/000025.htm?pgv_ref=aio2012&ptlang=2052");
        }

        private TextBox txtUrl;

        private Button btnSearch;

        private Button btnGo;

        private ListBox listBoxUrls;

        private ComboBox comboBoxUrl;

        private void InitControls()
        {
            

            UrlPannel=new Panel();
          
            BrowerPannel=new Panel();
            BrowerPannel.Enabled = true;

            comboBoxUrl=new ComboBox();
            comboBoxUrl.Anchor = AnchorStyles.Left|AnchorStyles.Right;
            comboBoxUrl.Width = 500;
            //this.comboBoxUrl.Location = new Point(5, 0);
            this.comboBoxUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.comboBoxUrl.Dock=DockStyle.Left;
            //this.comboBoxUrl.FormattingEnabled = true;
            //this.comboBoxUrl.Size = new System.Drawing.Size(632, 21);
            //
            this.Text = "Xpath助手";
            //HtmlTree
            HtmlTree = new TreeView();
            HtmlTree.Dock = DockStyle.Fill;
            HtmlTree.ShowNodeToolTips = true;
            //FileTree=new TreeView();
            //FileTree.Dock = DockStyle.Fill;
            //BuildTree();

            //
            richTextBox=new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;

            // 按钮 文字框
            txtUrl=new TextBox();
            //txtUrl.Location = new Point(5, 38);
            btnSearch=new Button();
            btnSearch.Location = new Point(5, 68);
            btnSearch.Text = "搜索";
            btnSearch.Click+=new EventHandler(btnSearch_Click);

            btnGo=new Button();
            btnGo.Text = "浏览";
            //btnGo.Location=new Point(0,0);
            btnGo.Dock = DockStyle.Left;
            //btnGo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnGo.Click+=new EventHandler(btnGo_Click);

            // listBoxUrls
            listBoxUrls=new ListBox();
            listBoxUrls.Location = new Point(100, 88);
            listBoxUrls.Dock=DockStyle.Fill;
            listBoxUrls.MouseClick+=new MouseEventHandler(listBoxUrls_MouseClick);

            //上下
            SplitContainer = new SplitContainer();
            SplitContainer.Orientation = Orientation.Horizontal;
            SplitContainer.Dock = DockStyle.Fill;

            //左右
            LeftRightSplitContainer=new SplitContainer();
            LeftRightSplitContainer.Dock = DockStyle.Fill;

            LeftRightSplitContainer.Panel1.Controls.Add(listBoxUrls);
            LeftRightSplitContainer.SplitterDistance = 5;
            //LeftRightSplitContainer.Panel1.Visible = false;

            //
            TopBottomSplitContainer = new SplitContainer();
            TopBottomSplitContainer.Dock = DockStyle.Fill;
            TopBottomSplitContainer.Orientation = Orientation.Horizontal;
            //TopBottomSplitContainer.Panel1.Controls.Add(txtUrl);
            //TopBottomSplitContainer.Panel1.Controls.Add(btnSearch);

            TopBottomSplitContainer.Panel1.Controls.Add(comboBoxUrl);
            //TopBottomSplitContainer.Panel1.Controls.Add(btnGo);
            //TopBottomSplitContainer.Panel2.Controls.Add(richTextBox);

            //CenterSplitContainer
            CenterSplitContainer=new SplitContainer();
            CenterSplitContainer.Dock=DockStyle.Fill;
            CenterSplitContainer.SplitterDistance = 80;
            CenterSplitContainer.Panel1.Controls.Add(HtmlTree);

            CenterSplitContainer.Panel2.Controls.Add(TopBottomSplitContainer);

            LeftRightSplitContainer.Panel2.Controls.Add(CenterSplitContainer);
            

            //SplitContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            
            
            UrlPannel.Controls.Add(btnGo);
            UrlPannel.Controls.Add(comboBoxUrl);

            UrlPannel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            UrlPannel.Dock=DockStyle.Top;
            UrlPannel.Height = comboBoxUrl.Height;
            BrowerPannel.Controls.Add(WebBrower);
            BrowerPannel.Dock=DockStyle.Fill;
            //BrowerPannel.Anchor = AnchorStyles.Left | AnchorStyles.Right |AnchorStyles.Bottom;
            SplitContainer.Panel1.Controls.Add(BrowerPannel);
            SplitContainer.Panel1.Controls.Add(UrlPannel);
            
            
            SplitContainer.Panel2.Controls.Add(LeftRightSplitContainer);
            
            this.Controls.Add(SplitContainer);
        }

        private void InitUrls()
        {
            listBoxUrls.Items.Add("http://www.360buy.com/product/149617.html");

            comboBoxUrl.DataSource = ApplicationSettings.UrlHistorys;

            comboBoxUrl.Focus();
        }

        private void RegisterEvent(cEXWB WebBrower)
        {
            WebBrower.DocumentComplete+=new DocumentCompleteEventHandler(WebBrower_DocumentComplete);
            WebBrower.WBLButtonDown += new csExWB.HTMLMouseEventHandler(WebBrower_WBLButtonDown);
            WebBrower.WBLButtonUp += new csExWB.HTMLMouseEventHandler(WebBrower_WBLButtonUp);
            WebBrower.NewWindow2+=new NewWindow2EventHandler(WebBrower_NewWindow2);
        }

        private bool AddNewBrowser(string TabText, string TabTooltip, string Url, bool BringToFront)
        {
            //Copy flags
            int iDochostUIFlag = (int)(DOCHOSTUIFLAG.NO3DBORDER |
                        DOCHOSTUIFLAG.FLAT_SCROLLBAR | DOCHOSTUIFLAG.THEME);
            int iDocDlCltFlag = (int)(DOCDOWNLOADCTLFLAG.DLIMAGES |
                        DOCDOWNLOADCTLFLAG.BGSOUNDS | DOCDOWNLOADCTLFLAG.VIDEOS);

            //if (WebBrower != null)
            //{
            //    iDochostUIFlag = WebBrower.WBDOCHOSTUIFLAG;
            //    iDocDlCltFlag = WebBrower.WBDOCDOWNLOADCTLFLAG;
            //}

            csExWB.cEXWB pWB = null;

            try
            {

                //Create and setup browser
                pWB = new csExWB.cEXWB();
                
                //pWB.Dock = cEXWB1.Dock;
                pWB.Anchor = WebBrower.Anchor;

                pWB.Location = WebBrower.Location;
                pWB.Size = WebBrower.Size;
                //pWB.Name = System.Guid.NewGuid().ToString();
                pWB.RegisterAsBrowser = true;
                pWB.WBDOCDOWNLOADCTLFLAG = iDocDlCltFlag;
                pWB.WBDOCHOSTUIFLAG = iDochostUIFlag;
                //pWB.FileDownloadDirectory = WebBrower.FileDownloadDirectory;
                //pWB.DownloadActiveX = false;
                //pWB.DownloadFrames = true;
                //pWB.DownloadImages = true;
                //pWB.DownloadJava = true;
                //pWB.DownloadScripts = true;
                //pWB.DownloadSounds = false;
                //pWB.DownloadVideo = false;
                
                pWB.RegisterAsBrowser = true;
                this.RegisterEvent(pWB);

                //Add to controls collection
                //this.Controls.Add(pWB);
                BrowerPannel.Controls.Add(pWB);

                if (BringToFront)
                {
                    //Bring to front
                    pWB.BringToFront();
                }
                //Increase count
                WebBrower = pWB;
            }
            catch (Exception ee)
            {
                return false;
            }

            return true;
        }

        #region 常规事件
        public void AssignBrowserObject(ref object obj)
        {
            obj = this.WebBrower.WebbrowserObject;
        }
        private const string m_AboutBlank = "about:blank";
        private const string m_Blank = "Blank";
        private void WebBrower_NewWindow2(object sender, csExWB.NewWindow2EventArgs e)
        {
            AddNewBrowser(m_Blank, m_AboutBlank, m_AboutBlank, true);
            AssignBrowserObject(ref e.browser);
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
                Html = pWB.DocumentSource;
                htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(pWB.DocumentSource);
                //Stopwatch sw=new Stopwatch();
                //sw.Start();
                //LoadHtmlTree();
                //sw.Stop();
                Stopwatch sw1 = new Stopwatch();
                sw1.Start();
                BuildTree3();
                sw1.Stop();
                richTextBox.Text+= string.Format("DOM树构建时间{0}毫秒", sw1.ElapsedMilliseconds)+"\n";
                //MessageBox.Show(sw1.ElapsedMilliseconds.ToString());
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
        #endregion

        #region 鼠标事件
        private int m_mposX = 0;
        private int m_mposY = 0;

        private string text = string.Empty;
        void WebBrower_WBLButtonUp(object sender, csExWB.HTMLMouseEventArgs e)
        {
            if (e.SrcElement != null)
            {
                //user is scrolling using scrollbars
                //if (e.SrcElement.tagName == "HTML")
                //    return;
                //If DIV then we can look for an HTML child element
                //AllForms.m_frmLog.AppendToLog("cEXWB1_WBLButtonUp==>" + e.SrcElement.tagName);
                TreeNodeEx tnRet = null;



                foreach (var tn in HtmlTree.Nodes)
                {
                    var treeNodeEx = tn as TreeNodeEx;
                    var selectedElement = new SelectedElement();
                    selectedElement.tagName = e.SrcElement.tagName.ToLower();
                    selectedElement.innerText = e.SrcElement.innerText;
                    tnRet = this.FindNodeExt(treeNodeEx, selectedElement);
                    if (tnRet != null) break;
                }
                if (tnRet!=null)
                {
                    tnRet.ForeColor = Color.Red;
                    tnRet.Expand();
                    HtmlTree.SelectedNode = tnRet;
                    var sb = new StringBuilder();
                    sb.AppendLine("xpath：" + tnRet.HtmlNode.XPath);
                    sb.AppendLine(HtmlAgilityPackHelper.GetStringByXPath(Html, tnRet.HtmlNode.XPath, "|"));
                    richTextBox.Text += sb.ToString();
                }
                
            }
            else
            {
                //AllForms.m_frmLog.AppendToLog("cEXWB1_WBLButtonUp");
            }
                
            //Rectangle rt = new Rectangle(m_mposX - 1, m_mposY - 1, 2, 2);
            //if (rt.Contains(e.ClientX, e.ClientY))
            //{
            //    //AllForms.m_frmLog.AppendToLog("MOUSE CLICKED");
            //}
        }

        void WebBrower_WBLButtonDown(object sender, csExWB.HTMLMouseEventArgs e)
        {
            m_mposX = e.ClientX;
            m_mposY = e.ClientY;
            if (e.SrcElement != null)
            {
                //if (e.SrcElement.tagName == "HTML")
                //    return;
                //AllForms.m_frmLog.AppendToLog("cEXWB1_WBLButtonDown==>" + e.SrcElement.tagName);
                this.Text = e.SrcElement.innerHTML;
            }
            else
            {
                //AllForms.m_frmLog.AppendToLog("cEXWB1_WBLButtonDown");
            }
        }
        #endregion

        private void btnSearch_Click(object sender,EventArgs e)
        {
            TreeNodeEx tnRet = null;



            foreach (var tn in HtmlTree.Nodes)
            {
                var treeNodeEx= tn as TreeNodeEx;
                

                tnRet = this.FindNodeExt(treeNodeEx, this.txtUrl.Text);
                if (tnRet != null) break;
            }
            tnRet.ForeColor = Color.Red;
            tnRet.Expand();
            
        }

        private void btnGo_Click(object sender,EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxUrl.Text.Trim()))
            {
                if (!ApplicationSettings.UrlHistorys.Contains(comboBoxUrl.Text.Trim()))
                {
                    ApplicationSettings.UrlHistorys.Add(comboBoxUrl.Text.Trim());
                    comboBoxUrl.DataSource = ApplicationSettings.UrlHistorys;
                    var filePath = Environment.CurrentDirectory + "\\" + "urls.json";
                    File.Delete(filePath);
                    File.WriteAllText(filePath, JsonHelper.ToJson(ApplicationSettings.UrlHistorys));
                }
                
                WebBrower.Navigate(comboBoxUrl.Text.Trim());
            }
        }

        private void listBoxUrls_MouseClick(object sender,EventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox != null && listbox.SelectedItem!=null)
            {
                var selectedItem = listbox.SelectedItem.ToString();
                WebBrower.Navigate(selectedItem);
            }
        }

        private static readonly string nbsp = " ";

        private void LoadHtmlTree()
        {
            if (this.htmlDocument == null || this.htmlDocument.DocumentNode == null)
            {
                return;
            }
            HtmlTree.BeginUpdate();
            this.HtmlTree.Nodes.Clear();

            foreach (var node in this.htmlDocument.DocumentNode.ChildNodes)
            {
                string text = node.Name;
                //if (node.HasAttributes)
                //{
                //    foreach (var attr in node.Attributes)
                //    {
                //        text += nbsp + attr.Name + "=" + attr.Value + nbsp;
                //    }
                //}

                var rootNode = new TreeNode(text);
                this.HtmlTree.Nodes.Add(rootNode);
                this.GetChildNode(rootNode, node);
            }
            HtmlTree.EndUpdate();
        }

        private void GetChildNode(TreeNode treeNode,HtmlNode htmlNode)
        {
            foreach (var node in htmlNode.ChildNodes)
            {
                string text = node.Name;
                //if (node.HasAttributes)
                //{
                //    foreach (var attr in node.Attributes)
                //    {
                //        text += nbsp + attr.Name + "=" + attr.Value + nbsp;
                //    }
                //}

                var tempTreeNode = new TreeNode(text);
                treeNode.Nodes.Add(tempTreeNode);
                this.GetChildNode(tempTreeNode, node);
            }
        }

        ///// <summary>
        ///// 生成目录树
        ///// </summary>
        //private void BuildTree()
        //{
        //    FileTree.BeginUpdate();

        //    //存放树节点的栈
        //    Stack<TreeNode> skNode = new Stack<TreeNode>();
        //    int imageIndex = 0;

        //    //添加磁盘列表
        //    List<string> drives = new List<string>();
        //    drives.Add(Directory.GetLogicalDrives().LastOrDefault());
        //    for (int i = 0; i < drives.Count; i++)
        //    {
        //        //每个节点的Text存放目录名，Name存放全路径
        //        TreeNode node = new TreeNode(drives[i], 0, 0);
        //        node.Name = drives[i];

        //        FileTree.Nodes.Add(node);
        //        skNode.Push(node);
        //    }

        //    while (skNode.Count > 0)
        //    {
        //        //弹出栈顶目录，并获取路径
        //        TreeNode curNode = skNode.Pop();
        //        string path = curNode.Name;

        //        FileInfo fInfo = new FileInfo(path);
        //        if ((fInfo.Attributes & FileAttributes.Directory) != 0)
        //        {
        //            string[] subDirs = null;
        //            string[] subFiles = null;
        //            try
        //            {
        //                //获取当前目录下的所有子目录和文件
        //                subDirs = Directory.GetDirectories(path);
        //                subFiles = Directory.GetFiles(path);
        //            }
        //            catch
        //            { }

        //            if (subDirs != null && subFiles != null)
        //            {
        //                //目录入栈
        //                for (int i = 0; i < subDirs.Length; i++)
        //                {
        //                    string dirName = Path.GetFileName(subDirs[i]);
        //                    TreeNode dirNode = new TreeNode(dirName, 1, 1);
        //                    dirNode.Name = subDirs[i];

        //                    curNode.Nodes.Add(dirNode);
        //                    skNode.Push(dirNode);
        //                }

        //                //文件无需入栈
        //                for (int i = 0; i < subFiles.Length; i++)
        //                {
        //                    string fileName = Path.GetFileName(subFiles[i]);
        //                    curNode.Nodes.Add(subFiles[i], fileName, 2);
        //                }
        //            }
        //        }
        //    }

        //    FileTree.EndUpdate();
        //}

        ///// <summary>
        ///// 生成目录树
        ///// </summary>
        //private void BuildTree2()
        //{
        //    FileTree.BeginUpdate();

        //    Stack<DomNode> stackDomNode=new Stack<DomNode>();
        //    foreach (var firstNode in this.htmlDocument.DocumentNode.ChildNodes)
        //    {
        //        DomNode domNode = new DomNode();
        //        string text = firstNode.Name;
        //        if (firstNode.HasAttributes)
        //        {
        //            foreach (var attr in firstNode.Attributes)
        //            {
        //                text += nbsp + attr.Name + "=" + attr.Value + nbsp;
        //            }
        //        }

        //        domNode.TreeNode = new TreeNode(text);
        //        domNode.HtmlNode = firstNode;
        //        FileTree.Nodes.Add(domNode.TreeNode);
        //        stackDomNode.Push(domNode);
        //        RootDomNode = domNode;
        //    }

        //    FileTree.EndUpdate();

        //    while (stackDomNode.Count>0)
        //    {
        //        DomNode curDomNode = stackDomNode.Pop();
        //        if(curDomNode.HtmlNode.ChildNodes.Count>0)
        //        {
        //            foreach (var node in curDomNode.HtmlNode.ChildNodes)
        //            {
        //                DomNode domNode = new DomNode();
        //                string text = node.Name;
        //                if (node.HasAttributes)
        //                {
        //                    foreach (var attr in node.Attributes)
        //                    {
        //                        text += nbsp + attr.Name + "=" + attr.Value + nbsp;
        //                    }
        //                }

        //                domNode.TreeNode = new TreeNode(text);
        //                domNode.HtmlNode = node;
        //                curDomNode.TreeNode.Nodes.Add(domNode.TreeNode);
        //                stackDomNode.Push(domNode);
        //            }
        //        }
        //    }

        //    FileTree.EndUpdate();
        //}

        /// <summary>
        /// 生成目录树
        /// </summary>
        private void BuildTree3()
        {
            HtmlTree.BeginUpdate();
            HtmlTree.Nodes.Clear();
            Stack<TreeNodeEx> stackDomNode = new Stack<TreeNodeEx>();
            foreach (var firstNode in this.htmlDocument.DocumentNode.ChildNodes)
            {
                
                string text = firstNode.Name;
                if (firstNode.HasAttributes)
                {
                    foreach (var attr in firstNode.Attributes)
                    {
                        text += nbsp + attr.Name + "=" + attr.Value + nbsp;
                    }
                }
                TreeNodeEx domNode = new TreeNodeEx(text);
                
                domNode.HtmlNode = firstNode;
                HtmlTree.Nodes.Add(domNode);
                stackDomNode.Push(domNode);
            }

            while (stackDomNode.Count > 0)
            {
                TreeNodeEx curDomNode = stackDomNode.Pop();
                if (curDomNode.HtmlNode.ChildNodes.Count > 0)
                {
                    foreach (var node in curDomNode.HtmlNode.ChildNodes)
                    {
                        var text = string.Empty;
                        if (node.NodeType == HtmlNodeType.Text || node.NodeType == HtmlNodeType.Comment)
                        {
                            text = node.InnerText;
                        }
                        else
                        {
                            text = node.Name;
                            if (node.HasAttributes)
                            {
                                foreach (var attr in node.Attributes)
                                {
                                    text += nbsp + attr.Name + "=" + attr.Value + nbsp;
                                }
                            }
                        }
                        if(!string.IsNullOrEmpty(text.Trim()))
                        {
                            TreeNodeEx domNode = new TreeNodeEx(text.Trim());
                            domNode.HtmlNode = node;
                            domNode.ToolTipText = node.XPath;
                            curDomNode.Nodes.Add(domNode);
                            stackDomNode.Push(domNode); 
                        }
                        
                    }
                }
            }
            HtmlTree.ExpandAll();
            HtmlTree.EndUpdate();
        }


        // 非递归寻找
        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strValue) return tnParent;
            else if (tnParent.Nodes.Count == 0) return null;
            TreeNode tnCurrent, tnCurrentPar;

            //Init node
            tnCurrentPar = tnParent;
            tnCurrent = tnCurrentPar.FirstNode;
            while (tnCurrent != null && tnCurrent != tnParent)
            {
                while (tnCurrent != null)
                {
                    if (tnCurrent.Text == strValue) return tnCurrent;
                    else if (tnCurrent.Nodes.Count > 0)
                    {
                        //Go into the deepest node in current sub-path
                        tnCurrentPar = tnCurrent;
                        tnCurrent = tnCurrent.FirstNode;
                    }
                    else if (tnCurrent != tnCurrentPar.LastNode)
                    {
                        //Goto next sible node
                        tnCurrent = tnCurrent.NextNode;
                    }
                    else
                        break;
                }

                //Go back to parent node till its has next sible node
                while (tnCurrent != tnParent && tnCurrent == tnCurrentPar.LastNode)
                {
                    tnCurrent = tnCurrentPar;
                    tnCurrentPar = tnCurrentPar.Parent;
                }
                //Goto next sible node

                if (tnCurrent != tnParent)
                    tnCurrent = tnCurrent.NextNode;
            }

            return null;
        }


        // 递归
        private TreeNodeEx FindNodeExt(TreeNodeEx tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.HtmlNode.InnerText==strValue) return tnParent;
            TreeNodeEx tnRet = null;
            foreach (var tn in tnParent.Nodes)
            {
                var treeNodeEx =tn as TreeNodeEx;
                tnRet = FindNodeExt(treeNodeEx, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }

        // 递归
        private TreeNodeEx FindNodeExt(TreeNodeEx tnParent, SelectedElement strValue)
        {
            if (tnParent == null) return null;

            //if (tnParent.HtmlNode.InnerText == strValue) return tnParent;
            if(tnParent.HtmlNode.Name==strValue.tagName)
            {
                if(tnParent.HtmlNode.InnerText==strValue.innerText)
                {
                    return tnParent;
                }
            }

            TreeNodeEx tnRet = null;
            foreach (var tn in tnParent.Nodes)
            {
                var treeNodeEx = tn as TreeNodeEx;

                tnRet = FindNodeExt(treeNodeEx, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }
    }

    public class DomNode
    {
        public TreeNode TreeNode { get; set; }

        public HtmlNode HtmlNode { get; set; }

        public List<DomNode>  ChildsNode { get; set; }
    }

    public class TreeNodeEx:TreeNode
    {
        public TreeNodeEx(string text)
            : base(text)
        {
        }

        public HtmlNode HtmlNode { get; set; }
    }

    public class SelectedElement
    {
        public string tagName { get; set; }

        public string className { get; set; }

        public string id { get; set; }

        public string innerHTML { get; set; }

        public string innerText { get; set; }
    }

    public class ApplicationSettings
    {
        private static List<string> urlHistorys;
        public static List<string> UrlHistorys 
        { 
            get
            {
                if (urlHistorys == null)
                {
                    urlHistorys = new List<string>();
                    var filePath = Environment.CurrentDirectory + "\\" + "urls.json";
                    var data = string.Empty;
                    if(File.Exists(filePath))
                    {
                        data=File.ReadAllText(filePath);
                    }
                    else
                    {
                        File.WriteAllText(filePath,string.Empty);
                    }
                    urlHistorys = JsonHelper.FromJson<List<string>>(data) ?? new List<string>();
                }
                return urlHistorys;
            }
            set
            {
                urlHistorys = value;
            }
        } 
    }
}
