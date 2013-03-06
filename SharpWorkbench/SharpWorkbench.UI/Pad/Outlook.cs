// -----------------------------------------------------------------------
// <copyright file="Outlook.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows.Forms;
using SharpWorkbench.Controls;
using SharpWorkbench.Core.Pad;

namespace SharpWorkbench.UI.Pad
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Outlook : UserControl, IPadContent
    {
        public Control Control { get { return this; } }

        public Outlook()
        {
            
            //Control.Controls.Add(NavBar);
        }

        public void RedrawContent()
        {
            if (Control != null)
            {
                
            }
        }
    }
}
