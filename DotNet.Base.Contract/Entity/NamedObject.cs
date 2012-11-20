using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 带有属性Name的对象
    /// </summary>
    [Serializable]
    public abstract class NamedObject
    {
        /// <summary>
        /// 数据字典
        /// </summary>
        private Dictionary<string, object> _properties;

        /// <summary>
        /// 
        /// </summary>
        public NamedObject()
        {
            _properties=new Dictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        public NamedObject(Dictionary<string,object> properties)
        {
            if(properties==null)
            {
                throw new ArgumentNullException("数据字典不能为空");
            }
            _properties = properties;
        }

        /// <summary>
        /// 对象数据字典
        /// </summary>
        public Dictionary<string,object> Properties
        {
            get
            {
                if(_properties==null)
                {
                    _properties=new Dictionary<string, object>();
                }
                return _properties;
            }
        }

        /// <summary>
        /// 将数据表转化为属性键值对字典集
        /// </summary>
        /// <param name="dataTable">数据源表</param>
        /// <returns>属性键值对字典集</returns>
        public static List<Dictionary<string,object>> GetNamedObjects(DataTable dataTable)
        {
            List<Dictionary<string,object>> dictList=new List<Dictionary<string, object>>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Dictionary<string,object> dict=new Dictionary<string, object>();
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    dict.Add(dataColumn.ColumnName,dataRow[dataColumn.ColumnName]);
                }
                dictList.Add(dict);
            }
            return dictList;
        }

        /// <summary>
        /// 从读取器枚举出属性键值对字典集
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="mustCloseReader">接收读取后是否强制关闭</param>
        /// <returns>属性键值对字典枚举集</returns>
        public static IEnumerable<Dictionary<string, object>> EnumerateObjects(IDataReader reader, bool mustCloseReader)
        {
            if (reader.IsClosed)
            {
                throw new ArgumentException("reader", "读取器已关闭");
            }
            object[] values = new object[reader.FieldCount];

            while (reader.Read())
            {
                reader.GetValues(values);
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    string key = reader.GetName(i);
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, values[i]);
                    }
                }
                yield return dict;
            }
            if (mustCloseReader)
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 从读取器枚举出通用类型对象集
        /// </summary>
        /// <typeparam name="T">通用基本类型</typeparam>
        /// <param name="reader">数据读取器</param>
        /// <param name="mustCloseReader">接收读取后是否强制关闭</param>
        /// <returns>通用类型对象枚举集</returns>
        public static IEnumerable<T> EnumerateObjects<T>(IDataReader reader, bool mustCloseReader)
        {
            Type t = typeof(T);
            ConstructorInfo ctlInfo = t.GetConstructor(new Type[] { typeof(Dictionary<string, object>) });
            if (ctlInfo == null)
            {
                throw new Exception("泛型T未提供'Dictionary<string, object>'参数类型的构造函数");
            }
            IEnumerable<Dictionary<string, object>> ems = EnumerateObjects(reader, mustCloseReader);
            foreach (Dictionary<string, object> dic in ems)
            {
                object obj = ctlInfo.Invoke(new object[] { dic });
                yield return (T)obj;
            }
        }

        /// <summary>
        /// 从数据表枚举属性键值对字典集
        /// </summary>
        /// <param name="dataTable">数据源表</param>
        /// <returns>属性键值对字典枚举集</returns>
        internal static IEnumerable<Dictionary<string, object>> EnumerateObjects(DataTable dataTable)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                yield return dic;
            }
        }

        /// <summary>
        /// 设置一个属性的值
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        public void SetValue<T>(NamedProperty<T> propName, T propValue)
        {
            if (propName == null)
            {
                throw new ArgumentNullException("propName", "参数不能为空");
            }

            if (!Properties.ContainsKey(propName.Name))
            {
                Properties.Add(propName.Name, propValue == null ? propName.DefaultValue : propValue);
            }
            else
            {
                Properties[propName.Name] = propValue == null ? propName.DefaultValue : propValue;
            }
        }

        /// <summary>
        /// 取得属性的值
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <returns>对象属性值</returns>
        public T GetValue<T>(NamedProperty<T> propName)
        {
            if (propName == null)
            {
                throw new ArgumentNullException("propName", "参数不能为空");
            }
            if (Properties.ContainsKey(propName.Name))
            {
                object obj = Properties[propName.Name];
                if ((obj as DBNull)==null)
                {
                    return propName.DefaultValue;
                }
                return (T)Convert.ChangeType(Properties[propName.Name], typeof(T));
            }
            return propName.DefaultValue;
        }

    }
}
