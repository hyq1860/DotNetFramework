using System;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// Event handler called after a configuration has changed.
    /// </summary>
    /// <param name="sender">
    /// <para>The source of the event.</para>
    /// </param>
    /// <param name="e">
    /// <para>A <see cref="ConfigurationChangedEventArgs"/> that contains the event data.</para>
    /// </param>
    public delegate void ConfigChangedEventHandler(object sender, ConfigChangedEventArgs e);

    /// <summary>
    /// 配置改变事件参数
    /// </summary>
    public class ConfigChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 配置参数接口
        /// </summary>
        private IConfigParameter getParameter;

        /// <summary>
        /// 是否当配置改变时候重启AppDomain
        /// </summary>
        private bool restartAppDomainOnChange = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigChangedEventArgs"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="getParameter">
        /// </param>
        public ConfigChangedEventArgs(IConfigParameter getParameter)
            : this(getParameter, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigChangedEventArgs"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="getParameter">
        /// </param>
        /// <param name="restartAppDomainOnChange">
        /// </param>
        public ConfigChangedEventArgs(IConfigParameter getParameter, bool restartAppDomainOnChange)
        {
            this.getParameter = getParameter;
            this.restartAppDomainOnChange = restartAppDomainOnChange;
        }

        /// <summary>
        /// 配置参数
        /// </summary>
        public IConfigParameter GetParameter
        {
            get { return getParameter; }
        }

        /// <summary>
        /// 是否当配置改变时候重启AppDomain
        /// </summary>
        public bool RestartAppDomainOnChange
        {
            get { return this.restartAppDomainOnChange; }
        }
    }
}