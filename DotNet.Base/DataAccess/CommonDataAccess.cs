// -----------------------------------------------------------------------
// <copyright file="CommonDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommonDataAccess:ICommonDataAccess
    {
        public int UpdateIntColumn(string tableName, string columnName, int value, string keyColumn, int primaryKey)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("CommonUpdateInt"))
            {
                var sql = string.Format("update {0} set {1}={2} where {3}={4};",tableName,columnName,value,keyColumn,primaryKey);
                cmd.CommandText = sql;
                //cmd.SetParameterValue("@TableName", tableName);
                //cmd.SetParameterValue("@ColumnName", columnName);
                //cmd.SetParameterValue("@PrimaryKey", primaryKey);
                //cmd.SetParameterValue("@ColumnValue", value);
                //cmd.SetParameterValue("@KeyColumn", keyColumn);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Up(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber)
        {
            var data = new Dictionary<string, Int64>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("CommonUp"))
            {
                cmd.CommandText = string.Format(cmd.CommandText, tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while(dr.Read())
                    {
                        if (!Convert.IsDBNull(dr[primaryColumn]) && !Convert.IsDBNull(dr[orderColumn]))
                        {
                            data.Add(dr[primaryColumn].ToString(), Convert.ToInt64(dr[orderColumn]));
                        }
                    }
                }

                if (data.Count == 1)
                {
                    return 2;
                }
                else
                {
                    string sql = "update {0} set {1}={2} where {3}={4}";

                    StringBuilder sb=new StringBuilder();
                    foreach (KeyValuePair<string, long> keyValuePair in data)
                    {
                        if(keyValuePair.Key==primaryValue.ToString())
                        {
                            sb.Append(
                                string.Format(
                                    sql,
                                    tableName,
                                    orderColumn,
                                    data.Max(s => s.Value),
                                    primaryColumn,
                                    primaryValue) + ";");

                        }
                        else
                        {
                            sb.Append(
                                string.Format(
                                    sql,
                                    tableName,
                                    orderColumn,
                                    data.Min(s => s.Value),
                                    primaryColumn,
                                    keyValuePair.Key) + ";");
                        }
                    }

                    //var temp = string.Format(sql, tableName, orderColumn, data.Min(s => s.Value), primaryColumn, data.FirstOrDefault().Key) + ";";
                    //temp += string.Format(sql, tableName, orderColumn, data.Max(s => s.Value), primaryColumn, data.LastOrDefault().Key) + ";";
                    cmd.CommandText = sb.ToString();
                    return cmd.ExecuteNonQuery();
                }
                
            }
           
        }

        public int Down(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber)
        {
            var data = new Dictionary<string, Int64>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("CommonDown"))
            {
                cmd.CommandText = string.Format(cmd.CommandText, tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr[primaryColumn]) && !Convert.IsDBNull(dr[orderColumn]))
                        {
                            data.Add(dr[primaryColumn].ToString(), Convert.ToInt64(dr[orderColumn]));
                        }
                    }
                }

                if (data.Count == 1)
                {
                    return 2;
                }
                else
                {
                    string sql = "update {0} set {1}={2} where {3}={4}";

                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, long> keyValuePair in data)
                    {
                        if (keyValuePair.Key == primaryValue.ToString())
                        {
                            sb.Append(
                                string.Format(
                                    sql,
                                    tableName,
                                    orderColumn,
                                    data.Min(s => s.Value),
                                    primaryColumn,
                                    primaryValue) + ";");

                        }
                        else
                        {
                            sb.Append(
                                string.Format(
                                    sql,
                                    tableName,
                                    orderColumn,
                                    data.Max(s => s.Value),
                                    primaryColumn,
                                    keyValuePair.Key) + ";");
                        }
                    }
                    cmd.CommandText = sb.ToString();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int MoveUp(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            using(DataCommand cmd = DataCommandManager.GetDataCommand("CommonUp"))
            {
                string sql = "update {0} set {1}={2} where {3}={4}";

                cmd.CommandText = string.Format(sql, tableName, orderColumn, o2, keyColumn,p1) + ";" + string.Format(sql, tableName, orderColumn, o1,keyColumn, p2);
                return cmd.ExecuteNonQuery();
            }
        }

        public int MoveDown(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("CommonDown"))
            {
                string sql = "update {0} set {1}={2} where {3}={4}";

                cmd.CommandText = string.Format(sql, tableName, orderColumn, o2, keyColumn,p1) + ";" + string.Format(sql, tableName, orderColumn, o1,keyColumn, p2);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
