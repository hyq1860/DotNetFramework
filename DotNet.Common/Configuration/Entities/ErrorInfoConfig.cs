using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Common.Configuration
{
    [ConfigFile("~/Language/zh-cn/ErrorInfo.config", ConfigPathType.ServerPath)]
    [XmlRoot("root")]
    public class ErrorInfoConfig
    {
        [XmlElement("ErrorInfo")]
        public ErrorInfoCollection ErrorInfoList { get; set; }
    }

    public class ErrorInfoCollection : KeyedCollection<string, ErrorInfo>
    {
        protected override string GetKeyForItem(ErrorInfo item)
        {
            return item.Code;
        }
    }
}
