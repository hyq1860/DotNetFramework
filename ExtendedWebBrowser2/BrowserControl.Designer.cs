namespace ExtendedWebBrowser2
{
  partial class BrowserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserControl));
      this.containerPanel = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.goButton = new System.Windows.Forms.Button();
      this.addressTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // containerPanel
      // 
      resources.ApplyResources(this.containerPanel, "containerPanel");
      this.containerPanel.Name = "containerPanel";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.goButton);
      this.panel2.Controls.Add(this.addressTextBox);
      this.panel2.Controls.Add(this.label1);
      resources.ApplyResources(this.panel2, "panel2");
      this.panel2.Name = "panel2";
      // 
      // goButton
      // 
      resources.ApplyResources(this.goButton, "goButton");
      this.goButton.Name = "goButton";
      this.goButton.UseVisualStyleBackColor = false;
      this.goButton.Click += new System.EventHandler(this.goButton_Click);
      // 
      // addressTextBox
      // 
      resources.ApplyResources(this.addressTextBox, "addressTextBox");
      this.addressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.addressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
      this.addressTextBox.Name = "addressTextBox";
      this.addressTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.addressTextBox_KeyUp);
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // BrowserControl
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.containerPanel);
      this.Controls.Add(this.panel2);
      this.Name = "BrowserControl";
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel containerPanel;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button goButton;
    private System.Windows.Forms.TextBox addressTextBox;
    private System.Windows.Forms.Label label1;
  }
}
