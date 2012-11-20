using System.Xml.Serialization;

namespace DotNet.Web.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// A custome class for list item
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// Gets or sets the ListItem's text
        /// </summary>
        [XmlAttribute("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the ListItem's value
        /// </summary>
        [XmlAttribute( "value" )]
        public string Value { get; set; }

        [XmlAttribute( "isSelected" )]
        [JsonProperty(PropertyName = "selected")]
        public bool IsSelected { get; set; }
    }
}
