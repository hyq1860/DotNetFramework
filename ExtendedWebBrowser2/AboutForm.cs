using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExtendedWebBrowser2
{
  public partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();
    }

    private void licenseButton_Click(object sender, EventArgs e)
    {
      Process.Start("http://creativecommons.org/licenses/by-sa/2.5/");
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void theWheelLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://www.thewheel.nl");
    }
  }
}