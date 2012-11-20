using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace DotNet.Data
{
    /// <summary>
    /// AccessProvider
    /// </summary>
    public class AccessProvider:IDbProvider
    {
        #region AccessProvider

        public DbProviderFactory Instance()
        {
            return OleDbFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as OleDbCommand) != null)
            {
                OleDbCommandBuilder.DeriveParameters(cmd as OleDbCommand);
            }
        }

        public DbParameter CreateParameter(string parametersName, DbType dbType, int size)
        {
            OleDbParameter parameter = size > 0 ? new OleDbParameter(parametersName, (OleDbType)dbType, size) : new OleDbParameter(parametersName, (OleDbType)dbType);
            return parameter;
        }

        public bool IsSupportStoredProcedures()
        {
            return false;
        }

        #endregion
    }
}
