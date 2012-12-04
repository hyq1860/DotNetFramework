using System;
using System.Reflection;
using System.Text;
using System.Web;
using DotNet.Common.Utility;
using System.Collections.Generic;

namespace DotNet.Common.Logging
{
    public static class Logger
    {
        private static object _syncObject = new object();

        private static ILogProvider LogProvider = null;

        private static void GetLogProvider()
        {
            if (LogProvider == null)
            {
                lock (_syncObject)
                {
                    if (LogProvider == null)
                    {
                        ProviderSetting currentProviderSetting = null;
                        ProviderConfigurationSection section = (ProviderConfigurationSection)System.Configuration.ConfigurationManager.GetSection("LoggingProvider");
                        ProviderCollection providers = section.Providers;

                        foreach (ProviderSetting ps in providers)
                        {
                            if (ps.Name == section.DefaultProvider)
                            {
                                currentProviderSetting = ps;
                                break;
                            }
                        }

                        if (currentProviderSetting != null)
                        {
                            Type t = Type.GetType(currentProviderSetting.Type);

                            if (t != null)
                            {
                                Type[] paramTypes = new Type[1]
                                { 
                                    typeof(string)
                                };
                                ConstructorInfo providerConstructorInfo = t.GetConstructor(paramTypes);

                                object[] paramValues = new object[1] 
                                { 
                                    currentProviderSetting .LoggerName
                                };

                                LogProvider = (ILogProvider)providerConstructorInfo.Invoke(paramValues);
                            }
                        }
                    }
                }
            }
        }

        private static string GetLogCategory<T>()
        {
            string logCategory = "";
            // get log category name from attribute
            object[] logCategoryAttributes = typeof(T).GetCustomAttributes(typeof(LogCategoryAttribute), true);
            if (logCategoryAttributes != null && logCategoryAttributes.Length > 0)
            {
                logCategory = ((LogCategoryAttribute)logCategoryAttributes[0]).Category;
            }
            else
            { // get log category name from fullname of type directly
                logCategory = typeof(T).FullName;
            }
            return logCategory;
        }

        public static string GetMessage<T>(string message)
        {
            return typeof(T).FullName + " " + message;
        }

        public static void Log(string message)
        {
            Log(string.Empty, LogLevel.Error, message);
        }

        public static void Log(LogLevel level, string message)
        {
            Log(string.Empty, level, message);
        }

        public static void Log(string logCategory, string message)
        {
            Log(logCategory, LogLevel.Error, message);
        }

        public static void Log<T>(string message)
        {
            Log<T>(LogLevel.Error, message);
        }

        public static void Log<T>(LogLevel level, string message)
        {
            Log(GetLogCategory<T>(), level, message);
        }

        public static void Log(string logCategory, LogLevel level, string message)
        {
            try
            {
                GetLogProvider();

                if (LogProvider == null || string.IsNullOrEmpty(message))
                {
                    return;
                }

                // add by Marble Wu, 2010,10,19
                // 增加记录服务器名
                StringBuilder serverInfo = new StringBuilder();
                serverInfo.AppendLine();
                serverInfo.AppendFormat("WHO: {0}", System.Net.Dns.GetHostName());
                serverInfo.AppendLine();

                // add by Marble Wu，2010-01-12
                // 如果是web请求，则记录当前RawUrl
                HttpContext ctx = HttpContext.Current;
                if (ctx != null
                    && ctx.Request != null
                    && !string.IsNullOrEmpty(ctx.Request.RawUrl))
                {

                    serverInfo.AppendLine("HTTP HEADER: ------------------------------- ");
                    serverInfo.AppendLine(string.Format("CLIENT IP: {0}", CommonUtily.GetAllIPAddress()));
                    foreach (string key in ctx.Request.Headers.Keys)
                    {
                        if (!_skipHttpHeaders.Contains(key))
                        {
                            serverInfo.AppendLine(string.Format("{0}: {1}", key, ctx.Request.Headers[key]));
                        }
                    }
                    serverInfo.AppendLine(string.Format("URL: {0}", ctx.Request.RawUrl));
                    serverInfo.AppendLine("HTTP HEADER: ------------------------------- ");
                    serverInfo.AppendLine(string.Empty);
                }

                message = string.Concat(serverInfo.ToString(), message);

                LogProvider.Log(logCategory, level, message);
            }
            catch 
            {

            }
        }

        public static void Log(string logCategory, LogLevel level, string message,Exception ex)
        {

            GetLogProvider();

            if (LogProvider == null || string.IsNullOrEmpty(message))
            {
                return;
            }

            // add by Marble Wu, 2010,10,19
            // 增加记录服务器名
            StringBuilder serverInfo = new StringBuilder();
            serverInfo.AppendLine();
            serverInfo.AppendFormat("WHO: {0}", System.Net.Dns.GetHostName());
            serverInfo.AppendLine();

            // add by Marble Wu，2010-01-12
            // 如果是web请求，则记录当前RawUrl
            HttpContext ctx = HttpContext.Current;
            if (ctx != null
                && ctx.Request != null
                && !string.IsNullOrEmpty(ctx.Request.RawUrl))
            {

                serverInfo.AppendLine("HTTP HEADER: ------------------------------- ");
                serverInfo.AppendLine(string.Format("CLIENT IP: {0}", CommonUtily.GetAllIPAddress()));
                foreach (string key in ctx.Request.Headers.Keys)
                {
                    if (!_skipHttpHeaders.Contains(key))
                    {
                        serverInfo.AppendLine(string.Format("{0}: {1}", key, ctx.Request.Headers[key]));
                    }
                }
                serverInfo.AppendLine(string.Format("URL: {0}", ctx.Request.RawUrl));
                serverInfo.AppendLine("HTTP HEADER: ------------------------------- ");
                serverInfo.AppendLine(string.Empty);
            }

            message = string.Concat(serverInfo.ToString(), message);

            LogProvider.Log(logCategory, level, message, ex);
        }

        private static List<string> _skipHttpHeaders = new List<string>() 
        {
            "Cache-Control",
            "Connection",
            "Accept",
            "Accept-Encoding",
            "Accept-Language",
            "Accept-Charset",
            "ASP.NET_SessionId",
        };
    }
}