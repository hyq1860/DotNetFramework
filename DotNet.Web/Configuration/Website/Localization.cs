using System.Xml.Serialization;

namespace DotNet.Web.Configuration.Website
{
    public class Localization
    {
        [XmlElement("companyCode")]
        public string CompanyCode 
        {
            get;
            set;
        }

        [XmlElement("storeCompanyCode")]
        public string StoreCompanyCode 
        {
            get;
            set;
        }

        [XmlElement("languageCode")]
        public string LanguageCode 
        {
            get;
            set;
        }
    }
}
