using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExtendedWebBrowser2.Properties;

namespace ExtendedWebBrowser2
{
  partial class OptionsForm : Form
  {
    public OptionsForm()
    {
      InitializeComponent();
    }

    private void OptionsForm_Load(object sender, EventArgs e)
    {
      SettingsHelper helper = SettingsHelper.Current;
      switch (helper.FilterLevel)
      {
        case PopupBlockerFilterLevel.High:
          this.filterLevelHighRadioButton.Checked = true;
          break;
        case PopupBlockerFilterLevel.Medium:
          this.filterLevelMediumRadioButton.Checked = true;
          break;
        case PopupBlockerFilterLevel.Low:
          this.filterLevelLowRadioButton.Checked = true;
          break;
        default:
          this.filterLevelNoneRadioButton.Checked = true;
          break;
      }
      this.doNotShowScriptErrorsCheckBox.Checked = !helper.ShowScriptErrors;

      // Check if we're working on Windows XP SP2 or higher.
      // Since the .Net Framework 2.0 requires SP2 to be installed,
      // and for Windows 2003 requires SP1 we do not need to check against 
      // the service pack level. We DO need to check the version number.
      if (!(Environment.OSVersion.Version.Major >= 5 && Environment.OSVersion.Version.Minor >= 1))
      {
        // The requirements for the pop-up blocker aren't met. 
        // Disable the groupbox with the settings.
        this.popupBlockerGroupBox.Enabled = false;
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      SettingsHelper helper = SettingsHelper.Current;
      if (this.filterLevelHighRadioButton.Checked)
        helper.FilterLevel = PopupBlockerFilterLevel.High;
      else if (this.filterLevelMediumRadioButton.Checked)
        helper.FilterLevel = PopupBlockerFilterLevel.Medium;
      else if (this.filterLevelLowRadioButton.Checked)
        helper.FilterLevel = PopupBlockerFilterLevel.Low;
      else
        helper.FilterLevel = PopupBlockerFilterLevel.None;
      helper.ShowScriptErrors = !this.doNotShowScriptErrorsCheckBox.Checked;
      helper.Save();
      DialogResult = DialogResult.OK;
      this.Close();
    }

  }
}