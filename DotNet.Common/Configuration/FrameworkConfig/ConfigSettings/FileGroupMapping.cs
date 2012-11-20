using System.Configuration;

namespace DotNet.Common.Configuration
{

    /// <summary>
    /// Represents a <see cref="ConfigProviderMapping"/> for configuration provider.
    /// </summary>
    public class FileGroupMapping : NamedConfigurationElement
    {

        private const string pathProperty = "path";
        private const string filesProperty = "files";

        /// <summary>
        /// Intialzie an instance of the <see cref="ConfigProviderMapping"/> class.
        /// </summary>
        public FileGroupMapping()
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
        /// 
        /// </summary>
        [ConfigurationProperty(filesProperty, IsRequired = false)]
        public FileMappingCollection FileMappings
        {
            get
            {
                return (FileMappingCollection)base[filesProperty];
            }
        }
    }
}