using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DotNet.Common.Configuration
{
    public class ProviderConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderCollection Providers
        {
            get { return (ProviderCollection)base["providers"]; }
        }

        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "XMLConfigProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
        }
    }
}
