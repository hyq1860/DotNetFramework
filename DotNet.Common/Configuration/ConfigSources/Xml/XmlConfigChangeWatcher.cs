using System;
using System.IO;


namespace DotNet.Common.Configuration.XmlConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlConfigChangeWatcher : ConfigChangeWatcher, IConfigChangeWatcher
    {
        private const string eventSourceName = "APFramework Configuration";

        public XmlConfigChangeWatcher(XmlConfigGetParameter getParameter)
            : base(getParameter)
        {
        }

        /// <summary>
        /// <para>Allows an <see cref="XmlConfigChangeWatcher"/> to attempt to free resources and perform other cleanup operations before the <see cref="ConfigurationChangeFileWatcher"/> is reclaimed by garbage collection.</para>
		/// </summary>
		~XmlConfigChangeWatcher()
		{
			Disposing(true);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigChangedEventArgs BuildEventData()
        {
            return new XmlConfigChangedEventArgs(this.GetParameter as XmlConfigGetParameter, this.RestartAppDomainOnChange);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DateTime GetCurrentLastWriteTime()
        {
            DateTime lastWriteTime = DateTime.MinValue;
            DateTime lastWriteTimeMax = DateTime.MinValue;

            XmlConfigGetParameter getParameter = this.GetParameter as XmlConfigGetParameter;

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