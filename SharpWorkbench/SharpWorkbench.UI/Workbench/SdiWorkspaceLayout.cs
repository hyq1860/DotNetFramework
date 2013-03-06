using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Reflection;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI;
using SharpWorkbench.Core.Pad;
using SharpWorkbench.Core.ViewContent;
using SharpWorkbench.Core.Workbench;
using SharpWorkbench.UI.Pad;
using SharpWorkbench.UI.Common;
using WeifenLuo.WinFormsUI.Docking;

namespace SharpWorkbench.UI.WorkBench
{
    using SharpWorkbench.Controls;

    /// <summary>
	/// This is the a Workspace with a single document interface.
	/// </summary>
	public class SdiWorkbenchLayout : IWorkbenchLayout
	{
        DefaultWorkbench wbForm;

        const string _layoutFile = "LayoutConfigFile.xml";
        DockPanel dockPanel;
        Dictionary<string, PadContentWrapper> contentHash = new Dictionary<string, PadContentWrapper>();
        // prevent setting ActiveContent to null when application loses focus (e.g. because of context menu popup)
        DockContent lastActiveContent;
		
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		public static extern bool LockWindowUpdate(IntPtr hWnd);

        public SdiWorkbenchLayout() { }

        #region Implementation of IWorkbenchLayout

        public IWorkbenchWindow ActiveWorkbenchwindow
        {
            get
            {
                if (dockPanel == null || dockPanel.ActiveDocument == null) // || dockPanel.ActiveDocument.IsDisposed
                {
                    return null;
                }
                return dockPanel.ActiveDocument as IWorkbenchWindow;
            }
        }

        public object ActiveContent
        {
            get
            {
                DockContent activeContent;
                if (dockPanel == null)
                {
                    activeContent = lastActiveContent;
                }
                else
                {
                    activeContent = dockPanel.ActiveContent as DockContent ?? lastActiveContent;
                }
                lastActiveContent = activeContent;

                if (activeContent == null) // || activeContent.IsDisposed
                {
                    return null;
                }
                if (activeContent is IWorkbenchWindow)
                {
                    return ((IWorkbenchWindow)activeContent).ActiveViewContent;
                }

                if (activeContent is PadContentWrapper)
                {
                    return ((PadContentWrapper)activeContent).PadContent;
                }

                return activeContent;
            }
        }

        public void ShowPad(PadDescriptor content, bool bActivatedIt)
        {
            if (content == null)
            {
                return;
            }
            if (!contentHash.ContainsKey(content.Class))
            {
                DockContent newContent = CreateContent(content);
                newContent.Show(dockPanel);
            }
            else
            {
                contentHash[content.Class].Show();
                if (bActivatedIt)
                    contentHash[content.Class].PanelPane.Activate();
            }
        }

        public void Attach(IWorkbench workbench)
        {
            wbForm = (DefaultWorkbench)workbench;
            wbForm.SuspendLayout();
            wbForm.IsMdiContainer = true;

            //dockPanel = new WeifenLuo.WinFormsUI.DockPanel();
            //dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            //dockPanel.ActiveAutoHideContent = null;
            //dockPanel.Dock = DockStyle.Fill;

            //wbForm.Controls.Add(dockPanel);// 将eifenLuo.WinFormsUI.DockPanel dockpanel加入到controls

           
            
            InitOutlook(wbForm);
            wbForm.TopMenu.Dock = System.Windows.Forms.DockStyle.Top;
            wbForm.Controls.Add(wbForm.TopMenu);// 加入菜单
            LoadConfiguration();

            //ShowPads();
            //ShowViewContents();
            //RedrawAllComponents();

            wbForm.ResumeLayout(false);
        }

        private void InitOutlook(DefaultWorkbench wbForm)
        {
            //var NavBar = new NavBar();
            //NavBar.Dock = System.Windows.Forms.DockStyle.Left;
            //NavBar.Location = new System.Drawing.Point(0, 85);
            //NavBar.Name = "NavBar";
            //NavBar.Size = new System.Drawing.Size(160, 600);
            //NavBar.TabIndex = 1;
            //NavBar.ImageButtonClick += new NavBar.ButtonClickHander(this.NavBar_ImageButtonClick);

            wbForm.Controls.Add(wbForm.Outlook);

            var contentView = new System.Windows.Forms.Panel();
            contentView.Dock = System.Windows.Forms.DockStyle.Fill;
            contentView.Location = new System.Drawing.Point(160, 85);
            contentView.Name = "PnlContent";
            contentView.Size = new System.Drawing.Size(830, 600);
            contentView.TabIndex = 2;

            //默认usercontrols
            var defaultControl = new UserControl();
            contentView.Controls.Add(defaultControl);

            wbForm.Controls.Add(contentView);
            //wbForm.Controls.Add(NavBar);
        }

        private void NavBar_ImageButtonClick(object sender, string targetModule)
        {

        }

        public void Detach()
        {
            StoreConfiguration();

            DetachPadContents(true);
            DetachViewContents(true);

            try
            {
                if (dockPanel != null)
                {
                    dockPanel.Dispose();
                    dockPanel = null;
                }
            }
            catch (Exception e)
            {
                MessageService.ShowError(e);
            }
            if (contentHash != null)
            {
                contentHash.Clear();
            }

            wbForm.Controls.Clear();
        }

        public void ShowPad(PadDescriptor content)
        {
            ShowPad(content, false);
        }

		public IWorkbenchWindow ShowView(IViewContent content)
		{
			if (content.WorkbenchWindow is SdiWorkspaceWindow) {
				SdiWorkspaceWindow oldSdiWindow = (SdiWorkspaceWindow)content.WorkbenchWindow;
				if (!oldSdiWindow.IsDisposed) {
					oldSdiWindow.Show(dockPanel);
					return oldSdiWindow;
				}
			}
			if (!content.Control.Visible) {
				content.Control.Visible = true;
			}
			content.Control.Dock = DockStyle.Fill;
			SdiWorkspaceWindow sdiWorkspaceWindow = new SdiWorkspaceWindow(content);
			if (dockPanel != null) {
				sdiWorkspaceWindow.Show(dockPanel);
			}
			
			return sdiWorkspaceWindow;
		}

        public void RedrawAllComponents()
        {
            // redraw correct pad content names (language changed).
            foreach (PadDescriptor padDescriptor in ((IWorkbench)wbForm).PadContentCollection)
            {
                DockContent c = contentHash[padDescriptor.Class];
                if (c != null)
                {
                    c.Text = StringParser.Parse(padDescriptor.Title);
                }
            }
            //RedrawMainMenu();
        }

        public void LoadConfiguration()
        {
            if (dockPanel != null)
            {
                string configPath = FileUtility.Combine(PropertyService.ConfigDirectory, "layouts", _layoutFile);

                if (!File.Exists(configPath))
                    return;

                LockWindowUpdate(wbForm.Handle);
                try
                {
                    dockPanel.LoadFromXml(configPath, new DeserializeDockContent(GetContent));
                }
                catch { }
                finally
                {
                    LockWindowUpdate(IntPtr.Zero);
                }
            }
        }

        public void StoreConfiguration()
        {
            try
            {
                if (dockPanel != null)
                {
                    string configPath = Path.Combine(PropertyService.ConfigDirectory, "layouts");
                    if (!Directory.Exists(configPath))
                        Directory.CreateDirectory(configPath);
                    dockPanel.SaveAsXml(Path.Combine(configPath, _layoutFile));
                }
            }
            catch (Exception e)
            {
                MessageService.ShowError(e);
            }
        }

        #endregion

        #region helper methods

        DockContent GetContent(string padTypeName)
        {
            if (padTypeName.StartsWith("ViewContent|"))
            {
                string filePath = padTypeName.Substring("ViewContent|".Length);
                if (File.Exists(filePath))
                {
                    IWorkbenchWindow contentWindow = FileService.OpenFile(filePath);
                    if (contentWindow is DockContent)
                        return (DockContent)contentWindow;
                    else
                        return null;
                }
                else
                    return null;
            }

            foreach (PadDescriptor content in WorkbenchSingleton.Workbench.PadContentCollection)
            {
                if (content.Class == padTypeName)
                {
                    return CreateContent(content);
                }
            }
            return null;
        }

        void ShowPads()
        {
            foreach (PadDescriptor content in WorkbenchSingleton.Workbench.PadContentCollection)
            {
                if (!contentHash.ContainsKey(content.Class))
                {
                    ShowPad(content);
                }
            }
            // ShowPads could create new pads if new addins have been installed, so we
            // need to call AllowInitialize here instead of in LoadLayoutConfiguration
            foreach (PadContentWrapper content in contentHash.Values)
            {
                content.AllowInitialize();
            }
        }

        void ShowViewContents()
        {
            foreach (IViewContent content in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                ShowView(content);
            }
        }

        void DetachPadContents(bool dispose)
        {
            foreach (PadContentWrapper padContentWrapper in contentHash.Values)
            {
                padContentWrapper.allowInitialize = false;
            }
            foreach (PadDescriptor content in ((DefaultWorkbench)wbForm).PadContentCollection)
            {
                try
                {
                    PadContentWrapper padContentWrapper = contentHash[content.Class];
                    padContentWrapper.DockPanel = null;
                    if (dispose)
                    {
                        padContentWrapper.DetachContent();
                        padContentWrapper.Dispose();
                    }
                }
                catch (Exception e) { MessageService.ShowError(e); }
            }
            if (dispose)
            {
                contentHash.Clear();
            }
        }

        void DetachViewContents(bool dispose)
        {
            foreach (IViewContent viewContent in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                try
                {
                    SdiWorkspaceWindow f = (SdiWorkspaceWindow)viewContent.WorkbenchWindow;
                    f.DockPanel = null;
                    if (dispose)
                    {
                        viewContent.WorkbenchWindow = null;
                        f.DetachContent();
                        f.Dispose();
                    }
                }
                catch (Exception e) { MessageService.ShowError(e); }
            }
        }

        PadContentWrapper CreateContent(PadDescriptor content)
        {
            if (contentHash.ContainsKey(content.Class))
            {
                return contentHash[content.Class];
            }
            ICSharpCode.Core.Properties properties = (ICSharpCode.Core.Properties)PropertyService.Get("Workspace.ViewMementos", new ICSharpCode.Core.Properties());

            PadContentWrapper newContent = new PadContentWrapper(content);
            if (content.Icon != null)
            {
                newContent.Icon = IconService.GetIcon(content.Icon);
            }
            newContent.Text = StringParser.Parse(content.Title);
            contentHash[content.Class] = newContent;
            return newContent;
        }

        #endregion

    }
}
