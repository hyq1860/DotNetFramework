using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigFile("IndexConfig.config")]
    [XmlRoot("indexConfiguration")]
    public class IndexConfiguration
    {
        [XmlAttribute("tempPath")]
        public string TempPath { get; set; }

        [XmlAttribute("backUpPath")]
        public string BackUpPath { get; set; }

        [XmlAttribute("backUpCount")]
        public int BackUpCount { get; set; }

        [XmlElement("webSite")]
        public WebSiteCollection WebSiteList { get; set; }
    }

    public class EngineCollection : KeyedCollection<string, Engine>
    {
        protected override string GetKeyForItem(Engine item)
        {
            return item.Name;
        }
    }

    public class WebSiteCollection : KeyedCollection<string, WebSite>
    {
        protected override string GetKeyForItem(WebSite item)
        {
            return item.Name;
        }
    }

    [XmlRoot("webSite")]
    public class WebSite
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("configFullPath")]
        public string ConfigFullPath { get; set; }

        [XmlAttribute("nextIndexEngine")]
        public string NextIndexEngine { get; set; }

        [XmlElement("engine")]
        public EngineCollection EngineList { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("engine")]
    public class Engine
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("ip")]
        public string IP { get; set; }

        [XmlAttribute("indexPath")]
        public string IndexPath { get; set; }
    }
}
