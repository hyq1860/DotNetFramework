using System.Linq;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Web.Configuration
{
    [ConfigFile("~/Sources/HtmlBlockStringResource.config", ConfigPathType.ServerPath)]
    [XmlRoot( "htmlBlockConfig", Namespace = "http://www.YinTai.com/Language" )]
    public class HtmlBlockResourceConfiguration
    {
        /// <summary>
        /// Get or set the BlockCollection
        /// </summary>
        [XmlElement( "htmlBlock" )]
        public HtmlBlockCollection BlockCollection
        {
            get;
            set;
        }

        /// <summary>
        /// Get a htmlblock item by the alias
        /// </summary>
        /// <param name="alia">The htmlblock's alias</param>
        /// <returns><see cref="DotNet.Web.Configuration.HtmlBlockItem"/></returns>
        public HtmlBlockItem GetHtmlBlock(string alias)
        {
            return BlockCollection.Where( html => html.Alias.ToLower( ) == alias.ToLower( ) )
                .SingleOrDefault<HtmlBlockItem>( );
        }
    }
}
