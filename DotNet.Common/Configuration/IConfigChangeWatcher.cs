using System;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigChangeWatcher : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IConfigParameter GetParameter { get; }

        /// <summary>
        /// 
        /// </summary>
        event ConfigChangedEventHandler Changed;

        /// <summary>
        /// starts the object watching for configuration changes
        /// </summary>
        void StartWatching();

        /// <summary>
        /// stops the object from watching for configuration changes
        /// </summary>
        void StopWatching();
    }
}