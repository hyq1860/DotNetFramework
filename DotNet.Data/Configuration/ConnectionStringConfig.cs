using System.Collections.ObjectModel;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Data.Configuration
{
    [ConfigFile("ConnectionString.config")]
    [XmlRoot("ConnectionStrings")]
    public class ConnectionStringConfigs
    {
        [XmlElement("Database")]
        public DatabaseCollection DatabaseList { get; set; }
    }

    public class DatabaseCollection : KeyedCollection<string, DataBase>
    {
        protected override string GetKeyForItem(DataBase item)
        {
            return item.Name;
        }
    }

    public class DataBase
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute("retryTimes")]
        public int RetryTimes
        {
            get;
            set;
        }

        [XmlAttribute("providerFactoryName")]
        public string ProviderFactoryName { get; set; }

        [XmlAttribute("isSupportStoredProcedure")]
        public bool IsSupportStoredProcedure { get; set; }

        [XmlElement("dbType")]
        public DatabaseTypeCollection ConnectionStringList { get; set; }
    }

    public class DatabaseType
    {
        [XmlAttribute("name")]
        public DatabaseTypeEnum Name
        {
            get;
            set;
        }

        [XmlElement("ConnectionString")]
        public string[] ConnectionStrings
        {
            get;
            set;
        }
    }


    public enum DatabaseTypeEnum
    {
        Query,
        Transaction
    }

    public class DatabaseTypeCollection : KeyedCollection<string, DatabaseType>
    {
        protected override string GetKeyForItem(DatabaseType item)
        {
            return item.Name.ToString();
        }
    }
}
