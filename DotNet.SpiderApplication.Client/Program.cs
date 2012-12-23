using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            //Create a new mutex using specific mutex name
            //http://www.csharpwin.com/csharpspace/10656r1776.shtml
            bool bCreatedNew;
            var mutex = new Mutex(false, "DotNet.SpiderApplication.Client", out bCreatedNew);
            if (bCreatedNew)
            {
                Application.Run(new Main());
            }
        }
    }
}
