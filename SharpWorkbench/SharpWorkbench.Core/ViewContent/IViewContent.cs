using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpWorkbench.Core.Workbench;

namespace SharpWorkbench.Core.ViewContent
{	
	/// <summary>
	/// IViewContent is the base interface for all editable data
	/// inside SharpDevelop.
	/// </summary>
    public interface IViewContent : IDisposable
	{
        /// <summary>
        /// This is the Windows.Forms control for the view.
        /// </summary>
        Control Control
        {
            get;
        }

        /// <summary>
        /// The workbench window in which this view is displayed.
        /// </summary>
        IWorkbenchWindow WorkbenchWindow
        {
            get;
            set;
        }

        /// <summary>
        /// This is the whole name of the content, e.g. the file name or
        /// the url depending on the type of the content.
        /// </summary>
        /// <returns>
        /// Title Name, if not set it returns UntitledName
        /// </returns>
        string TitleName
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the file name (if any) assigned to this view.
        /// </summary>
        string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// If this property returns true the content could not be altered.
        /// </summary>
        bool IsReadOnly
        {
            get;
        }

		/// <summary>
		/// Loads the content from the location <code>fileName</code>
		/// </summary>
		void Load(string fileName);

        /// <summary>
        /// Is called each time the name for the content has changed.
        /// </summary>
        event EventHandler TitleNameChanged;
	}
}
