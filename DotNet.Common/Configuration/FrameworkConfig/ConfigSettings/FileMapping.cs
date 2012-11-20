using System.Configuration;

namespace DotNet.Common.Configuration
{

    /// <summary>
    /// Represents a <see cref="FileMapping"/> for configuration provider.
    /// </summary>
    public class FileMapping : NamedConfigurationElement
    {
        private FileGroupMapping fileGroupMapping = null;
        private const string pathProperty = "path";

        /// <summary>
        /// Intialzie an instance of the <see cref="ConfigProviderMapping"/> class.
        /// </summary>
        public FileMapping()
        {
        }

        /// <summary>
        /// Gets or sets the path of the element.
        /// </summary>
        /// <value>
        /// The path of the element.
        /// </value>
        [ConfigurationProperty(pathProperty, DefaultValue = "", IsRequired = false)]
        public string Path
        {
            get { return (string)this[pathProperty]; }
            set { this[pathProperty] = value; }
        }

        /// <summary>
        /// 文件
        /// </summary>
        public FileGroupMapping FileGroupMapping
        {
            get { return fileGroupMapping; }
            set { fileGroupMapping = value; }
        }
    }
}