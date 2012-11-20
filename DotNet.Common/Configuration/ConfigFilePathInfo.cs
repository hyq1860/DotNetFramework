namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 表示配置文件路径信息。
    /// </summary>
    public class ConfigFilePathInfo
    {
        #region [ Fields ]
        private string directory;
        private string fileName;
        #endregion

        #region [ Properties ]

        /// <summary>
        /// 获取或设置配置文件所在的目录路径。
        /// </summary>
        public string Directory
        {
            get { return this.directory; }
            set { this.directory = value; }
        }

        /// <summary>
        /// 获取或设置不包含路径配置文件名或文件名模式匹配字符串(如：*.config)。
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        #endregion
    }
}