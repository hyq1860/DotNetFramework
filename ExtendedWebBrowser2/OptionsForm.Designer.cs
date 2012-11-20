namespace ExtendedWebBrowser2
{
  partial class OptionsForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
      this.popupBlockerGroupBox = new System.Windows.Forms.GroupBox();
      this.filterLevelHighRadioButton = new System.Windows.Forms.RadioButton();
      this.filterLevelMediumRadioButton = new System.Windows.Forms.RadioButton();
      this.filterLevelLowRadioButton = new System.Windows.Forms.RadioButton();
      this.filterLevelNoneRadioButton = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.doNotShowScriptErrorsCheckBox = new System.Windows.Forms.CheckBox();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.popupBlockerGroupBox.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // popupBlockerGroupBox
      // 
      this.popupBlockerGroupBox.Controls.Add(this.filterLevelHighRadioButton);
      this.popupBlockerGroupBox.Controls.Add(this.filterLevelMediumRadioButton);
      this.popupBlockerGroupBox.Controls.Add(this.filterLevelLowRadioButton);
      this.popupBlockerGroupBox.Controls.Add(this.filterLevelNoneRadioButton);
      resources.ApplyResources(this.popupBlockerGroupBox, "popupBlockerGroupBox");
      this.popupBlockerGroupBox.Name = "popupBlockerGroupBox";
      this.popupBlockerGroupBox.TabStop = false;
      // 
      // filterLevelHighRadioButton
      // 
      resources.ApplyResources(this.filterLevelHighRadioButton, "filterLevelHighRadioButton");
      this.filterLevelHighRadioButton.Name = "filterLevelHighRadioButton";
      this.filterLevelHighRadioButton.TabStop = true;
      this.filterLevelHighRadioButton.UseVisualStyleBackColor = true;
      // 
      // filterLevelMediumRadioButton
      // 
      resources.ApplyResources(this.filterLevelMediumRadioButton, "filterLevelMediumRadioButton");
      this.filterLevelMediumRadioButton.Name = "filterLevelMediumRadioButton";
      this.filterLevelMediumRadioButton.TabStop = true;
      this.filterLevelMediumRadioButton.UseVisualStyleBackColor = true;
      // 
      // filterLevelLowRadioButton
      // 
      resources.ApplyResources(this.filterLevelLowRadioButton, "filterLevelLowRadioButton");
      this.filterLevelLowRadioButton.Name = "filterLevelLowRadioButton";
      this.filterLevelLowRadioButton.TabStop = true;
      this.filterLevelLowRadioButton.UseVisualStyleBackColor = true;
      // 
      // filterLevelNoneRadioButton
      // 
      resources.ApplyResources(this.filterLevelNoneRadioButton, "filterLevelNoneRadioButton");
      this.filterLevelNoneRadioButton.Name = "filterLevelNoneRadioButton";
      this.filterLevelNoneRadioButton.TabStop = true;
      this.filterLevelNoneRadioButton.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.doNotShowScriptErrorsCheckBox);
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // doNotShowScriptErrorsCheckBox
      // 
      resources.ApplyResources(this.doNotShowScriptErrorsCheckBox, "doNotShowScriptErrorsCheckBox");
      this.doNotShowScriptErrorsCheckBox.Name = "doNotShowScriptErrorsCheckBox";
      this.doNotShowScriptErrorsCheckBox.UseVisualStyleBackColor = true;
      // 
      // okButton
      // 
      resources.ApplyResources(this.okButton, "okButton");
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      resources.ApplyResources(this.cancelButton, "cancelButton");
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // OptionsForm
      // 
      this.AcceptButton = this.okButton;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.popupBlockerGroupBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "OptionsForm";
      this.Load += new System.EventHandler(this.OptionsForm_Load);
      this.popupBlockerGroupBox.ResumeLayout(false);
      this.popupBlockerGroupBox.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox popupBlockerGroupBox;
    private System.Windows.Forms.RadioButton filterLevelHighRadioButton;
    private System.Windows.Forms.RadioButton filterLevelMediumRadioButton;
    private System.Windows.Forms.RadioButton filterLevelLowRadioButton;
    private System.Windows.Forms.RadioButton filterLevelNoneRadioButton;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox doNotShowScriptErrorsCheckBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label label1;
  }
}