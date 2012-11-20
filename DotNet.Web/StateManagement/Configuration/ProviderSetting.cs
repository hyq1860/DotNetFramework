using System.Configuration;

namespace DotNet.Web.StateManagement
{
    public class ProviderSetting : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }

            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return this["type"].ToString();
            }

            set
            {
                this["type"] = value;
            }
        }
    }
}