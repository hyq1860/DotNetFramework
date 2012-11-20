using System;
using System.Collections.Generic;

using DotNet.Common.Diagnostics;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 配置属性辅助类。
    /// </summary>
    public class AttributeHelper
    {
        #region [ Fields ]

        /// <summary>
        /// </summary>
        private static Dictionary<Type, ConfigFileAttribute> typeConfigFileAttributes
            = new Dictionary<Type, ConfigFileAttribute>();

        /// <summary>
        /// lock object
        /// </summary>
        private static object syncObj = new object();

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 获取配置属性。
        /// </summary>
        /// <typeparam name="TConfig">表示配置类型。</typeparam>
        /// <typeparam name="TAttribute">表示配置属性类型。</typeparam>
        /// <returns>配置。</returns>
        public static TAttribute GetConfigAttribute<TConfig, TAttribute>()
            where TConfig : class, new()
            where TAttribute : ConfigFileAttribute
        {
            return GetConfigAttribute<TAttribute>(typeof(TConfig));
        }

        /// <summary>
        /// 获取配置属性。
        /// </summary>
        /// <typeparam name="TConfig">表示配置类型。</typeparam>
        /// <returns>配置属性。</returns>
        public static ConfigFileAttribute GetConfigAttribute<TConfig>() where TConfig : class, new()
        {
            return GetConfigAttribute<ConfigFileAttribute>(typeof(TConfig));
        }

        /// <summary>
        /// 获取配置属性。
        /// </summary>
        /// <typeparam name="TAttribute">表示配置属性类型。</typeparam>
        /// <param name="configType">配置类型。</param>
        /// <returns>配置属性。</returns>
        public static TAttribute GetConfigAttribute<TAttribute>(Type configType) where TAttribute : ConfigFileAttribute
        {
            Check.Argument.IsNotNull("configType", configType);

            ConfigFileAttribute configFileAttribute = null;
            if (!typeConfigFileAttributes.TryGetValue(configType, out configFileAttribute))
            {
                lock (syncObj)
                {
                    if (!typeConfigFileAttributes.TryGetValue(configType, out configFileAttribute))
                    {
                        configFileAttribute = 
                            Attribute.GetCustomAttribute(configType, typeof(ConfigFileAttribute)) as ConfigFileAttribute;
                        if (configFileAttribute != null)
                        {
                            typeConfigFileAttributes.Add(configType, configFileAttribute);
                        }
                    }
                }
            }

            return configFileAttribute as TAttribute;
        }

        #endregion
    }
}