using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Client
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.ServiceModel;
    using System;

    using System.Windows.Forms;

    using IfacesEnumsStructsClasses;

    using csExWB;

     //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
    public class CalculatorCallbackService : ICalculatorCallback
    {
        public void DisplayResult(double result, double x, double y)
        {
            Console.WriteLine("x + y = {2} when x = {0}  and y = {1}", x, y, result);
        }

        public static bool done = false;
        public static List<string> Data = new List<string>();

        public event EventHandler Quit;

        /// <summary>
        /// Raises the <see cref="Quit"/> event
        /// </summary>
        protected void OnQuit()
        {
            EventHandler h = Quit;
            if (null != h)
                h(this, EventArgs.Empty);
        }


        public event EventHandler Processed;

        /// <summary>
        /// Raises the <see cref="Quit"/> event
        /// </summary>
        protected void OnProcess()
        {
            EventHandler h = Processed;
            if (null != h)
                h(this, EventArgs.Empty);
        }

        public void Process(List<string> data)
        {
            Data = data;
            this.OnQuit();
        }

        public void Display()
        {

            //MessageBox.Show(data[data.Count-1]);
            //foreach (var str in data)
            //{
            //    Console.WriteLine(str);
            //}
            OnProcess();
            // Starts a new instance of the program itself
            //Application.Restart();
            //System.Environment.Exit(0);
        }

        #region ISessionCallback Members

        public TimeSpan Renew()
        {
            return SessionUtility.Timeout - (DateTime.Now - SessionUtility.LastActivityTime);
        }

        public void OnSessionKilled(SessionInfo sessionInfo)
        {
            MessageBox.Show("The current session has been killed!", sessionInfo.SessionID.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        public void OnSessionTimeout(SessionInfo sessionInfo)
        {
            MessageBox.Show("The current session timeout!", sessionInfo.SessionID.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        #endregion
    }
}
