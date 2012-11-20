using System;
using System.Collections.Generic;
using System.Text;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace DotNet.Common.Logging
{
    public class Log4NetLogProvider : ILogProvider
    {
        private static object _syncObject = new object();
        private ILog curLog;
        private string m_LoggerName = "";

        public Log4NetLogProvider()
        {
            curLog = null;
        }

        public Log4NetLogProvider(string loggerName)
        {
            curLog = null;
            this.m_LoggerName = loggerName;
        }

        public string LoggerName
        {
            get { return m_LoggerName; }
        }

        private ILog GetLog(string logCategory)
        {
            ILog resultLog = null;
            if (!string.IsNullOrEmpty(logCategory))
            {
                resultLog = LogManager.GetLogger(logCategory);
            }

            if (resultLog == null)
            {
                if (curLog == null)
                {
                    lock (_syncObject)
                    {
                        if (curLog == null &&　!string.IsNullOrEmpty(m_LoggerName))
                        {
                            curLog = LogManager.GetLogger(m_LoggerName);
                        }
                    }
                }

                return curLog;
            }
            else
            {
                return resultLog;
            }
        }

        public void Log(string logCategory, LogLevel level, string message)
        {
            ILog localLog = GetLog(logCategory);
            if (localLog == null || string.IsNullOrEmpty(message)) return;
            switch (level)
            {
                case LogLevel.Info:
                    {
                        if (localLog.IsInfoEnabled)
                        {
                            localLog.Info(message);
                        }

                        break;
                    }

                case LogLevel.Warn:
                    {
                        if (localLog.IsWarnEnabled)
                        {
                            localLog.Warn(message);
                        }

                        break;
                    }

                case LogLevel.Debug:
                    {
                        if (localLog.IsDebugEnabled)
                        {
                            localLog.Debug(message);
                        }

                        break;
                    }

                case LogLevel.Error:
                    {
                        if (localLog.IsErrorEnabled)
                        {
                            localLog.Error(message);
                        }

                        break;
                    }

                case LogLevel.Fatal:
                    {
                        if (localLog.IsFatalEnabled)
                        {
                            localLog.Fatal(message);
                        }

                        break;
                    }
            }

            if (localLog != curLog)
            {
                localLog = null;
            }
        }

        public void Log(string logCategory, LogLevel level, string message, Exception ex)
        {
            ILog localLog = GetLog(logCategory);
            if (localLog == null || string.IsNullOrEmpty(message)) return;
            switch (level)
            {
                case LogLevel.Info:
                    {
                        if (localLog.IsInfoEnabled)
                        {
                            localLog.Info(message,ex);
                        }

                        break;
                    }

                case LogLevel.Warn:
                    {
                        if (localLog.IsWarnEnabled)
                        {
                            localLog.Warn(message,ex);
                        }

                        break;
                    }

                case LogLevel.Debug:
                    {
                        if (localLog.IsDebugEnabled)
                        {
                            localLog.Debug(message,ex);
                        }

                        break;
                    }

                case LogLevel.Error:
                    {
                        if (localLog.IsErrorEnabled)
                        {
                            localLog.Error(message,ex);
                        }

                        break;
                    }

                case LogLevel.Fatal:
                    {
                        if (localLog.IsFatalEnabled)
                        {
                            localLog.Fatal(message,ex);
                        }

                        break;
                    }
            }

            if (localLog != curLog)
            {
                localLog = null;
            }
        }
    }
}
