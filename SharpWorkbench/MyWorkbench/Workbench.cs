using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MyWorkbench
{
    public class Workbench : Form
    {
        static Workbench instance;

        public static Workbench Instance
        {
            get
            {
                return instance;
            }
        }

        public static void InitializeWorkbench()
        {
            instance = new Workbench();
        }

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;

        private Workbench()
        {
            // restore form location from last session
            FormLocationHelper.Apply(this, "StartupFormPosition", true);

            dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            dockPanel.DocumentStyle = DocumentStyle.SystemMdi;

            var  formOne = new FormOne();
            formOne.Text = "测试1";
            formOne.Dock=DockStyle.Fill;
            formOne.Show(dockPanel, DockState.DockLeft);

            var formTwo = new FormOne();
            formTwo.Text = "测试2";
            formTwo.Dock = DockStyle.Fill;
            formTwo.Show(dockPanel, DockState.DockRight);

            dockPanel.Dock=DockStyle.Fill;
            dockPanel.ResumeLayout(true, true);
            this.Controls.Add(dockPanel);
            
            Application.Idle += OnApplicationIdle;
        }

        void OnApplicationIdle(object sender, EventArgs e)
        {
            // Use the Idle event to update the status of menu and toolbar.
            // Depending on your application and the number of menu items with complex conditions,
            // you might want to update the status less frequently.
        }
    }
}
