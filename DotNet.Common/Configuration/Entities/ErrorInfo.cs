using System.Xml.Serialization;

namespace DotNet.Common.Configuration
{
    [XmlRoot("ErrorInfo")]
    public class ErrorInfo
    {
        [XmlAttribute("Code")]
        public string Code { get; set; }

        [XmlAttribute("String")]
        public string String { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }
    }
}
