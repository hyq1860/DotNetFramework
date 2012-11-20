using System.Xml;
using System.Xml.Serialization;

namespace DotNet.Web.Configuration
{
    /// <summary>
    /// A custom class for html block string setting
    /// </summary>
    public class HtmlBlockItem
    {
        /// <summary>
        /// Gets or sets the htmlblock's alias
        /// </summary>
        [XmlAttribute( "alias" )]
        public string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the htmlblock's content
        /// </summary>
        [XmlText]
        public string Content
        {
            get;
            set;
        }
    }
}
