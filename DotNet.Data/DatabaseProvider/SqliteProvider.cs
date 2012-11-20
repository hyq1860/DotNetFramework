using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SQLite;

namespace DotNet.Data
{
    /// <summary>
    /// SqliteProvider
    /// </summary>
    public class SqliteProvider:IDbProvider
    {
        public DbProviderFactory Instance()
        {
            return SQLiteFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as SQLiteCommand) != null)
            {
                //SQLiteCommandBuilder.DeriveParameters(cmd as SQLiteCommand);
            }
        }

        public DbParameter CreateParameter(string parametersName, DbType dbType, int size)
        {
            SQLiteParameter parameter = size > 0 ? new SQLiteParameter(parametersName, dbType, size) : new SQLiteParameter(parametersName, (SqlDbType)dbType);
            return parameter;
        }

        public bool IsSupportStoredProcedures()
        {
            throw new NotImplementedException();
        }
    }
}
