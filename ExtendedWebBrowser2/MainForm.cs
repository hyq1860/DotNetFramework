using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExtendedWebBrowser2
{
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1301:AvoidDuplicateAccelerators")]
  partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      _windowManager = new WindowManager(this.tabControl);
      _windowManager.CommandStateChanged += new EventHandler<CommandStateEventArgs>(_windowManager_CommandStateChanged);
      _windowManager.StatusTextChanged += new EventHandler<TextChangedEventArgs>(_windowManager_StatusTextChanged);
    }

    // Update the status text
    void _windowManager_StatusTextChanged(object sender, TextChangedEventArgs e)
    {
      this.toolStripStatusLabel.Text = e.Text;
    }

    // Enable / disable buttons
    void _windowManager_CommandStateChanged(object sender, CommandStateEventArgs e)
    {
      this.forwardToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Forward) == BrowserCommands.Forward);
      this.backToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Back) == BrowserCommands.Back);
      this.printPreviewToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.PrintPreview) == BrowserCommands.PrintPreview);
      this.printPreviewToolStripMenuItem.Enabled = ((e.BrowserCommands & BrowserCommands.PrintPreview) == BrowserCommands.PrintPreview);
      this.printToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Print) == BrowserCommands.Print);
      this.printToolStripMenuItem.Enabled = ((e.BrowserCommands & BrowserCommands.Print) == BrowserCommands.Print);
      this.homeToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Home) == BrowserCommands.Home);
      this.searchToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Search) == BrowserCommands.Search);
      this.refreshToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Reload) == BrowserCommands.Reload);
      this.stopToolStripButton.Enabled = ((e.BrowserCommands & BrowserCommands.Stop) == BrowserCommands.Stop);
    }

    #region Tools menu
    // Executed when the user clicks on Tools -> Options
    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (OptionsForm of = new OptionsForm())
      {
        of.ShowDialog(this);
      }
    }
    // Tools -> Show script errors
    private void scriptErrorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ScriptErrorManager.Instance.ShowWindow();
    }

    #endregion

    #region File Menu

    // File -> Print
    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Print();
    }

    // File -> Print Preview
    private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PrintPreview();
    }

    // File -> Exit
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    // File -> Open URL
    private void openUrlToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (OpenUrlForm ouf = new OpenUrlForm())
      {
        if (ouf.ShowDialog() == DialogResult.OK)
        {
          ExtendedWebBrowser brw = _windowManager.New(false);
          brw.Navigate(ouf.Url);
        }
      }
    }

    // File -> Open File
    private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog ofd = new OpenFileDialog())
      {
        ofd.Filter = Properties.Resources.OpenFileDialogFilter;
        if (ofd.ShowDialog() == DialogResult.OK)
        {
          Uri url = new Uri(ofd.FileName);
          WindowManager.Open(url);
        }
      }
    }
    #endregion

    #region Help Menu
    
    // Executed when the user clicks on Help -> About
    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      About();
    }

    /// <summary>
    /// Shows the AboutForm
    /// </summary>
    private void About()
    {
      using (AboutForm af = new AboutForm())
      {
        af.ShowDialog(this);
      }
    }

    #endregion


    /// <summary>
    /// The WindowManager class
    /// </summary>
    private WindowManager _windowManager;

    // This is handy when all the tabs are closed.
    private void tabControl_VisibleChanged(object sender, EventArgs e)
    {
      if (tabControl.Visible)
      {
        this.panel1.BackColor = SystemColors.Control;
      }
      else
        this.panel1.BackColor = SystemColors.AppWorkspace;
    }

    // Starting the app here...
    private void MainForm_Load(object sender, EventArgs e)
    {
      // Open a new browser window
      _windowManager.New();
    }


    #region Printing & Print Preview
    private void Print()
    {
      ExtendedWebBrowser brw = _windowManager.ActiveBrowser;
      if (brw != null)
        brw.ShowPrintDialog();
    }

    private void PrintPreview()
    {
      ExtendedWebBrowser brw = _windowManager.ActiveBrowser;
      if (brw != null)
        brw.ShowPrintPreviewDialog();
    }
    #endregion

    #region Toolstrip buttons
    private void closeWindowToolStripButton_Click(object sender, EventArgs e)
    {
      this._windowManager.New();
    }

    private void closeToolStripButton_Click(object sender, EventArgs e)
    {
      this._windowManager.Close();
    }

    private void printToolStripButton_Click(object sender, EventArgs e)
    {
      Print();
    }

    private void printPreviewToolStripButton_Click(object sender, EventArgs e)
    {
      PrintPreview();
    }

    private void backToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null && _windowManager.ActiveBrowser.CanGoBack)
        _windowManager.ActiveBrowser.GoBack();
    }

    private void forwardToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null && _windowManager.ActiveBrowser.CanGoForward)
        _windowManager.ActiveBrowser.GoForward();
    }

    private void stopToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null)
      {
        _windowManager.ActiveBrowser.Stop();
      }
      stopToolStripButton.Enabled = false;
    }

    private void refreshToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null)
      {
        _windowManager.ActiveBrowser.Refresh(WebBrowserRefreshOption.Normal);
      }
    }

    private void homeToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null)
        _windowManager.ActiveBrowser.GoHome();
    }

    private void searchToolStripButton_Click(object sender, EventArgs e)
    {
      if (_windowManager.ActiveBrowser != null)
        _windowManager.ActiveBrowser.GoSearch();
    }

    #endregion

   

    public WindowManager WindowManager
    {
      get { return _windowManager; }
    }

  }
}