using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Web.Configuration
{
    [XmlRoot("listConfig" , Namespace="http://www.YinTai.com/Language")]
    [ConfigFile("~/Configs/List.config",ConfigPathType.ServerPath)]
    public class ListConfiguration
    {
        /// <summary>
        /// Get or Set the ListGroup collection
        /// </summary>
        [XmlElement("listGroup")]
        public ListGroupCollection Groups
        {
            get;
            set;
        }

        /// <summary>
        /// Get the listitems by group the name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<ListItem> GetListItems(string groupName)
        {
            List<ListItem> items = null;
            
            ListGroup listGroup = Groups.Where( group => group.Name.ToLower( ) == groupName.ToLower( ) )
                .SingleOrDefault<ListGroup>( );

            if ( listGroup != null )
            {
                items = listGroup.Items;
            }

            return items;
        }
    }
}
