using System.Data;
using System.Data.Common;

namespace DotNet.Data
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// 返回DbProviderFactory实例
        /// </summary>
        /// <returns></returns>
        DbProviderFactory Instance();

        /// <summary>
        /// 检索SQL参数信息并填充
        /// </summary>
        /// <param name="cmd"></param>
        void DeriveParameters(IDbCommand cmd);

        /// <summary>
        /// 创建SQL参数
        /// </summary>
        /// <param name="parametersName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        DbParameter CreateParameter(string parametersName, DbType dbType, int size);

        /// <summary>
        /// 是否支持存储过程
        /// </summary>
        /// <returns></returns>
        bool IsSupportStoredProcedures();
    }
}
