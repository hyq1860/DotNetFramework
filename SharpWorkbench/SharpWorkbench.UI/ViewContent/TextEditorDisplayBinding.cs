using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

using ICSharpCode.Core;
using SharpWorkbench.Core.ViewContent;
using SharpWorkbench.Core.Workbench;

namespace SharpWorkbench.UI.ViewContent
{
	public class TextViewDisplayBinding : IDisplayBinding
	{		
		public virtual bool CanCreateContentForFile(string fileName)
		{
			return true;
		}

		public virtual IViewContent CreateContentForFile(string fileName)
		{
			TextViewDisplayBindingWrapper b2 = new TextViewDisplayBindingWrapper();
			
			b2.Load(fileName);
			return b2;
		}
		
	}
	
	public class TextViewDisplayBindingWrapper : AbstractViewContent
	{
        RichTextBox _rtb;

        public override Control Control
        {
            get
            {
                return _rtb;
            }
        }

        public override void Load(string fileName)
        {
            _rtb = new RichTextBox();
            StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
            string s = sr.ReadToEnd();
            sr.Close();
            _rtb.Text = s;
            _rtb.Dock = DockStyle.Fill;
            _rtb.ReadOnly = true;
            _rtb.Font = new Font("宋体", 11);
            FileName = fileName;
            TitleName = Path.GetFileName(fileName);
        }

        public override void Dispose()
        {
            _rtb.Dispose();
            _rtb = null;
        }

    }
}
