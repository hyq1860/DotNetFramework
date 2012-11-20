using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WebKit;
using WebKit.DOM;
using WebKit.Interop;

namespace DotNet.InformationMonitor
{
    using Gecko;

    public partial class Form1 : Form
    {
        private WebKitBrowser webKitBrowser;

        public WebKitBrowser WebKitBrowser
        {
            get
            {
                if(webKitBrowser==null)
                {
                    webKitBrowser=new WebKitBrowser();
                    this.Controls.Add(webKitBrowser);
                }
                return webKitBrowser;
            }
        }

        private GeckoWebBrowser _geckoWebBrowser;
        public GeckoWebBrowser GeckoWebBrowser
        {
            get
            {
                if (_geckoWebBrowser == null)
                {
                    _geckoWebBrowser = new GeckoWebBrowser();
                    //panel1.Controls.Add(_geckoWebBrowser);
                }
                return _geckoWebBrowser;
            }
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var data = 0;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeckoWebBrowser.Navigate(textBox1.Text);
            //WebKitBrowser.Navigate(textBox1.Text);
            //WebKitBrowser.DocumentCompleted +=
            //            new WebBrowserDocumentCompletedEventHandler(webKitBrowser1_DocumentCompleted);


            //NodeList bodyCollection = (doc.GetElementsByTagName("body") as NodeList);

            //var text = bodyCollection.FirstOrDefault().TextContent;

        }


        void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string documentContent = WebKitBrowser.DocumentText;
            NodeList bodyCollection = (WebKitBrowser.Document.GetElementsByTagName("body") as NodeList);
            var t=WebKitBrowser.Document.GetElementsByTagName("body").FirstOrDefault().TextContent;

            WebKitBrowser.Document.GetElementById("dataContainer").nodeValue();
            //var t1=WebKitBrowser.GetCurrentElement().InnerText;
            //WebKitBrowser.WebView.setSelectedDOMRange();
        }


        string ProcessXPath(string path)
        {

            try
            {

                var jsstr = "document.evaluate( \"" + path + "\", document, null,  XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.";

                if (path.Contains("text()"))
                {

                    jsstr += "nodeValue";

                }

                else
                {

                    jsstr += "innerHTML";

                }

                var val = WebKitBrowser.StringByEvaluatingJavaScriptFromString(jsstr).Trim().Trim('"');

                return val;

            }

            catch (Exception ex)
            {

                return string.Empty;

            }

        }
    }
}
