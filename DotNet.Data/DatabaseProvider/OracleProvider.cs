using System.Data;
using System.Data.Common;
//using System.Data.OracleClient;

namespace DotNet.Data.Provider
{
    ///// <summary>
    ///// OracleProvider
    ///// </summary>
    //public class OracleProvider:IDbProvider
    //{
    //    #region IDbProvider 成员
    //    public DbProviderFactory Instance()
    //    {
    //        return OracleClientFactory.Instance;
    //    }

    //    public void DeriveParameters(IDbCommand cmd)
    //    {
    //        if ((cmd as OracleCommand) != null)
    //        {
    //            OracleCommandBuilder.DeriveParameters(cmd as OracleCommand);
    //        }
    //    }

    //    public DbParameter CreateParameter(string parametersName, DbType dbType, int size)
    //    {
    //        OracleParameter parameter = size > 0 ? new OracleParameter(parametersName, (OracleType)dbType, size) : new OracleParameter(parametersName, (OracleType)dbType);
    //        return parameter;
    //    }

    //    public bool IsSupportStoredProcedures()
    //    {
    //        return true;
    //    }
    //    #endregion
    //}
}
