// -----------------------------------------------------------------------
// <copyright file="Winformhelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows.Forms;

namespace DotNet.SpiderApplication.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Winformhelper
    {
        public delegate void ControlTextMethod(Control control, string text);
        private void SetControlText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                ControlTextMethod controlTextMethod = new ControlTextMethod(SetControlText);
                control.Invoke(controlTextMethod, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
            Application.DoEvents();
        }
    }
}
