using DotNet.Common.Configuration;
using DotNet.Data.Configuration;
using DotNet.Web.Configuration.Cookie;


namespace DotNet.Web.Configuration
{
    public class ConfigHelper
    {
        public static ConnectionStringConfigs ConnectionStringConfigs
        {
            get { return ConfigManager.GetConfig<ConnectionStringConfigs>(); }
        }

        public static CookieConfiguration CookieConfig
        {
            get { return ConfigManager.GetConfig<CookieConfiguration>(); }
        }


        public static StringResourceConfiguration StringResourceConfig
        {
            get { return ConfigManager.GetConfig<StringResourceConfiguration>(); }
        }

        /// <summary>
        /// Gets the current <see cref="DotNet.Web.Configuration.HtmlBlockResourceConfiguration"/>
        /// </summary>
        public static HtmlBlockResourceConfiguration HtmlBlockConfig
        {
            get { return ConfigManager.GetConfig<HtmlBlockResourceConfiguration>(); }
        }

        /// <summary>
        /// Gets the current <see cref="DotNet.Web.Configuration.ListConfiguration"/>
        /// </summary>
        public static ListConfiguration ListConfig
        {
            get { return ConfigManager.GetConfig<ListConfiguration>(); }
        }

        /// <summary>
        /// Gets the params config.
        /// </summary>
        public static ParamsConfiguration ParamsConfig
        {
            get { return ConfigManager.GetConfig<ParamsConfiguration>(); }
        }
    }
}