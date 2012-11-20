using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DotNet.SpiderApplication.Client
{
    using DotNet.IoC;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());

            Application.Run(new Main());
        }
    }
}
