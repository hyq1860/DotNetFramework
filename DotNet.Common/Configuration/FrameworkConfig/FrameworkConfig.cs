using System.Configuration;
using System.Web;

using DotNet.Common.Configuration.DotNetConfig;
using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration
{
	/// <summary>
	/// 表示 Framework 的配置。
	/// </summary>
	public class FrameworkConfig
	{
		public const string ConfigPath = "~/Framework.config";

		/// <summary>
		/// 
		/// </summary>
		public static CommonSettings CommonSettings
		{
			get
			{
				CommonSettings commonSettings = GetConfig<CommonSettings>();
				return commonSettings ?? CommonSettings.Default;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static ConfigSettings ConfigSettings
		{
			get
			{
				ConfigSettings commonSettings = GetConfig<ConfigSettings>();
				return commonSettings ?? ConfigSettings.Default;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetConfig<T>() where T : ConfigurationSection, new()
		{
			DotNetConfigFileAttribute attribute = AttributeHelper.GetConfigAttribute<T, DotNetConfigFileAttribute>();
			if (attribute == null)
			{
				ConfigThrowHelper.ThrowConfigException(
					R.ConfigError_NoConfigAttribute, typeof(T).FullName, typeof(DotNetConfigFileAttribute).FullName);
			}

			return GetConfig<T>(attribute.SectionName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sectionName"></param>
		/// <returns></returns>
		public static T GetConfig<T>(string sectionName) where T : ConfigurationSection, new()
		{
			Check.Argument.IsNotEmpty("sectionName", sectionName);

			DotNetConfigGetParameter getParameter = new DotNetConfigGetParameter(ConfigPath, sectionName);
			return ConfigSource.DotNet.GetConfig<T>(getParameter, true);
		}

		/// <summary>
		/// 
		/// </summary>
		public static bool RestartAppDomain()
		{
			bool restarted = false;
			try
			{
				HttpRuntime.UnloadAppDomain();
				restarted = true;
			}
			catch
			{
				restarted = false;
			}

			return restarted;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string GetFrameworkConfigPath(string fileName)
		{
			return ConfigSettings.GetConfigPath(false, string.Format("{0}/{1}", "Framework", fileName));
		}
	}
}