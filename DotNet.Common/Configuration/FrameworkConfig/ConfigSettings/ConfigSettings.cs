using System;
using System.Configuration;

using DotNet.Common.Utility;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 框架配置设置
    /// </summary>
    [DotNetConfigFile("ConfigSettings", RestartAppDomainOnChange = true)]
    public class ConfigSettings : ConfigurationSettings<ConfigProviderMapping>
    {
        private const string DefaultAppRootPath = "~/";
        private const string DefaultRelativePath = "Configs";
        private const string DefaultMultiLanguageRelativePath = "Configs/Languages";
        private const string DefaultExtensions = ".config";

        private const string AppRootPathProperty = "appRootPath";
        private const string RelativePathProperty = "relativePath";
        private const string MultiLangRelativePathProperty = "multiLangRelativePath";
        private const string ExtensionsProperty = "extensions";
        private const string FileGroupsProperty = "fileGroups";

        /// <summary>
        /// 初始化 <see cref="ConfigSettings"/> 类的新实例。 
        /// </summary>
        public ConfigSettings()
            : base()
        {
        }

        /// <summary>
        ///  获取或设置支持配置文件所在根路径。
        /// </summary>
        /// <value>即可是绝对路径（完全限定路径）也可是相对路径。</value>
        [ConfigurationProperty(AppRootPathProperty, IsRequired = false)]
        public string AppRootPath
        {
            get
            {
                string appPathRoot = (string)this[AppRootPathProperty];
                if (StringHelper.IsEmpty(appPathRoot))
                {
                    appPathRoot = DefaultAppRootPath;
                }

                return appPathRoot;
            }

            set { this[AppRootPathProperty] = value; }
        }

        /// <summary>
        /// 兼容上一个版本。
        /// </summary>
        public string AppPathRoot
        {
            get { return AppRootPath; }
        }

        /// <summary>
        ///  获取或设置支持配置文件所在根路径。
        /// </summary>
        /// <value>即可是绝对路径（完全限定路径）也可是相对路径。</value>
        [ConfigurationProperty(RelativePathProperty, IsRequired = false)]
        public string RelativePath
        {
            get
            {
                string relativePath = (string)this[RelativePathProperty];
                if (StringHelper.IsEmpty(relativePath))
                {
                    relativePath = DefaultRelativePath;
                }

                return relativePath;
            }

            set { this[RelativePathProperty] = value; }
        }

        /// <summary>
        /// 获取或设置支持多语言配置文件所在根路径。
        /// </summary>
        /// <value>即可是绝对路径（完全限定路径）也可是相对路径。</value>
        [ConfigurationProperty(MultiLangRelativePathProperty, IsRequired = false)]
        public string MultiLangRelativePath
        {
            get
            {
                string multiLanguagePath = (string)this[MultiLangRelativePathProperty];
                if (StringHelper.IsEmpty(multiLanguagePath))
                {
                    multiLanguagePath = DefaultMultiLanguageRelativePath;
                }

                return multiLanguagePath;
            }

            set { this[MultiLangRelativePathProperty] = value; }
        }

        /// <summary>
        ///  获取或设置支持配置文件所在根路径。
        /// </summary>
        /// <value>即可是绝对路径（完全限定路径）也可是相对路径。</value>
        public string RootPath
        {
            get
            {
                return string.Format("{0}/{1}",
                    AppRootPath.TrimEnd('/', '\\'),
                    RelativePath.TrimStart('/', '\\'));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MultiLanguageRootPath
        {
            get
            {
                bool isRoot = MultiLangRelativePath.StartsWith("~/");
                return isRoot ? MultiLangRelativePath :
                    string.Format("{0}/{1}",
                        AppRootPath.TrimEnd('/', '\\'),
                        MultiLangRelativePath.TrimStart('/', '\\').TrimEnd('/', '\\'));
            }
        }

        /// <summary>
        /// 获取或设置允许的配置文件扩展名列表。
        /// </summary>
        /// <remarks>
        /// 文件扩展名配置格式：.config|.xml，系统会先搜索扩展名为.config配置文件，然后再搜索扩展名为.xml配置文件<br />
        /// Website.config / Website.xml 认为是同一个文件，优先匹配Website.config
        /// </remarks>
        [ConfigurationProperty(ExtensionsProperty, IsRequired = false)]
        public string Extensions
        {
            get
            {
                string extensions = (string)this[ExtensionsProperty];
                if (StringHelper.IsEmpty(extensions))
                {
                    extensions = DefaultExtensions;
                }

                return extensions;
            }
            set { this[ExtensionsProperty] = value; }
        }

        /// <summary>
        /// 获取配置文件组映射。
        /// </summary>
        /// <value>配置文件组映射。</value>
        [ConfigurationProperty(FileGroupsProperty, IsRequired = false)]
        public FileGroupMappingCollection FileGroupMappings
        {
            get
            {
                return (FileGroupMappingCollection)base[FileGroupsProperty];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetConfigFilePath<T>()
            where T : class, new()
        {
            ConfigFileAttribute configFileAttribute = AttributeHelper.GetConfigAttribute<T, ConfigFileAttribute>();
            if (configFileAttribute == null)
            {
                ConfigThrowHelper.ThrowConfigException(R.ConfigError_NoConfigAttribute, typeof(T).FullName, typeof(ConfigFileAttribute).FullName);
            }

            string rootPath = GetConfigFileRootPath<T>();

            return string.Format("{0}/{1}", rootPath.TrimEnd('/', '\\'), configFileAttribute.FileName.TrimStart('/', '\\'));
        }

        /// <summary>
        /// 获取配置文件根路径。
        /// </summary>
        /// <returns>配置文件根路径。</returns>
        public string GetConfigFileRootPath<T>() where T : class
        {
            bool supportMultiLanguages = false;
            Type configType = typeof(T);
            ConfigFileAttribute configFileAttribute = AttributeHelper.GetConfigAttribute<ConfigFileAttribute>(configType);
            if (configFileAttribute != null)
            {
                supportMultiLanguages = configFileAttribute.SupportMultiLanguages;
            }

            string path = "";
            string fileMappingPath = "";
            string groupMappingPath = "";

            // 获取所有文件映射
            FileMappingCollection fileMappings = GetFileMappings();
            FileMapping fileMapping = null;
            if (fileMappings.Contains(configType.Name))
            {
                fileMapping = fileMappings.Get(configType.Name);
            }
            else if (fileMappings.Contains(configType.FullName))
            {
                fileMapping = fileMappings.Get(configType.FullName);
            }

            if (fileMapping != null)
            {
                fileMappingPath = fileMapping.Path;
                groupMappingPath = fileMapping.FileGroupMapping.Path;
            }

            if (fileMappingPath != "")
            {
                path = GetConfigPath(fileMappingPath);
            }
            else if (groupMappingPath != "")
            {
                path = GetConfigPath(groupMappingPath);
            }
            else
            {
                path = supportMultiLanguages ? MultiLanguageRootPath : RootPath;
            }

            return supportMultiLanguages ?
                string.Format("{0}/{1}", path, ApplicationContext.LanguageCode) : path;
        }

        /// <summary>
        /// 获取所有配置文件映射。
        /// </summary>
        /// <value>所有配置文件映射。</value>
        /// <exception cref="ConfigurationErrorsException">配置文件映射关系名重复。</exception>
        /// <remarks>
        /// 一个配置文件只能从属于一个文件组。
        /// </remarks>
        public FileMappingCollection GetFileMappings()
        {
            FileMappingCollection fileMappings = new FileMappingCollection();
            if (FileGroupMappings != null)
            {
                foreach (FileGroupMapping group in FileGroupMappings)
                {
                    foreach (FileMapping file in group.FileMappings)
                    {
                        if (fileMappings.Contains(file.Name))
                        {
                            ConfigThrowHelper.ThrowConfigException(R.ConfigError_DuplicateFileMapping, file.Name);
                        }

                        file.FileGroupMapping = group;
                        fileMappings.Add(file);
                    }
                }
            }
            return fileMappings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ConfigProviderMapping GetProviderMapping(string name)
        {
            return this.ProviderMappings.Get(name);
        }

        /// <summary>
        /// 
        /// </summary>
        public static ConfigSettings Default
        {
            get
            {
                ConfigSettings settings = new ConfigSettings();
                return settings;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetConfigPath(string path)
        {
            string configPath = path;
            if (StringHelper.IsEmpty(configPath))
                configPath = string.Empty;

            if (!configPath.StartsWith("~/"))
            {
                configPath = string.Format("{0}/{1}", AppRootPath.TrimEnd('/', '\\'), path.TrimStart('/', '\\'));
            }

            return configPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supportMultiLang"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetConfigPath(bool supportMultiLang, string path)
        {
            return supportMultiLang ?
                string.Format("{0}/Languages/{1}/{2}", RootPath.TrimEnd('/', '\\'), ApplicationContext.LanguageCode, path.TrimStart('/', '\\')) :
                 string.Format("{0}/{1}", RootPath.TrimEnd('/', '\\'), path.TrimStart('/', '\\'));
        }
    }
}