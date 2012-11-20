using System.Linq;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Web.Configuration.Cookie
{
    [ConfigFile("Cookie.config")]
    [XmlRoot("cookieConfig", Namespace="http://www.YinTai.com/cookie")]
    public class CookieConfiguration
    {
        [XmlElement("cookie")]
        public ConfigCookieEntryCollection ConfigCookieEntrys { get; set; }

        public ConfigCookieEntry GetConfigCookieEntry(string cookieName)
        {
            return ConfigCookieEntrys.Where(c => c.Name.ToUpper() == cookieName.ToUpper()).SingleOrDefault();
        }

        //public List<ConfigCookieEntry>  GetClientCookieEntrys()
        //{
        //    return ConfigCookieEntrys.Where( cookie => cookie.RenderInClient ).ToList( );
        //}
    }
}