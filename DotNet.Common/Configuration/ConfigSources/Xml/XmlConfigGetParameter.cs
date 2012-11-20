namespace DotNet.Common.Configuration.XmlConfig
{
    /// <summary>
    /// 表示为获取相同Schema的Xml配置所需的入口参数。
    /// </summary>
    public class XmlConfigGetParameter : FileConfigGetParameter, IConfigParameter
    {
        /// <summary>
        /// 初始化 <see cref="XmlConfigGetParameter"/> 类的新实例。
        /// </summary>
        public XmlConfigGetParameter()
            : base()
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录，初始化 <see cref="XmlConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件的路径。</param>
        public XmlConfigGetParameter(string path)
            : base(path)
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录，初始化 <see cref="XmlConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件所在的目录。</param>
        /// <param name="filter">配置文件的类型。例如，“*.config”所有以.config扩展名的配置文件。</param>
        public XmlConfigGetParameter(string path, string filter)
            : base(path, filter)
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录和文件类型，初始化 <see cref="XmlConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件路径。</param>
        /// <param name="includeSubdirectories">指示在所有子目录中搜索配置文件的标识。</param>
        public XmlConfigGetParameter(string path, bool includeSubdirectories)
            : base(path, includeSubdirectories)
        {
        }

        /// <summary>
        /// 给定配置文件所在的目录和文件类型，初始化 <see cref="XmlConfigGetParameter"/> 类的新实例。
        /// </summary>
        /// <param name="path">配置文件所在的目录。</param>
        /// <param name="filter">配置文件的类型。例如，“*.config”所有以.config扩展名的配置文件。</param>
        /// <param name="includeSubdirectories">指示在所有子目录中搜索配置文件的标识。</param>
        public XmlConfigGetParameter(string path, string filter, bool includeSubdirectories)
            : base(path, filter, includeSubdirectories)
        {
        }
    }
}