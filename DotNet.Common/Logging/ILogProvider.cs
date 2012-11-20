using System;

namespace DotNet.Common.Logging
{
    public interface ILogProvider
    {
        string LoggerName { get; }

        void Log(string logCategory, LogLevel level, string message);

        void Log(string logCategory, LogLevel level, string message,Exception ex);
    }
}
