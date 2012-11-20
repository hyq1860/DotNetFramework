using DotNet.Common.Configuration.DotNetConfig;
using DotNet.Common.Configuration.XmlConfig;
using DotNet.Common.Diagnostics;
using DotNet.Common.Reflection;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigSource
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfigSource DataSetXml
        {
            get
            {
                return Create(ConfigSourceType.DataSetXmlConfig);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfigSource DotNet
        {
            get
            {
                return Create(ConfigSourceType.DotNetConfig);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfigSource Xml
        {
            get
            {
                return Create(ConfigSourceType.XmlConfig);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IConfigSource Database
        {
            get
            {
                return Create(ConfigSourceType.DatabaseConfig);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static IConfigSource Create(ConfigSourceType sourceType)
        {
            IConfigSource configSource = null;
            switch (sourceType)
            {
                //chenjunyu@yintai.com, disabled DataSetXmlConfig
                //case ConfigSourceType.DataSetXmlConfig:
                //    configSource = DataSetXmlConfigSource.Current;
                //    break;
                case ConfigSourceType.DotNetConfig:
                    configSource = DotNetConfigSource.Current;
                    break;
                case ConfigSourceType.XmlConfig:
                    configSource = XmlConfigSource.Current;
                    break;
                default:
                    configSource = Create(sourceType.ToString());
                    break;
            }

            return configSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IConfigSource Create(string name)
        {
            Check.Argument.IsNotNull("name", name);

            ConfigSettings configSettings = FrameworkConfig.GetConfig<ConfigSettings>();
            ConfigProviderMapping providerMapping = configSettings.GetProviderMapping(name);
            IConfigSource configSource = MethodCaller.CreateInstance(providerMapping.Type) as IConfigSource;
            if (configSource == null)
            {
                ConfigThrowHelper.ThrowConfigException(R.ConfigSourceNotFound, name);
            }

            return configSource;
        }
    }
}