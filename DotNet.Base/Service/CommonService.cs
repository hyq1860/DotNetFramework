// -----------------------------------------------------------------------
// <copyright file="CommonService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract;
    using DotNet.Base.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommonService : ICommonService
    {
        private ICommonDataAccess _dataAccess=new CommonDataAccess();

        public int Down(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber)
        {
            return _dataAccess.Down(tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
        }

        public int Up(string tableName, string primaryColumn, Int64 primaryValue, string orderColumn, Int64 orderNumber)
        {
            return _dataAccess.Up(tableName, primaryColumn, primaryValue, orderColumn,orderNumber);
        }

        public int UpdateIntColumn(string tableName, string columnName, int value, string keyColumn, int primaryKey)
        {
            return _dataAccess.UpdateIntColumn(tableName, columnName, value,keyColumn,primaryKey );
        }

        public int MoveUp(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            return _dataAccess.MoveUp(tableName, keyColumn, orderColumn, p1, p2, o1, o2);
        }

        public int MoveDown(string tableName, string keyColumn, string orderColumn, long p1, long p2, long o1, long o2)
        {
            return _dataAccess.MoveDown(tableName, keyColumn, orderColumn, p1, p2, o1, o2);
        }
    }
}
