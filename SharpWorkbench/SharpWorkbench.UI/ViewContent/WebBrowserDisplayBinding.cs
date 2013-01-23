using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;
using SharpWorkbench.Core.ViewContent;
using SharpWorkbench.Core.Workbench;
using SharpWorkbench.UI.WorkBench;

namespace SharpWorkbench.UI.ViewContent
{
    public class BrowserDisplayBinding : IDisplayBinding
    {
        public bool CanCreateContentForFile(string fileName)
        {
            fileName = fileName.ToLower();
            return fileName.StartsWith("http:")
                || fileName.StartsWith("https:")
                || fileName.StartsWith("ftp:")
                || fileName.StartsWith("browser://")
                || fileName.EndsWith(".htm")
                || fileName.EndsWith(".html");
        }

        public IViewContent CreateContentForFile(string fileName)
        {
            BrowserPane browserPane = new BrowserPane();
            if (fileName.StartsWith("browser://"))
            {
                browserPane.Load(fileName.Substring("browser://".Length));
            }
            else
            {
                browserPane.Load(fileName);
            }
            return browserPane;
        }
    }

	public class BrowserPane : AbstractViewContent
	{
		HtmlViewPane htmlViewPane;

        protected BrowserPane(bool showNavigation)
        {
            htmlViewPane = new HtmlViewPane(showNavigation);

            htmlViewPane.WebBrowser.DocumentTitleChanged += new EventHandler(TitleChanged);
            htmlViewPane.Closed += delegate
            {
                WorkbenchWindow.CloseWindow(true);
            };

            TitleChanged(null, null);
        }

        void TitleChanged(object sender, EventArgs e)
        {
            string title = htmlViewPane.WebBrowser.DocumentTitle;
            if (title != null)
                title = title.Trim();
            if (title == null || title.Length == 0)
                TitleName = "Browser";
            else
                TitleName = title;
        }

        public BrowserPane(Uri uri)
            : this(true)
        {
            htmlViewPane.Navigate(uri);
        }

        public BrowserPane()
            : this(true)
        {
        }

		public HtmlViewPane HtmlViewPane {
			get {
				return htmlViewPane;
			}
		}
				
		public Uri Url {
			get {
				return htmlViewPane.Url;
			}
		}
		
        public override Control Control
        {
            get
            {
                return htmlViewPane;
            }
        }

        public override void Load(string fileName)
        {
            FileName = fileName;
            htmlViewPane.Navigate(fileName);
        }

        public override void  Dispose()
        {
            htmlViewPane.Dispose();
        }

    }
	
	public class HtmlViewPane : UserControl
	{
        const string sButtonsPath = "/SharpDevelop/ViewContent/Browser/Toolbar";
        const string DefaultHomepage = "http://www.cnblogs.com/michael-zhang";
        const string DefaultSearchUrl = "http://www.google.com/";

        ExtendedWebBrowser webBrowser = null;
        ToolStrip toolStrip;
        Control urlBox;
        public event EventHandler Closed;

		public HtmlViewPane(bool showNavigation)
		{
			Dock = DockStyle.Fill;
			Size = new Size(500, 500);
			
			webBrowser = new ExtendedWebBrowser();
			webBrowser.Dock = DockStyle.Fill;
            webBrowser.NewWindowExtended += delegate(object sender, NewWindowExtendedEventArgs e)
            {
                e.Cancel = true;
                WorkbenchSingleton.Workbench.ShowView(new BrowserPane(e.Url));
            };
            webBrowser.Navigated += delegate
            {
                // do not use e.Url (frames!)
                urlBox.Text = webBrowser.Url.ToString();

                // Update toolbar:
                foreach (object o in toolStrip.Items)
                {
                    IStatusUpdate up = o as IStatusUpdate;
                    if (up != null)
                        up.UpdateStatus();
                }
            };
			Controls.Add(webBrowser);
			
			if (showNavigation) {
                toolStrip = ToolbarService.CreateToolStrip(this, sButtonsPath);
                toolStrip.GripStyle = ToolStripGripStyle.Hidden;
                Controls.Add(toolStrip);
			}
		}

		public void SetUrlComboBox(ComboBox comboBox)
		{
            this.urlBox = comboBox;
            urlBox.KeyUp += delegate(object sender, KeyEventArgs e)
            {
                Control ctl = (Control)sender;
                if (e.KeyData == Keys.Return)
                {
                    e.Handled = true;
                    UrlBoxNavigate(ctl);
                }
            };

			comboBox.DropDownStyle = ComboBoxStyle.DropDown;
			comboBox.Items.Clear();
			comboBox.Items.AddRange(PropertyService.Get("Browser.URLBoxHistory", new string[0]));
			comboBox.AutoCompleteMode      = AutoCompleteMode.Suggest;
			comboBox.AutoCompleteSource    = AutoCompleteSource.HistoryList;
		}

        public ExtendedWebBrowser WebBrowser
        {
            get
            {
                return webBrowser;
            }
        }

		public Uri Url {
			get {
				if (webBrowser.Url == null)
					return new Uri("about:blank");
                return webBrowser.Url;
			}
		}

        public void Navigate(string url)
        {
            webBrowser.Navigate(new Uri(url));
        }

        public void Navigate(Uri url)
        {
            webBrowser.Navigate(url);
        }

        void UrlBoxNavigate(Control ctl)
        {
            string text = ctl.Text.Trim();
            if (text.IndexOf(':') < 0)
            {
                text = "http://" + text;
            }
            Navigate(text);
            ComboBox comboBox = ctl as ComboBox;
            if (comboBox != null)
            {
                comboBox.Items.Remove(text);
                comboBox.Items.Insert(0, text);
                // Add to URLBoxHistory:
                string[] history = PropertyService.Get("Browser.URLBoxHistory", new string[0]);
                int pos = Array.IndexOf(history, text);
                if (pos < 0 && history.Length >= 20)
                {
                    pos = history.Length - 1; // remove last entry and insert new at the beginning
                }
                if (pos < 0)
                {
                    // insert new item
                    string[] newHistory = new string[history.Length + 1];
                    history.CopyTo(newHistory, 1);
                    history = newHistory;
                }
                else
                {
                    for (int i = pos; i > 0; i--)
                    {
                        history[i] = history[i - 1];
                    }
                }
                history[0] = text;
                PropertyService.Set("Browser.URLBoxHistory", history);
            }
        }

        public void GoHome()
        {
            Navigate(DefaultHomepage);
        }

        public void GoSearch()
        {
            Navigate(DefaultSearchUrl);
        }

        public void Close()
        {
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                webBrowser.Dispose();
            }
        }

	}
}
