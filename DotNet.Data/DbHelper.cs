using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace DotNet.Data
{
    /// <summary>
    /// 数据库访问助手
    /// </summary>
    public class DbHelper
    {
        #region 私有变量
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static string _connectionstring = null;

        protected static string _providerfactoryname = null;

        protected static string _dbtype = null;

        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        //抽象类
        private static DbProviderFactory _factory;

        ///// <summary>
        ///// 数据接口
        ///// </summary>
        private static IDbProvider _provider;

        /// <summary>
        /// 查询次数统计
        /// </summary>
        //private static int _querycount = 0;

        /// <summary>
        /// Parameters缓存哈希表
        /// </summary>
        private static Hashtable _prameterscache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 进程辅助对象
        /// </summary>
        private static object lockHelper = new object();
        #endregion

        #region 属性
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionstring))
                {
                    //_connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    _connectionstring = @"Server=HYQ-PC\DATACENTER;Database=db;Uid=sa;Pwd=sasa;";
                }
                return _connectionstring;
            }
            //set
            //{
            //    dbConnectionString = value;
            //}
        }

        /// <summary>
        /// 数据库Provider名称
        /// </summary>
        public static string DbProviderFactoryName
        {
            get
            {
                if (_providerfactoryname == null)
                {
                    //_providerfactoryname = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
                    _providerfactoryname = "System.Data.SqlClient";
                }
                return _providerfactoryname;
            }
        }

        /// <summary>
        /// 数据库驱动版本
        /// </summary>
        public static string DbType
        {
            get
            {
                if (_dbtype == null)
                {
                    //_dbtype = ConfigurationManager.AppSettings["DBType"];
                    _dbtype = "SqlServer";
                }
                return _dbtype;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static IDbProvider Provider
        {
            get
            {
                //双重锁定
                if (_provider == null)
                {
                    lock (lockHelper)
                    {
                        if (_provider == null)
                        {
                            try
                            {
                                //反射创建数据库接口
                                _provider = (IDbProvider)Activator.CreateInstance(Type.GetType(string.Format("DotNet.Data.{0}Provider", DbType), false, true));
                            }
                            catch
                            {
                                throw new Exception("配置文件DBType节点数据库类型是否正确");
                            }
                        }
                    }
                }
                return _provider;
            }
        }
        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        //DbProviderFactory抽象类
        public static DbProviderFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    //dbProviderFactory=DbProviderFactories.GetFactory(dbProviderFactoryName);
                    _factory = Provider.Instance();
                }
                return _factory;
            }
        }
        #endregion

        #region 私有方法
        //private static void AttachParameters(DbCommand dbCommand, DbParameter[] commandParameters)
        //{
        //    if (dbCommand == null) throw new ArgumentNullException("command");
        //    if (commandParameters != null)
        //    {
        //        foreach (DbParameter p in commandParameters)
        //        {
        //            if (p != null)
        //            {
        //                // 检查未分配值的输出参数,将其分配以DBNull.Value.
        //                if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
        //                {
        //                    p.Value = DBNull.Value;
        //                }
        //                dbCommand.Parameters.Add(p);
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 参数克隆
        private static DbParameter[] CloneParameters(DbParameter[] originalParameters)
        {
            DbParameter[] clonedParameters = new DbParameter[originalParameters.Length];
            for (int i = 0; i < originalParameters.Length; i++)
            {
                clonedParameters[i] = (DbParameter)((ICloneable)originalParameters[i]).Clone();
            }
            return clonedParameters;
        }
        #endregion

        #region 缓存方法

        /// <summary>
        /// 设置sql操作参数，缓存
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        public static void CacheParameters(string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            string hashKey = ConnectionString + ":" + commandText;
            _prameterscache[hashKey] = commandParameters;
        }
        //获取缓存的参数
        public static DbParameter[] GetCachedParameters(string commandText)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            string hashKey = ConnectionString + ":" + commandText;
            DbParameter[] cachedParameters = _prameterscache[hashKey] as DbParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            return CloneParameters(cachedParameters);
        }
        #endregion

        #region PrepareCommand方法
        private static void PrepareCommand(DbCommand dbCommand, DbConnection dbConnection, DbTransaction dbTransaction, CommandType commandType, string commandText, DbParameter[] commandParameters)
        {
            if (dbCommand == null) throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(commandText) || commandText.Length == 0) throw new ArgumentNullException("commandText");
            //数据库连接未打开，打开之
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            //给命令分配一个数据库连接.
            dbCommand.Connection = dbConnection;
            //设置命令文本(存储过程名或SQL语句)
            dbCommand.CommandText = commandText;
            //分配事务
            if (dbTransaction != null)
            {
                if (dbTransaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                dbCommand.Transaction = dbTransaction;
            }
            //设置命令类型.
            dbCommand.CommandType = commandType;
            //分配命令参数
            if (commandParameters != null)
            {
                //AttachParameters(dbCommand, commandParameters);
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        dbCommand.Parameters.Add(p);
                    }
                }
            }
            return;
        }
        #endregion

        #region 探索运行时的存储过程,返回DbParameter参数数组,初始化参数值为 DBNull.Value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="spName"></param>
        /// <param name="includeReturnValueParameter"></param>
        /// <returns></returns>
        private static DbParameter[] DiscoverSpParameterSet(DbConnection dbConnection, string spName, bool includeReturnValueParameter)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            if (string.IsNullOrEmpty(spName) || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = spName;
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbConnection.Open();
            Provider.DeriveParameters(dbCommand);
            dbConnection.Close();
            //如果不包含返回值参数,将参数集中的每一个参数删除
            if (!includeReturnValueParameter)
            {
                dbCommand.Parameters.RemoveAt(0);
            }
            //创建参数数组
            DbParameter[] discoveredParameters = new DbParameter[dbCommand.Parameters.Count];
            //将dbCommand的parameters参数集复制到discoveredParameters数组
            foreach (DbParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }
        #endregion

        #region 表中字段是否存在
        /// <summary>
        /// 检测表中某个字段及值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int Exists(string key, string value, string tableName)
        {
            string sql = "select count(1) from " + tableName + " where " + key + "='" + value + "'";
            int returnValue = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql, null));
            return returnValue;
        }
        #endregion

        #region 检索返回指定的存储过程的参数集

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="spName"></param>
        /// <param name="includeReturnValueParameter"></param>
        /// <returns></returns>
        private static DbParameter[] GetSpParameterSetInternal(DbConnection dbConnection, string spName, bool includeReturnValueParameter)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            if (string.IsNullOrEmpty(spName))
            {
                throw new ArgumentNullException("spName");
            }
            string hashKey = dbConnection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            DbParameter[] cachedParameters;
            cachedParameters = _prameterscache[hashKey] as DbParameter[];
            if (cachedParameters == null)
            {
                DbParameter[] spParameters = DiscoverSpParameterSet(dbConnection, spName, includeReturnValueParameter);
                _prameterscache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }
            return CloneParameters(cachedParameters);
        }
        #endregion

        #region ExecuteNonQuery方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            DbCommand dbCommand = Factory.CreateCommand();//_factory.CreateCommand();
            PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
            int returnValue = dbCommand.ExecuteNonQuery();
            dbCommand.Parameters.Clear();//清除参数，以便下次利用
            dbConnection.Close();
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                int returnValue = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();
                return returnValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString) || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                return ExecuteNonQuery(dbConnection, commandType, commandText, commandParameters);
            }
        }
        //public static int ExecuteNonQuery(DbConnection dbConnection, string spName, params object[] parameterValues)
        //{
        //    if (dbConnection == null)
        //    {
        //        throw new ArgumentNullException("dbConnection");
        //    }
        //    if (spName == null || spName.Length == 0)
        //    {
        //        throw new ArgumentNullException("spName");
        //    }
        //    if (parameterValues != null && parameterValues.Length > 0)
        //    {
        //        DbParameter[] commandParameters=get
        //    }
        //}
        #endregion

        #region ExecuteScalar 返回结果集中的第一行第一列
        /// <summary>
        /// 返回结果集中的第一行第一列
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())//_factory.CreateCommand();
            {
                PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
                object returnValue = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return returnValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                object returnValue = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return returnValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString) || ConnectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectionString");
            }
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                return ExecuteScalar(dbConnection, commandType, commandText, commandParameters);
            }
        }
        #endregion

        #region ExecuteDataSet()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
                using (DbDataAdapter da = Factory.CreateDataAdapter())
                {
                    da.SelectCommand = dbCommand;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dbCommand.Parameters.Clear();
                    return ds;
                }
            }
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="dbTransaction">The db transaction.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                using (DbDataAdapter da = Factory.CreateDataAdapter())
                {
                    da.SelectCommand = dbCommand;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dbCommand.Parameters.Clear();
                    return ds;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                return ExecuteDataSet(dbConnection, commandType, commandText, commandParameters);
            }
        }
        #endregion

        #region ExecuteDataTable()

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
                using (DbDataAdapter da = Factory.CreateDataAdapter())
                {
                    da.SelectCommand = dbCommand;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dbCommand.Parameters.Clear();
                    DataTable dt = ds.Tables[0];
                    return dt;
                }
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                using (DbDataAdapter da = Factory.CreateDataAdapter())
                {
                    da.SelectCommand = dbCommand;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dbCommand.Parameters.Clear();
                    DataTable dt = ds.Tables[0];
                    return dt;
                }
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException("dbConnectionString");
            }
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                DataSet ds = ExecuteDataSet(dbConnection, commandType, commandText, commandParameters);
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }

        #endregion

        #region ExecuteReader()

        /// <summary>
        /// 返回datareader
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(DbConnection dbConnection, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = Factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, dbTransaction, commandType, commandText, commandParameters);
                using (DbDataReader dataReader = dbCommand.ExecuteReader())
                {
                    dbCommand.Parameters.Clear();
                    return dataReader;
                }
                //DbDataReader dataReader = dbCommand.ExecuteReader();
                //{
                //    dbCommand.Parameters.Clear();
                //    return dataReader;
                //}
            }
        }

        /// <summary>
        /// 返回datareader
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return ExecuteReader(dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// 返回datareader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ArgumentNullException("DBConnectionString");
            }
            DbConnection dbConnection = null;
            try
            {
                dbConnection = Factory.CreateConnection();
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                return ExecuteReader(dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
            }
            catch
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                }
                throw;
            }
        }

        #endregion

        #region 生成参数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateParameter(string paramName, DbType dbType, Int32 size, ParameterDirection direction, object value)
        {
            DbParameter param;
            param = Provider.CreateParameter(paramName, dbType, size);
            param.Direction = direction;
            if (!(direction == ParameterDirection.Output && value == null))
            {
                param.Value = value;
            }
            return param;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateInParameter(string paramName, DbType dbType, Int32 size, object value)
        {
            return CreateParameter(paramName, dbType, size, ParameterDirection.Input, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static DbParameter CreateOutParameter(string paramName, DbType dbType, Int32 size)
        {
            return CreateParameter(paramName, dbType, size, ParameterDirection.Output, null);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 从datareader中读取二进制数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private byte[] GetBytesFromDataReader(int index, IDataReader dataReader)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    long offset = 0;
                    long byteRead = 0;
                    do
                    {
                        byteRead = dataReader.GetBytes(index, offset, buffer, 0, bufferSize);
                        memoryStream.Write(buffer, 0, (int)byteRead);
                        memoryStream.Flush();
                        offset += byteRead;
                    }
                    while (byteRead == bufferSize);
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 从DataRow中读取二进制数据
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private byte[] GetBytesFromDataRow(string columnName, DataRow dataRow)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = (byte[])dataRow[columnName];
                if (buffer != null)
                {
                    int length = buffer.GetUpperBound(0);
                    //获取数组指定纬度的上限
                    memoryStream.Write(buffer, 0, length);
                    memoryStream.Flush();
                }
                return memoryStream.ToArray();
            }
        }

        #endregion

        #region 简单数据库事务

        /// <summary>
        /// Executes the SQL tran.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="sqlString1">The SQL string1.</param>
        /// <param name="sqlString2">The SQL string2.</param>
        public static void ExecuteSqlTran(CommandType commandType, string sqlString1, string sqlString2)
        {
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                DbCommand dbCommand = Factory.CreateCommand();
                dbCommand.Connection = dbConnection;
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                dbCommand.Transaction = dbTransaction;
                try
                {
                    dbCommand.CommandType = commandType;
                    dbCommand.CommandText = sqlString1;
                    dbCommand.ExecuteNonQuery();
                    dbCommand.CommandText = sqlString2;
                    dbCommand.ExecuteNonQuery();
                    dbTransaction.Commit();
                }
                catch (DbException de)
                {
                    dbTransaction.Rollback();
                    throw new Exception(de.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="sqlStringList"></param>
        public static void ExecuteSqlTran(CommandType commandType, List<string> sqlStringList)
        {
            using (DbConnection dbConnection = Factory.CreateConnection())
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
                DbCommand dbCommand = Factory.CreateCommand();
                dbCommand.CommandType = commandType;
                dbCommand.Connection = dbConnection;
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                dbCommand.Transaction = dbTransaction;
                try
                {
                    for (int i = 0; i < sqlStringList.Count; i++)
                    {
                        string strSql = sqlStringList[i];
                        if (strSql.Trim().Length > 1)
                        {
                            dbCommand.CommandText = strSql;
                            dbCommand.ExecuteNonQuery();
                        }
                    }
                    dbTransaction.Commit();
                }
                catch (DbException de)
                {
                    dbTransaction.Rollback();
                    throw new Exception(de.Message);
                }
            }
        }

        #endregion

        #region 其他：采用emit生成实体，分页，合并结构相同的两个datatable等

        public static List<T> ToList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            if (dt == null) return list;
            DataTableEntityBuilder<T> eblist = DataTableEntityBuilder<T>.CreateBuilder(dt.Rows[0]);
            foreach (DataRow info in dt.Rows)
                list.Add(eblist.Build(info));
            dt.Dispose();
            dt = null;
            return list;
        }
        public class DataTableEntityBuilder<T>
        {
            private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });
            private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });
            public delegate T Load(DataRow dataRecord);
            public Load handler;
            private DataTableEntityBuilder() { }
            public T Build(DataRow dataRecord)
            {
                try
                {
                    return handler(dataRecord);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            public static DataTableEntityBuilder<T> CreateBuilder(DataRow dataRow)
            {
                Type type = typeof(T);
                DataTableEntityBuilder<T> dynamicBuilder = new DataTableEntityBuilder<T>();
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity", type, new Type[] { typeof(DataRow) }, type, true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(type);
                //Type[] writeStringArgs = new Type[1];
                //writeStringArgs[0] = typeof (DataRow);
                //newobj: 用于创建引用类型的对象；
                //ldstr:用于创建String对象变量；
                //newarr:用于创建数组型对象；
                //box:在值类型转换为引用类型的对象时，将值类型拷贝纸托管堆上分配内存；
                generator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
                //generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(writeStringArgs));
                generator.Emit(OpCodes.Stloc, result);
                for (int index = 0; index < dataRow.ItemArray.Length; index++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dataRow.Table.Columns[index].ColumnName,
                                                                   BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    //定义两个用来实现 if 语句的 Label（lblNext 为 if语句中的else处，lblEnd为整个if语句之后）  
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        //对对象调用后期绑定方法，并且将返回值推送到计算堆栈上。 
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        //如果 value 为 true、非空或非零，则将控制转移到目标指令。 
                        generator.Emit(OpCodes.Brtrue, endIfLabel);

                        //OpCodes.Ldloc：将指定索引处的局部变量加载到计算堆栈上。
                        generator.Emit(OpCodes.Ldloc, result);
                        //将索引为 0 的参数加载到计算堆栈上。 
                        generator.Emit(OpCodes.Ldarg_0);
                        //generator.Emit(OpCodes.Ldloc_0);

                        //将所提供的 int32 类型的值作为 int32 推送到计算堆栈上。
                        //generator.Emit(OpCodes.Box, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        //
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        //
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);
                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }

        internal class DynamicBuilder<T>
        {
            private const string getItem = "get_Item";

            private const string isDBNull = "IsDBNull";

            private static IDictionary<Type, Type> types = new Dictionary<Type, Type>();

            private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod(getItem, new Type[] { typeof(int) });

            private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod(isDBNull, new Type[] { typeof(int) });

            private delegate T Load(IDataRecord dataRecord);

            private Load handler;

            private DynamicBuilder() { }

            static DynamicBuilder()
            {
                types.Add(typeof(bool), typeof(Nullable<bool>));
                types.Add(typeof(byte), typeof(Nullable<byte>));
                types.Add(typeof(DateTime), typeof(Nullable<DateTime>));
                types.Add(typeof(decimal), typeof(Nullable<decimal>));
                types.Add(typeof(double), typeof(Nullable<double>));
                types.Add(typeof(float), typeof(Nullable<float>));
                types.Add(typeof(Guid), typeof(Nullable<Guid>));
                types.Add(typeof(Int16), typeof(Nullable<Int16>));
                types.Add(typeof(Int32), typeof(Nullable<Int32>));
                types.Add(typeof(Int64), typeof(Nullable<Int64>));
            }

            public T Build(IDataRecord dataRecord)
            {
                return handler(dataRecord);
            }

            public static DynamicBuilder<T> CreateBuilder(IDataRecord dataRecord)
            {
                DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>();

                DynamicMethod method = new DynamicMethod("DynamicCreate", typeof(T),
                        new Type[] { typeof(IDataRecord) }, typeof(T), true);
                ILGenerator generator = method.GetILGenerator();

                LocalBuilder result = generator.DeclareLocal(typeof(T));
                generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);

                for (int i = 0; i < dataRecord.FieldCount; i++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dataRecord.GetName(i));
                    Label endIfLabel = generator.DefineLabel();

                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);

                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);

                        bool isNullable = false;
                        if (propertyInfo.PropertyType.Name.ToLower().Contains("nullable"))
                            isNullable = true;
                        Type type = dataRecord.GetFieldType(i);

                        generator.Emit(OpCodes.Unbox_Any, isNullable ? types[type] : type);

                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                        generator.MarkLabel(endIfLabel);
                    }
                }

                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);

                dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
                return dynamicBuilder;
            }
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connection">SQL数据库连接对象</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(DbConnection connection, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            using (DbCommand cmd = Factory.CreateCommand())
            {
                string Sql = GetPageSql(connection, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount);
                PrepareCommand(cmd, connection, null, CommandType.Text, Sql, commandParameters);
                //cmd.CommandText = Sql;
                DbDataAdapter ap = Factory.CreateDataAdapter();
                ap.SelectCommand = cmd;
                DataSet st = new DataSet();
                ap.Fill(st, "PageResult");
                cmd.Parameters.Clear();
                return st.Tables["PageResult"];
            }
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            using (DbConnection dbconnection = Factory.CreateConnection())
            {
                dbconnection.ConnectionString = ConnectionString;
                dbconnection.Open();
                return ExecutePage(dbconnection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
            }
        }

        /// <summary>
        /// 取得分页的SQL语句
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</param>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>分页的sql语句</returns>
        private static string GetPageSql(DbConnection connection, DbCommand cmd, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            //查询结果集总记录数
            RecordCount = 0;
            //
            PageCount = 0;
            //页面记录条数
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            //查询出总记录数
            string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            //cmd.CommandText = SqlCount;
            //RecordCount = (int)cmd.ExecuteScalar();
            RecordCount = (int)ExecuteScalar(CommandType.Text, SqlCount, null);
            if (RecordCount % PageSize == 0)//总记录是偶数
            {
                PageCount = RecordCount / PageSize;
            }
            else//总记录数是奇数
            {
                PageCount = RecordCount / PageSize + 1;
            }
            if (PageIndex > PageCount)//如果当前页码索引大于总页数
                PageIndex = PageCount;
            if (PageIndex < 1)
                PageIndex = 1;
            string Sql = null;
            if (PageIndex == 1)//如果是第一页
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            }
            else
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += SqlTablesAndWhere + " where (";
                }
                Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                Sql += ")) " + OrderFields;
            }
            return Sql;
        }

        #endregion
    }
}
