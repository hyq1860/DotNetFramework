using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;
using DotNet.Common.Serialization;
using DotNet.Common.Utility;

namespace DotNet.Common.Configuration.XmlConfig
{
    /// <summary>
    /// 表示以xml作为配置文件。
    /// </summary>
    public class XmlConfigSource : AbstractConfigSource, IConfigSource
    {
        private static XmlConfigSource configSource = new XmlConfigSource();

        /// <summary>
        /// 初始化 <see cref="XmlConfigSource"/> 类的新实例。
        /// </summary>
        public XmlConfigSource()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        ~XmlConfigSource()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static XmlConfigSource Current
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
            ConfigFileAttribute attribute = AttributeHelper.GetConfigAttribute<ConfigFileAttribute>(typeof(T));
            if (attribute == null)
            {
                ConfigThrowHelper.ThrowConfigException(
                    R.ConfigError_NoConfigAttribute, typeof(T).FullName, typeof(ConfigFileAttribute).FullName);
            }

            ConfigSettings configSettings = FrameworkConfig.GetConfig<ConfigSettings>();
            string configFilePath = string.Empty;

            switch (attribute.ConfigPathType)
            {
                case ConfigPathType.FullPhysicalPath:
                    {
                       configFilePath = attribute.FileName; 
                    }
                    break;
                case ConfigPathType.ServerPath:
                    if (HttpContext.Current == null)
                    {
                        configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, attribute.FileName.Replace("~/", ""));
                        break;
                    }
                    configFilePath = HttpContext.Current.Server.MapPath(attribute.FileName);
                    break;
                default:
                    {
                       configFilePath = configSettings.GetConfigFilePath<T>(); 
                    }
                    break;
            }
            XmlConfigGetParameter getParameter = new XmlConfigGetParameter(configFilePath, attribute.IncludeSubdirectories);

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
            Check.Argument.IsAssignableFrom("getParameter", getParameter, typeof(XmlConfigGetParameter));

            T config = GetConfigFromCache<T>(getParameter);
            if (config == null)
            {
                lock (this.SyncLock)
                {
                    config = GetConfigFromCache<T>(getParameter);
                    if (config == null)
                    {
                        XmlConfigGetParameter cp = getParameter as XmlConfigGetParameter;
                        if (cp.Files.Length > 0)
                        {
                            if (cp.Files.Length == 1)
                            {
                                try
                                {
                                    config = Serializer.XmlSerializer.FromFile<T>(cp.Files[0]);
                                }
                                catch (Exception ex)
                                {
                                    ConfigThrowHelper.ThrowConfigException(ex, R.ConfigFileNotResolved, cp.Files[0], typeof(T).FullName);
                                }
                            }
                            else
                            {
                                try
                                {
                                    FileMergeResult fileMergeResult = XmlUtils.MergeFiles(new List<string>(cp.Files));
                                    if (!fileMergeResult.AllFilesMerged)
                                    {
                                        //Logger.Warning("Framework.Configuration", "XMLConfigProvider", fileMergeResult.ToString());
                                    }

                                    if (fileMergeResult.HasFileMerged)
                                    {
                                        config = Serializer.XmlSerializer.FromSerializedString<T>(fileMergeResult.FileContentMerged);
                                    }
                                    else
                                    {
                                        //Logger.Error("Framework.Configuration", "XMLConfigProvider", fileMergeResult.ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ConfigThrowHelper.ThrowConfigException(ex, R.ConfigFileNotResolved, cp.FilePaths, typeof(T).FullName);
                                }
                            }

                            AddConfigToCache(getParameter, config);
                            SetupWacher(new XmlConfigChangeWatcher(cp));
                        }
                    }
                }
            }

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T GetMergedConfig<T>()
        {
            ConfigFileAttribute attribute = AttributeHelper.GetConfigAttribute<ConfigFileAttribute>(typeof(T));
            if (attribute == null)
            {
                ConfigThrowHelper.ThrowConfigException(
                    R.ConfigError_NoConfigAttribute, typeof(T).FullName, typeof(ConfigFileAttribute).FullName);
            }

            ConfigSettings configSettings = FrameworkConfig.GetConfig<ConfigSettings>();
            string configFilePath = configSettings.GetConfigFilePath<T>();
            XmlConfigGetParameter getParameter = new XmlConfigGetParameter(configFilePath, attribute.IncludeSubdirectories);

            return GetMergedConfig<T>(getParameter, attribute.RestartAppDomainOnChange);
        }

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        public override T GetMergedConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange)
        {
            Check.Argument.IsNotNull("getParameter", getParameter);
            Check.Argument.IsAssignableFrom("getParameter", getParameter, typeof(XmlConfigGetParameter));

            T config = GetConfigFromCache<T>(getParameter);
            if (config == null)
            {
                lock (this.SyncLock)
                {
                    config = GetConfigFromCache<T>(getParameter);
                    if (config == null)
                    {
                        config = new T();

                        XmlConfigGetParameter cp = getParameter as XmlConfigGetParameter;
                        if (cp.HasFiles)
                        {
                            FileMergeResult mergeResult = new FileMergeResult(new List<string>(cp.Files));
                            foreach (string file in cp.Files)
                            {
                                try
                                {
                                    T splitConfig = Serializer.XmlSerializer.FromFile<T>(file);
                                    config.Merge(splitConfig);

                                    mergeResult.FilesMerged.Add(file);
                                }
                                catch (Exception ex)
                                {
                                    FileMergeFailReason mergeFailReasion = new FileMergeFailReason();
                                    mergeFailReasion.FileName = file;
                                    mergeFailReasion.MergeFailReason = ExceptionHelper.GetMessage(ex);
                                    mergeResult.FileMergeFailReasons.Add(mergeFailReasion);
                                }
                            }

                            if (!mergeResult.HasFileMerged)
                            {
                                // Logger.Error
                            }
                            else if (!mergeResult.AllFilesMerged)
                            {
                                //Logger.Warning("Framework.Configuration", "XMLConfigProvider", fileMergeResult.ToString());
                            }

                            AddConfigToCache(getParameter, config);
                            SetupWacher(new XmlConfigChangeWatcher(cp));
                        }
                    }
                }
            }

            return config;
        }
    }
}