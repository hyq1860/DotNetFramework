namespace ExtendedWebBrowser2
{
  partial class MainForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scriptErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.closeWindowToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.backToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.forwardToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.homeToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.closeToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.searchToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.panel1 = new System.Windows.Forms.Panel();
      this.menuStrip.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
      resources.ApplyResources(this.menuStrip, "menuStrip");
      this.menuStrip.Name = "menuStrip";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openUrlToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
      // 
      // openUrlToolStripMenuItem
      // 
      resources.ApplyResources(this.openUrlToolStripMenuItem, "openUrlToolStripMenuItem");
      this.openUrlToolStripMenuItem.Name = "openUrlToolStripMenuItem";
      this.openUrlToolStripMenuItem.Click += new System.EventHandler(this.openUrlToolStripMenuItem_Click);
      // 
      // openFileToolStripMenuItem
      // 
      resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
      this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
      this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      // 
      // printToolStripMenuItem
      // 
      resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
      // 
      // printPreviewToolStripMenuItem
      // 
      resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
      this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
      this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptErrorToolStripMenuItem,
            this.toolStripSeparator5,
            this.optionsToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
      // 
      // scriptErrorToolStripMenuItem
      // 
      resources.ApplyResources(this.scriptErrorToolStripMenuItem, "scriptErrorToolStripMenuItem");
      this.scriptErrorToolStripMenuItem.Name = "scriptErrorToolStripMenuItem";
      this.scriptErrorToolStripMenuItem.Click += new System.EventHandler(this.scriptErrorToolStripMenuItem_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
      this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
      // 
      // aboutToolStripMenuItem
      // 
      resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // toolStrip
      // 
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeWindowToolStripButton,
            this.toolStripSeparator3,
            this.backToolStripButton,
            this.forwardToolStripButton,
            this.toolStripSeparator1,
            this.stopToolStripButton,
            this.refreshToolStripButton,
            this.toolStripSeparator2,
            this.homeToolStripButton,
            this.closeToolStripButton,
            this.searchToolStripButton,
            this.toolStripSeparator4,
            this.printToolStripButton,
            this.printPreviewToolStripButton});
      resources.ApplyResources(this.toolStrip, "toolStrip");
      this.toolStrip.Name = "toolStrip";
      // 
      // closeWindowToolStripButton
      // 
      this.closeWindowToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.closeWindowToolStripButton, "closeWindowToolStripButton");
      this.closeWindowToolStripButton.Name = "closeWindowToolStripButton";
      this.closeWindowToolStripButton.Click += new System.EventHandler(this.closeWindowToolStripButton_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
      // 
      // backToolStripButton
      // 
      resources.ApplyResources(this.backToolStripButton, "backToolStripButton");
      this.backToolStripButton.Name = "backToolStripButton";
      this.backToolStripButton.Click += new System.EventHandler(this.backToolStripButton_Click);
      // 
      // forwardToolStripButton
      // 
      this.forwardToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.forwardToolStripButton, "forwardToolStripButton");
      this.forwardToolStripButton.Name = "forwardToolStripButton";
      this.forwardToolStripButton.Click += new System.EventHandler(this.forwardToolStripButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
      // 
      // stopToolStripButton
      // 
      this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.stopToolStripButton, "stopToolStripButton");
      this.stopToolStripButton.Name = "stopToolStripButton";
      this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripButton_Click);
      // 
      // refreshToolStripButton
      // 
      this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.refreshToolStripButton, "refreshToolStripButton");
      this.refreshToolStripButton.Name = "refreshToolStripButton";
      this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
      // 
      // homeToolStripButton
      // 
      this.homeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.homeToolStripButton, "homeToolStripButton");
      this.homeToolStripButton.Name = "homeToolStripButton";
      this.homeToolStripButton.Click += new System.EventHandler(this.homeToolStripButton_Click);
      // 
      // closeToolStripButton
      // 
      this.closeToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.closeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
      this.closeToolStripButton.Name = "closeToolStripButton";
      this.closeToolStripButton.Click += new System.EventHandler(this.closeToolStripButton_Click);
      // 
      // searchToolStripButton
      // 
      this.searchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.searchToolStripButton, "searchToolStripButton");
      this.searchToolStripButton.Name = "searchToolStripButton";
      this.searchToolStripButton.Click += new System.EventHandler(this.searchToolStripButton_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
      // 
      // printToolStripButton
      // 
      this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.printToolStripButton, "printToolStripButton");
      this.printToolStripButton.Name = "printToolStripButton";
      this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
      // 
      // printPreviewToolStripButton
      // 
      this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.printPreviewToolStripButton, "printPreviewToolStripButton");
      this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
      this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Name = "statusStrip1";
      // 
      // toolStripStatusLabel
      // 
      resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      this.toolStripStatusLabel.Spring = true;
      // 
      // tabControl
      // 
      resources.ApplyResources(this.tabControl, "tabControl");
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.VisibleChanged += new System.EventHandler(this.tabControl_VisibleChanged);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.tabControl);
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.Name = "panel1";
      // 
      // MainForm
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip);
      this.Controls.Add(this.menuStrip);
      this.MainMenuStrip = this.menuStrip;
      this.Name = "MainForm";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openUrlToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton backToolStripButton;
    private System.Windows.Forms.ToolStripButton forwardToolStripButton;
    private System.Windows.Forms.ToolStripButton stopToolStripButton;
    private System.Windows.Forms.ToolStripButton refreshToolStripButton;
    private System.Windows.Forms.ToolStripButton homeToolStripButton;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton closeToolStripButton;
    private System.Windows.Forms.ToolStripButton closeWindowToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
    private System.Windows.Forms.ToolStripButton searchToolStripButton;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    private System.Windows.Forms.ToolStripMenuItem scriptErrorToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
  }
}

