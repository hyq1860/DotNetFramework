using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExtendedWebBrowser2
{
  partial class OpenUrlForm : Form
  {
    public OpenUrlForm()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      // Check if a valid URL was typed.
      Uri result = null;
      try
      {
        result = new Uri(this.addressTextBox.Text);
      }
      catch (UriFormatException)
      { 
        // Eat this
      }
      if (result == null)
      {
        // Maybe the user forgot 'http://'
        try
        {
          result = new Uri("http://" + this.addressTextBox.Text);
        }
        catch (UriFormatException)
        {
          // Eat this
        }
        if (result == null)
        {
          this.invalidAddressLabel.Visible = true;
          return;
        }
      }
      if (result.Scheme == "http" || result.Scheme == "https" || result.Scheme == "file")
      {
        // We got a valid address
        _url = result;
        DialogResult = DialogResult.OK;
        Close();
      }
      else
      {
        // The URL might be valid, but the protocol is not supported
        this.invalidAddressLabel.Visible = false;
        return;
      }
    }

    private Uri _url;
    public Uri Url
    {
      get { return _url; }
    }

    private void OpenUrlForm_Load(object sender, EventArgs e)
    {
      this.invalidAddressLabel.Visible = false;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      this.invalidAddressLabel.Visible = false;
    }


  }
}