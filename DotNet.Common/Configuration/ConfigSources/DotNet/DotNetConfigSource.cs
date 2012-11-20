using System.Configuration;
using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration.DotNetConfig
{

    /// <summary>
    /// 
    /// </summary>
    public class DotNetConfigSource : AbstractConfigSource
    {
        private static DotNetConfigSource configSource = new DotNetConfigSource();

        /// <summary>
        /// 
        /// </summary>
        public static DotNetConfigSource Current
        {
            get { return configSource; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T GetConfig<T>()
        {
            DotNetConfigFileAttribute attribute = AttributeHelper.GetConfigAttribute<DotNetConfigFileAttribute>(typeof(T));
            if (attribute == null)
            {
                ConfigThrowHelper.ThrowConfigException(
                    R.ConfigError_NoConfigAttribute, typeof(T).FullName, typeof(DotNetConfigFileAttribute).FullName);
            }

            ConfigSettings configSettings = FrameworkConfig.GetConfig<ConfigSettings>();
            string configFilePath = configSettings.GetConfigFilePath<T>();
            DotNetConfigGetParameter getParameter = new DotNetConfigGetParameter(configFilePath, attribute.SectionName);
            
            return GetConfig<T>(getParameter, attribute.RestartAppDomainOnChange);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getParameter"></param>
        /// <param name="restartAppDomainOnChange"></param>
        /// <returns></returns>
        public override T GetConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange)
        {
            Check.Argument.IsNotNull("getParameter", getParameter);
            Check.Argument.IsAssignableFrom("getParameter", getParameter, typeof(DotNetConfigGetParameter));

            T config = GetConfigFromCache<T>(getParameter);
            if (config == null)
            {
                lock (this.SyncLock)
                {
                    config = GetConfigFromCache<T>(getParameter);
                    if (config == null)
                    {
                        DotNetConfigGetParameter parameter = getParameter as DotNetConfigGetParameter;
                        Check.Argument.IsTrue(parameter.HasFiles, "getParameter", R.ConfigFileNotSpecified);

                        System.Configuration.Configuration configuration = GetConfiguration(parameter.Files[0]);
                        config = configuration.GetSection(parameter.SectionName) as T;
                        Check.Argument.IsNotNull("getParameter", config, R.DotNetConfigCannotBeResolved, parameter.Files[0], parameter.SectionName);

                        AddConfigToCache(getParameter as IConfigParameter, config);
                        SetupWacher(new DotNetConfigChangeWatcher(parameter, restartAppDomainOnChange));
                    }
                }
            }

            return config;
        }

        /// <summary>
        /// 获取DotNet配置
        /// </summary>
        /// <param name="fileName">配置文件路径。</param>
        /// <returns>DotNet配置</returns>
        private static System.Configuration.Configuration GetConfiguration(string fileName)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = fileName;
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}