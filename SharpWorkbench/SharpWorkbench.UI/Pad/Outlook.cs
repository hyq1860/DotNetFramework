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
            var NavBar = new NavBar();
            NavBar.Dock = System.Windows.Forms.DockStyle.Left;
            NavBar.Location = new System.Drawing.Point(0, 85);
            NavBar.Name = "NavBar";
            NavBar.Size = new System.Drawing.Size(160, 600);
            NavBar.TabIndex = 1;
            Control.Controls.Add(NavBar);
        }

        public void RedrawContent()
        {
            if (Control != null)
            {
                
            }
        }
    }
}
