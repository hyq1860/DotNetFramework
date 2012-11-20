using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DotNet.Data
{
    /// <summary>
    /// SqlServerProvider
    /// </summary>
    public class SqlServerProvider:IDbProvider
    {
        public DbProviderFactory Instance()
        {
            return SqlClientFactory.Instance;
        }
        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as SqlCommand) != null)
            {
                SqlCommandBuilder.DeriveParameters(cmd as SqlCommand);
            }
        }
        public DbParameter CreateParameter(string parametersName, DbType dbType, int size)
        {
            SqlParameter parameter = size > 0 ? new SqlParameter(parametersName, (SqlDbType)dbType, size) : new SqlParameter(parametersName, (SqlDbType)dbType);
            return parameter;
        }

        public bool IsSupportStoredProcedures()
        {
            return true;
        }
    }
}
