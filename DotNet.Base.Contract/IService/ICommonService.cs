// -----------------------------------------------------------------------
// <copyright file="ICommonService.cs" company="">
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
    public interface ICommonService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryColumn"></param>
        /// <param name="primaryValue"></param>
        /// <param name="orderColumn"></param>
        /// <returns></returns>
        int Down(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryColumn"></param>
        /// <param name="primaryValue"></param>
        /// <param name="orderColumn"></param>
        /// <returns></returns>
        int Up(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber);

        int UpdateIntColumn(string tableName, string columnName, int value, string keyColumn, int primaryKey);

        int MoveUp(string tableName, string keyColumn, string orderColumn, Int64 p1, Int64 p2, Int64 o1, Int64 o2);
        int MoveDown(string tableName, string keyColumn, string orderColumn, Int64 p1, Int64 p2, Int64 o1, Int64 o2);
    }
}
