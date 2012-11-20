using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNet.Web.Configuration
{
    /// <summary>
    /// A custome class for list group 
    /// </summary>
    public class ListGroup
    {
        /// <summary>
        /// Gets or sets the ListGroup's name
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ListGroup's Items
        /// </summary>
        [XmlElement( "listItem" )]
        public List<ListItem> Items
        {
            get;
            set;
        }
    }
}
