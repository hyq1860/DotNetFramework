using System;
using System.Collections.Generic;
using System.Text;
using SharpWorkbench.Core.Workbench;

namespace SharpWorkbench.Core.ViewContent
{
    public abstract class AbstractViewContent : IViewContent
    {
        IWorkbenchWindow workbenchWindow = null;
        string _title;
        string _fileName;

        #region IViewContent Members


        public IWorkbenchWindow WorkbenchWindow
        {
            get
            {
                return workbenchWindow;
            }
            set
            {
                workbenchWindow = value;
            }
        }

        public string TitleName
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnTitleNameChanged(EventArgs.Empty);
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public event EventHandler TitleNameChanged;

        protected virtual void OnTitleNameChanged(EventArgs e)
        {
            if (TitleNameChanged != null)
            {
                TitleNameChanged(this, e);
            }
        }

        public virtual System.Windows.Forms.Control Control
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public virtual void Load(string fileName)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
