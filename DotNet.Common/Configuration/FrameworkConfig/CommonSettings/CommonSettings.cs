using System.Configuration;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 框架配置设置
    /// </summary>
	[DotNetConfigFile("commonSettings", RestartAppDomainOnChange=true)]
    public class CommonSettings : ConfigurationSection
    {
        private const string companyCodeProperty = "companyCode";
        private const string applicationNameProperty = "applicationName";
        private const string applicationInstanceNameProperty = "applicationInstanceName";
        private const string languageCodeProperty = "languageCode";

        /// <summary>
        /// 初始化 <see cref="CommonSettings"/> 类的新实例。 
        /// </summary>
        public CommonSettings()
            : base()
        {
        }

        /// <summary>
        /// 公司代码。
        /// </summary>
        [ConfigurationProperty(companyCodeProperty, IsRequired = false)]
        public string CompanyCode
        {
            get { return (string)this[companyCodeProperty]; }
            set { this[companyCodeProperty] = value; }
        }

        /// <summary>
        /// 应用程序名称。
        /// </summary>
        [ConfigurationProperty(applicationNameProperty, IsRequired = false)]
        public string ApplicationName
        {
            get { return (string)this[applicationNameProperty]; }
            set { this[applicationNameProperty] = value; }
        }

        /// <summary>
        /// 应用程序名称。
        /// </summary>
        [ConfigurationProperty(applicationInstanceNameProperty, IsRequired = false, DefaultValue="Default")]
        public string ApplicationInstanceName
        {
            get { return (string)this[applicationInstanceNameProperty]; }
            set { this[applicationInstanceNameProperty] = value; }
        }

        /// <summary>
        /// 应用程序名称。
        /// </summary>
        [ConfigurationProperty(languageCodeProperty, IsRequired = false, DefaultValue = "ZH-CN")]
        public string LanguageCode
        {
            get { return (string)this[languageCodeProperty]; }
            set { this[languageCodeProperty] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static CommonSettings Default
        {
            get
            {
                CommonSettings settings = new CommonSettings();
                settings.ApplicationName = "Default";
                settings.ApplicationInstanceName = "Default";
                settings.CompanyCode = "8601";
                settings.LanguageCode = "ZH-CN";
                return settings;
            }
        }
    }
}