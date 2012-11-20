using System.Web;
using DotNet.Common.Configuration.XmlConfig;
using DotNet.Common.Resources;
using DotNet.Common.Utility;

namespace DotNet.Common.Configuration
{
	public static class ConfigManager
	{
		/// <summary>
		/// 获取默认配置数据源。
		/// </summary>
		/// <returns>配置源。</returns>
		public static IConfigSource GetConfigSource()
		{
			return ConfigSource.Create(ConfigSourceType.XmlConfig);
		}

		/// <summary>
		/// 获取默认配置数据源。
		/// </summary>
		/// <param name="configSourceType"></param>
		/// <returns></returns>
		public static IConfigSource GetConfigSource(ConfigSourceType configSourceType)
		{
			return ConfigSource.Create(configSourceType);
		}

		/// <summary>
		/// 获取配置。
		/// </summary>
		/// <typeparam name="T">配置对象类型。</typeparam>
		/// <returns>配置。</returns>
		public static T GetConfig<T>(IConfigParameter para) where T : class, new()
		{
			return GetConfigSource().GetConfig<T>(para, false);
		}

		/// <summary>
		/// 获取配置。
		/// </summary>
		/// <typeparam name="T">配置对象类型。</typeparam>
		/// <returns>配置。</returns>
		public static T GetConfig<T>() where T : class, new()
		{
			return GetConfigSource().GetConfig<T>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetMergedConfig<T>() where T : class, IMergableConfig, new()
		{
			return GetConfigSource().GetMergedConfig<T>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileNameWithExtension"></param>
		/// <returns></returns>
		public static string GetConfigFilePath(string fileNameWithExtension)
		{
			return GetConfigFilePath(false, fileNameWithExtension);
		}

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="supportMultiLang"> </param>
	    /// <param name="fileNameWithExtension"></param>
	    /// <returns></returns>
	    public static string GetConfigFilePath(bool supportMultiLang, string fileNameWithExtension)
		{
			return ConfigHelper.ConfigSettings.GetConfigPath(false, fileNameWithExtension);
		}

		public static bool SaveConfig<T>(T t) where T : class, new()
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
					configFilePath = attribute.FileName;
					break;
				case ConfigPathType.ServerPath:
					configFilePath = HttpContext.Current.Server.MapPath(attribute.FileName);
					break;
				default:
					configFilePath = configSettings.GetConfigFilePath<T>();
					break;
			}

			XmlConfigGetParameter getParameter = new XmlConfigGetParameter(configFilePath, attribute.IncludeSubdirectories);


			return ObjectXmlSerializer.SaveXmlToFlie<T>(getParameter.FilePaths, t);
		}
	}
}
