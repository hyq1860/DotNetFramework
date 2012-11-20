using System.Configuration;

namespace DotNet.Common.Configuration
{
    public class ProviderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ProviderSetting provider = element as ProviderSetting;
            return provider.Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "add";
            }
        }
    }
}
