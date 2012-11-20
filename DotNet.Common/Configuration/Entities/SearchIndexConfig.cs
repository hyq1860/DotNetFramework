using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigFile("SearchIndexConfig.config")]
    [XmlRoot("indexConfiguration")]
    public class SearchIndexConfig
    {
        [XmlElement("engine")]
        public SearchEngineCollection EngineList { get; set; }
    }

    public class SearchEngineCollection : KeyedCollection<string, SearchEngine>
    {
        protected override string GetKeyForItem(SearchEngine item)
        {
            return item.IndexPath;
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("engine")]
    public class SearchEngine
    {
        [XmlAttribute("indexPath")]
        public string IndexPath { get; set; }
    }
}
