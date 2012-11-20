using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigFile("CustomKeywordConfig.config")]
    [XmlRoot("keywordConfig")]
    public class CustomKeywordConfig
    {
        [XmlElement("keywordInfo")]
        public KeywordCollection keywordList { get; set; }
    }

    public class KeywordCollection : KeyedCollection<string, Keyword>
    {
        protected override string GetKeyForItem(Keyword item)
        {
            return item.URL;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("keywordInfo")]
    public class Keyword
    {
        [XmlAttribute("keywordDescription")]
        public string Name { get; set; }

        [XmlAttribute("keywordURL")]
        public string URL { get; set; }
    }

}
