// -----------------------------------------------------------------------
// <copyright file="ICommonDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICommonDataAccess
    {
        int UpdateIntColumn(string tableName, string columnName, int value, string keyColumn, int primaryKey);

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keyColumn">主键列名称</param>
        /// <param name="primaryValue">主键值</param>
        /// <param name="orderColumn">排序列名称</param>
        /// <returns></returns>
        int Up(string tableName, string keyColumn, Int64 primaryValue, string orderColumn,Int64 orderNumber);

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keyColumn">主键列名称</param>
        /// <param name="primaryValue">主键值</param>
        /// <param name="orderColumn">排序列名称</param>
        /// <returns></returns>
        int Down(string tableName, string keyColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber);

        int MoveUp(string tableName, string keyColumn, string orderColumn, Int64 p1, Int64 p2, Int64 o1, Int64 o2);
        int MoveDown(string tableName, string keyColumn, string orderColumn, Int64 p1, Int64 p2, Int64 o1, Int64 o2);
    }
}
