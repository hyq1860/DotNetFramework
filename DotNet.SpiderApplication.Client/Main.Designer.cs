namespace DotNet.SpiderApplication.Client
{
    partial class Main
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnGenerateAutoUpdateXml = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGenerateAutoUpdateXml
            // 
            this.btnGenerateAutoUpdateXml.Location = new System.Drawing.Point(446, 282);
            this.btnGenerateAutoUpdateXml.Name = "btnGenerateAutoUpdateXml";
            this.btnGenerateAutoUpdateXml.Size = new System.Drawing.Size(105, 24);
            this.btnGenerateAutoUpdateXml.TabIndex = 2;
            this.btnGenerateAutoUpdateXml.Text = "生成升级文件";
            this.btnGenerateAutoUpdateXml.UseVisualStyleBackColor = true;
            this.btnGenerateAutoUpdateXml.Visible = false;
            this.btnGenerateAutoUpdateXml.Click += new System.EventHandler(this.btnGenerateAutoUpdateXml_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 340);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnGenerateAutoUpdateXml);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGenerateAutoUpdateXml;
        private System.Windows.Forms.StatusStrip statusStrip;
    }
}

