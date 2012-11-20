using System.Configuration;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 提供Provider配置模式的配置设置基类
    /// </summary>
    /// <typeparam name="T">Provider 类型。</typeparam>
    public abstract class ConfigurationSettings<T> : SerializableConfigurationSection
        where T : NamedConfigurationElement, new()
    {
        private const string defaultProviderProperty = "defaultProvider";
        private const string providerMappingProperty = "providers";

        /// <summary>
        /// 初始化 <see cref="ConfigurationSettings"/> 实例。
        /// </summary>
        public ConfigurationSettings()
            : base()
        {
        }

        /// <summary>
        /// 获取或设置默认的Provider。
        /// </summary>
        /// <value>默认的提供者。</value>
        [ConfigurationProperty(defaultProviderProperty, IsRequired = false)]
        public string DefaultProvider
        {
            get
            {
                return (string)this[defaultProviderProperty];
            }
            set
            {
                this[defaultProviderProperty] = value;
            }
        }

        /// <summary>
        /// 获取配置Provider集合。
        /// </summary>
        /// <value>配置Provider集合。</value>
        [ConfigurationProperty(providerMappingProperty, IsRequired = false)]
        public NamedConfigurationElementCollection<T> ProviderMappings
        {
            get
            {
                return (NamedConfigurationElementCollection<T>)base[providerMappingProperty];
            }
        }
    }
}
