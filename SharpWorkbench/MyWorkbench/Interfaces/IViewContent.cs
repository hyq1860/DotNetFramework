using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyWorkbench.Interfaces
{
    /// <summary>
    /// Interface for content displayed in the application.
    /// </summary>
    public interface IViewContent
    {
        /// <summary>
        /// Gets the control used to display this view content.
        /// </summary>
        Control Control
        {
            get;
        }

        string Title
        {
            get;
        }

        event EventHandler TitleChanged;

        /// <summary>
        /// Closes the view content. Returns true when the content was closed successfully,
        /// false when closing the content was aborted (e.g. by the user)
        /// </summary>
        bool Close();

        /// <summary>
        /// Saves the content, e.g. to a file. Returns true when the content has been saved successfully.
        /// </summary>
        bool Save();
        /// <summary>
        /// Asks the user to specify the file location/name and saves the content using the new name.
        /// Returns true when the content has been saved successfully.
        /// </summary>
        bool SaveAs();
    }
}
