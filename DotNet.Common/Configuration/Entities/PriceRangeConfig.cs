using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    [ConfigFile("PriceRange.config")]
    [XmlRoot("root")]
    public class PriceRangeConfig
    {
        [XmlElement("priceRange")]
        public PriceRangeCollection PriceRangeList { get; set; }
    }

    public class PriceRangeCollection : KeyedCollection<string, PriceRange>
    {
        protected override string GetKeyForItem(PriceRange item)
        {
            return item.Description;
        }
    }

    [XmlRoot("priceRange")]
    public class PriceRange
    {
        [XmlAttribute("lowerLimit")]
        public decimal LowerLimit { get; set; }

        [XmlAttribute("upperLimit")]
        public decimal UpperLimit { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
