using System;
using System.IO;


namespace DotNet.Common.Configuration.DotNetConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class DotNetConfigChangeWatcher : ConfigChangeWatcher, IConfigChangeWatcher
    {
        private const string eventSourceName = "APFramework Configuration.DotNetConfig";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getParameter"></param>
        public DotNetConfigChangeWatcher(DotNetConfigGetParameter getParameter)
            : base(getParameter)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getParameter"></param>
        /// <param name="restartAppDomainOnChange"></param>
        public DotNetConfigChangeWatcher(DotNetConfigGetParameter getParameter, bool restartAppDomainOnChange)
            : base(getParameter, restartAppDomainOnChange)
        {
        }

        /// <summary>
        /// <para>Allows an <see cref="DotNetConfigChangeWatcher"/> to attempt to free resources and perform other cleanup operations before the <see cref="ConfigurationChangeFileWatcher"/> is reclaimed by garbage collection.</para>
		/// </summary>
        ~DotNetConfigChangeWatcher()
		{
			Disposing(true);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigChangedEventArgs BuildEventData()
        {
            return new DotNetConfigChangedEventArgs(this.GetParameter as DotNetConfigGetParameter, this.RestartAppDomainOnChange);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DateTime GetCurrentLastWriteTime()
        {
            DateTime lastWriteTime = DateTime.MinValue;
            DateTime lastWriteTimeMax = DateTime.MinValue;

            DotNetConfigGetParameter getParameter = this.GetParameter as DotNetConfigGetParameter;

            string lastFilePaths = getParameter.FilePaths;
            getParameter.RefreshFiles();
            string currentFilePaths = getParameter.FilePaths;

            if (lastFilePaths == currentFilePaths)
            {
                foreach (string file in getParameter.Files)
                {
                    if (File.Exists(file))
                    {
                        lastWriteTime = File.GetLastWriteTime(file);
                        if (lastWriteTimeMax.CompareTo(lastWriteTime) == -1)
                        {
                            lastWriteTimeMax = lastWriteTime;
                        }
                    }
                    else
                    {
                        lastWriteTimeMax = DateTime.MaxValue;
                        break;
                    }
                }
            }
            else
            {
                lastWriteTimeMax = DateTime.MaxValue;
            }

            return lastWriteTimeMax;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetEventSourceName()
        {
            return eventSourceName;
        }
    }
}