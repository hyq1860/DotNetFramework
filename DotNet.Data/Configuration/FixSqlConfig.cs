using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Data.Configuration
{
    [ConfigFile("Data/FixSql_*.config")]
    [XmlRoot("fixSqls")]
    public class FixSqlConfig
    {
        [XmlElement("fixSql")]
        public FixSqlCollection FixSqlList { get; set; }

        public FixSql GetFixSqlByName(string name)
        {
            FixSql tempResult = null;
            if (FixSqlList != null)
            {
                foreach (FixSql fixSql in FixSqlList)
                {
                    if(fixSql.Name.ToUpper()==name)
                    {
                        tempResult = fixSql;
                        continue;
                    }
                }
                //tempResult = (from c in FixSqlList where c.Name.ToUpper() == name.ToUpper() select c).Take(1).SingleOrDefault();
            }

            if (tempResult == null) tempResult = new FixSql();

            return tempResult;
        }

        public FixSql GetAndCloneFixSqlByName(string name)
        {
            FixSql tempResult = GetFixSqlByName(name);
            if (tempResult == null) return null;

            FixSql tempInfo = tempResult.Clone();

            return tempInfo;
        }
    }

    public class FixSqlCollection : KeyedCollection<string, FixSql>
    {
        protected override string GetKeyForItem(FixSql item)
        {
            return item.Name;
        }
    }

    [XmlRoot("fixSql")]
    public class FixSql
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("database")]
        public string DatabaseName { get; set; }

        [DefaultValueAttribute(DatabaseTypeEnum.Query)]
        [XmlAttribute("databaseType")]
        public DatabaseTypeEnum databaseType { get; set; }

        [XmlElement("selectField")]
        public SelectField SelectFieldInfo { get; set; }

        [XmlElement("tableName")]
        public TableName TableNameInfo { get; set; }

        [XmlElement("orderField")]
        public OrderField OrderFieldInfo { get; set; }

        public FixSql Clone()
        {
            FixSql tempInfo = new FixSql();
            tempInfo.SelectFieldInfo = new SelectField();
            tempInfo.TableNameInfo = new TableName();
            tempInfo.OrderFieldInfo = new OrderField();

            tempInfo.Name = this.Name;
            tempInfo.SelectFieldInfo.Value = this.SelectFieldInfo.Value;
            tempInfo.TableNameInfo.Value = this.TableNameInfo.Value;
            tempInfo.OrderFieldInfo.Value = this.OrderFieldInfo.Value;

            return tempInfo;
        }
    }

    [XmlRoot("selectField")]
    public class SelectField
    {
        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot("tableName")]
    public class TableName
    {
        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot("orderField")]
    public class OrderField
    {
        [XmlText]
        public string Value { get; set; }
    }
}
