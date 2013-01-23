using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.Core;
using WeifenLuo.WinFormsUI;
using SharpWorkbench.Core.Pad;
using WeifenLuo.WinFormsUI.Docking;

namespace SharpWorkbench.UI.WorkBench
{
    class PadContentWrapper : DockContent
    {
        PadDescriptor padDescriptor;
        bool isInitialized = false;
        internal bool allowInitialize = false;

        public IPadContent PadContent
        {
            get
            {
                return padDescriptor.PadContent;
            }
        }

        public PadContentWrapper(PadDescriptor padDescriptor)
        {
            if (padDescriptor == null)
                throw new ArgumentNullException("padDescriptor");
            this.padDescriptor = padDescriptor;
            //this.DockableAreas = ((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft) |
            //                        WeifenLuo.WinFormsUI.DockAreas.DockRight) |
            //                       WeifenLuo.WinFormsUI.DockAreas.DockTop) |
            //                      WeifenLuo.WinFormsUI.DockAreas.DockBottom);
            this.DockAreas = ((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) |
                                    WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) |
                                   WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) |
                                  WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom);
            HideOnClose = true;
        }

        public void DetachContent()
        {
            Controls.Clear();
            padDescriptor = null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && Width > 0)
                ActivateContent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Visible && Width > 0)
                ActivateContent();
        }

        /// <summary>
        /// Enables initializing the content. This is used to prevent initializing all view
        /// contents when the layout configuration is changed.
        /// </summary>
        public void AllowInitialize()
        {
            allowInitialize = true;
            if (Visible && Width > 0)
                ActivateContent();
        }

        void ActivateContent()
        {
            if (!allowInitialize)
                return;
            if (!isInitialized)
            {
                isInitialized = true;
                IPadContent content = padDescriptor.PadContent;
                if (content == null)
                    return;
                Control control = content.Control;
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }

        protected override string GetPersistString()
        {
            return padDescriptor.Class;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (padDescriptor != null)
                {
                    padDescriptor.Dispose();
                    padDescriptor = null;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PadContentWrapper
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "PadContentWrapper";
            this.ResumeLayout(false);

        }
    }
}