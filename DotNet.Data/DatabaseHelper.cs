using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace DotNet.Data
{
    public class DatabaseHelper
    {
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


        #region ExecuteNonQuery方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(DbProviderFactory factory, DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            DbCommand dbCommand = factory.CreateCommand();//_factory.CreateCommand();
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
        public static int ExecuteNonQuery(DbProviderFactory factory,DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                int returnValue = dbCommand.ExecuteNonQuery();
                dbCommand.Parameters.Clear();
                return returnValue;
            }
        }
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
        public static object ExecuteScalar(DbProviderFactory factory,DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = factory.CreateCommand())//_factory.CreateCommand();
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
        public static object ExecuteScalar(DbProviderFactory factory, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                object returnValue = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return returnValue;
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
        public static DataSet ExecuteDataSet(DbProviderFactory factory, DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
                using (DbDataAdapter da = factory.CreateDataAdapter())
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
        public static DataSet ExecuteDataSet(DbProviderFactory factory, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                using (DbDataAdapter da = factory.CreateDataAdapter())
                {
                    da.SelectCommand = dbCommand;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dbCommand.Parameters.Clear();
                    return ds;
                }
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
        public static DataTable ExecuteDataTable(DbProviderFactory factory, DbConnection dbConnection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, (DbTransaction)null, commandType, commandText, commandParameters);
                using (DbDataAdapter da = factory.CreateDataAdapter())
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
        public static DataTable ExecuteDataTable(DbProviderFactory factory, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
                using (DbDataAdapter da = factory.CreateDataAdapter())
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
        public static DbDataReader ExecuteReader(DbProviderFactory factory, DbConnection dbConnection, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }
            using (DbCommand dbCommand = factory.CreateCommand())
            {
                PrepareCommand(dbCommand, dbConnection, dbTransaction, commandType, commandText, commandParameters);

                DbDataReader dataReader = dbCommand.ExecuteReader();

                bool canClear = true;
                foreach (DbParameter commandParameter in dbCommand.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    dbCommand.Parameters.Clear();
                }

                return dataReader;
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
        public static DbDataReader ExecuteReader(DbProviderFactory factory, DbTransaction dbTransaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }
            if (dbTransaction != null && dbTransaction.Connection == null)
            {
                throw new ArgumentNullException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return ExecuteReader(factory,dbTransaction.Connection, dbTransaction, commandType, commandText, commandParameters);
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

    }
}
