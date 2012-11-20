using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DotNet.Common.Logging
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

        [ConfigurationProperty("loggername", IsRequired = true)]
        public string LoggerName
        {
            get { return this["loggername"].ToString(); }
            set
            {
                this["loggername"] = value;
            }
        }
    }
}
