using System;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 指定配置文件的属性信息。
    /// </summary>
    /// <remarks>此属性只允许配置在类型非 <b>ConfigurationSection</b> 和 <b>ConfigurationSectionGroup</b> 的Class上。</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigFileAttribute : Attribute
    {
        private bool supportMultiLanguages = false;

        private string fileName = string.Empty;

        private ConfigPathType configPathType = ConfigPathType.Default;

        private bool includeSubdirectories = false;

        private bool restartAppDomainOnChange = false;

        /// <summary>
        /// 用正在属性化的配置文件位置特性初始化 <see cref="ConfigFileAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="fileName"></param>
        public ConfigFileAttribute(string fileName)
        {
            this.fileName = fileName;
        }

		/// <summary>
		/// 用正在属性化的配置文件位置特性初始化 <see cref="ConfigFileAttribute"/> 类的新实例。
		/// </summary>
		/// <param name="supportMultiLanguages">是否支持多语言标志。</param>
		/// <param name="fileName">相对路径文件名。</param>
		/// <param name="group">配置文件所属配置组。</param>
		public ConfigFileAttribute(bool supportMultiLanguages, string fileName)
		{
			this.supportMultiLanguages = supportMultiLanguages;
			this.fileName = fileName;
		}

        /// <summary>
        /// 用正在属性化的配置文件位置特性初始化 <see cref="ConfigFileAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="fileName">相对路径文件名。</param>
        /// <param name="configPathType">文件路径类型。</param>
        public ConfigFileAttribute(string fileName, ConfigPathType configPathType)
        {
            this.configPathType = configPathType;
            this.fileName = fileName;
        }

        /// <summary>
        /// 用正在属性化的配置文件位置特性初始化 <see cref="ConfigFileAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="supportMultiLanguages">是否支持多语言标志。</param>
        /// <param name="fileName">相对路径文件名。</param>
        /// <param name="configPathType">文件路径类型。</param>
        public ConfigFileAttribute(bool supportMultiLanguages, string fileName, ConfigPathType configPathType)
        {
            this.configPathType = configPathType;
            this.supportMultiLanguages = supportMultiLanguages;
            this.fileName = fileName;
        }

        /// <summary>
        /// 获取相对路径文件名。
        /// </summary>
        /// <value>相对路径文件名。</value>
        public virtual string FileName
        {
            get { return this.fileName; }
            protected set { this.fileName = value; }
        }

        /// <summary>
        /// 配置文件路径类型。
        /// </summary>
        public ConfigPathType ConfigPathType
        {
            get { return this.configPathType; }
            protected set { this.configPathType = value; }
        }

        /// <summary>
        /// 获取是否支持多语言标志。
        /// </summary>
        /// <value>是否支持多语言标志。</value>
        public bool SupportMultiLanguages
        {
            get { return this.supportMultiLanguages; }
            set { this.supportMultiLanguages = value; }
        }        

        /// <summary>
        /// 是否包含子目录
        /// </summary>
        public bool IncludeSubdirectories
        {
            get { return this.includeSubdirectories; }
            set { this.includeSubdirectories = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RestartAppDomainOnChange
        {
            get { return this.restartAppDomainOnChange; }
            set { this.restartAppDomainOnChange = true; }
        }
    }
}