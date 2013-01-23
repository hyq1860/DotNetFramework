using System;
using System.Collections;
using SharpWorkbench.Core.ViewContent;

namespace SharpWorkbench.Core.ViewContent
{
	/// <summary>
	/// The IWorkbenchWindow is the basic interface to a window which
	/// shows a view (represented by the IViewContent object).
	/// </summary>
	public interface IWorkbenchWindow
	{
		/// <summary>
		/// The window title.
		/// </summary>
		string Title {
			get;
			set;
		}
		
		/// <summary>
		/// The primary view content in this window.
		/// </summary>
		IViewContent ViewContent {
			get;
		}
		
		/// <summary>
		/// The current view content which is shown inside this window.
		/// </summary>
		IViewContent ActiveViewContent {
			get;
		}
		
		/// <summary>
		/// Closes the window, if force == true it closes the window
		/// without ask, even the content is dirty.
		/// </summary>
		/// <returns>true, if window is closed</returns>
		bool CloseWindow(bool force);
		
		/// <summary>
		/// Brings this window to front and sets the user focus to this
		/// window.
		/// </summary>
		void SelectWindow();
		
		void RedrawContent();
		
        /// <summary>
        /// Is called when the title of this window has changed.
        /// </summary>
        event EventHandler TitleChanged;
	}
}
