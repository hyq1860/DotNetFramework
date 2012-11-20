using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DotNet.InformationMonitor
{
    using Gecko;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            

            Xpcom.Initialize(XULRunnerLocator.GetXULRunnerLocation());
            //#endif
            Application.ApplicationExit += (sender, e) =>
            {
                Xpcom.Shutdown();
            };
            Application.Run(new Form1());
        }
    }
}
