using System;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI;
using SharpWorkbench.Core.Workbench;
using SharpWorkbench.Core.ViewContent;
using WeifenLuo.WinFormsUI.Docking;

namespace SharpWorkbench.UI.WorkBench
{
	public class SdiWorkspaceWindow : DockContent, IWorkbenchWindow
	{
		//readonly static string contextMenuPath = "/SharpDevelop/Workbench/OpenFileTab/ContextMenu";
		IViewContent content;

        public SdiWorkspaceWindow(IViewContent content)
        {
            this.content = content;

            content.WorkbenchWindow = this;

            content.TitleNameChanged += new EventHandler(SetTitleEvent);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            //this.DockableAreas = WeifenLuo.WinFormsUI.DockAreas.Document;
            this.DockPadding.All = 2;

            SetTitleEvent(this, EventArgs.Empty);
            //this.TabPageContextMenu = MenuService.CreateContextMenu(this, contextMenuPath);

            content.Control.Dock = DockStyle.Fill;
            Controls.Add(content.Control);
        }

        protected override string GetPersistString()
        {
            return "ViewContent|" + content.FileName;
        }
		
		protected override Size DefaultSize {
			get {
				return Size.Empty;
			}
		}

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                WorkbenchSingleton.Workbench.CloseView(content);
            }
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (content != null)
					DetachContent();
				if (this.TabPageContextMenu != null) {
					this.TabPageContextMenu.Dispose();
					this.TabPageContextMenu = null;
				}
			}
			// DetachContent must be called before the controls are disposed
			base.Dispose(disposing);
		}

        void SetTitleEvent(object sender, EventArgs e)
        {
            if (content == null)
            {
                return;
            }
            SetToolTipText();
            string newTitle = content.TitleName;

            if (content.IsReadOnly)
            {
                newTitle += "+";
            }

            if (newTitle != Title)
            {
                Title = newTitle;
            }
        }
		
		void SetToolTipText()
		{
			if (content != null) {
				try {
					if (content.FileName != null && content.FileName.Length > 0) {
						base.ToolTipText = Path.GetFullPath(content.FileName);
					} else {
						base.ToolTipText = null;
					}
				} catch (Exception) {
					base.ToolTipText = content.FileName;
				}
			} else {
				base.ToolTipText = null;
			}
        }

        #region Implementation of IWorkbenchWindow

        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
                OnTitleChanged(EventArgs.Empty);
            }
        }

        public IViewContent ViewContent
        {
            get
            {
                return content;
            }
        }
		
        public IViewContent ActiveViewContent
        {
            get
            {
                return content;
            }
        }

		public void DetachContent()
		{
			content = null;
			Controls.Clear();
		}
		
		public bool CloseWindow(bool force)
		{
			Dispose();
            return true;
		}

        public void SelectWindow()
        {
            Visible = true;
            Show();
        }

		public virtual void RedrawContent()
		{
            ;
		}
		
		protected virtual void OnTitleChanged(EventArgs e)
		{
			if (TitleChanged != null) {
				TitleChanged(this, e);
			}
		}
		
		public event EventHandler TitleChanged;

        #endregion
    }
}
