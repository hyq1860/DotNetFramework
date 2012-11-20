namespace DotNet.Common.Configuration
{
    public class ConfigHelper
    {
        public static IndexConfiguration IndexConfig
        {
            get
            {
                return ConfigManager.GetConfig<IndexConfiguration>();
            }
        }

        public static SearchIndexConfig SearchIndexConfig
        {
            get
            {
                return ConfigManager.GetConfig<SearchIndexConfig>();
            }
        }

        public static PriceRangeConfig PriceRangeConfig
        {
            get { return ConfigManager.GetConfig<PriceRangeConfig>(); }
        }

        public static ExManagerConfig ExManagerConfig
        {
            get { return ConfigManager.GetConfig<ExManagerConfig>(); }
        }

        public static ErrorInfoConfig ErrorInfoConfig
        {
            get { return ConfigManager.GetConfig<ErrorInfoConfig>(); }
        }

        //public static ConnectionStringConfigs ConnectionStringConfigs
        //{
        //    get { return ConfigManager.GetConfig<ConnectionStringConfigs>(); }
        //}

        public static MessageResourceConfig MessageConfig
        {
            get { return ConfigManager.GetConfig<MessageResourceConfig>(); }
        }

        public static CommentPointConfig CommentPointConfig
        {
            get { return ConfigManager.GetConfig<CommentPointConfig>(); }
        }

        //public static FixSqlConfig FixSqlConfig
        //{
        //    get { return ConfigManager.GetConfig<FixSqlConfig>(); }
        //}

        public static CustomKeywordConfig customKeywordConfig
        {
            get { return ConfigManager.GetConfig<CustomKeywordConfig>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static CommonSettings CommonSettings
        {
            get { return FrameworkConfig.GetConfig<CommonSettings>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static ConfigSettings ConfigSettings
        {
            get { return FrameworkConfig.GetConfig<ConfigSettings>(); }
        }
    }
}
