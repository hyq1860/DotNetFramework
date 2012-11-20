// -----------------------------------------------------------------------
// <copyright file="ThreadServiceHost.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Service.ServiceHostExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;

    public class ThreadServiceHost
    {
        const int SleepTime = 100;
        private ServiceHost _serviceHost = null;
        private Thread _thread;
        private bool _isRunning;
        public ThreadServiceHost(Type serviceType)
        {
            _serviceHost = new ServiceHost(serviceType);
            _thread = new Thread(RunService);
            _thread.Start();
        }
        void RunService()
        {
            try
            {
                _isRunning = true;
                _serviceHost.Open();
                while (_isRunning)
                {
                    Thread.Sleep(SleepTime);
                }
                _serviceHost.Close();
                ((IDisposable)_serviceHost).Dispose();
            }
            catch (Exception)
            {
                if (_serviceHost != null)
                    _serviceHost.Close();
            }
        }
        public void Stop()
        {
            lock (this)
            {
                _isRunning = false;
            }
        }
    }
}
