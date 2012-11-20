using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace DotNet.Common.Configuration
{
    [ConfigFile("CommentPoint.config")]
    [XmlRoot("CommentPoints")]
    public class CommentPointConfig
    {
        [XmlElement("CommentPoint")]
        public CommentPointCollection CommentPointList { get; set; }
    }

    public class CommentPointCollection : KeyedCollection<string, CommentPoint>
    {
        protected override string GetKeyForItem(CommentPoint item)
        {
            return item.ID;
        }
    }

    [XmlRoot("CommentPoint")]
    public class CommentPoint
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("point")]
        public int Point { get; set; }
    }
}
