using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DotNet.Common.Configuration;
using DotNet.Data.Configuration;
using DotNet.Common.Logging;

namespace DotNet.Data
{
	/// <summary>
	/// Lite data access for Sqlite,SqlServer,MySql,Oracle
	/// </summary>
	public class DataCommand : ICloneable, IDisposable
	{
		#region [ Variables ]

		private int connectionStringIndex = 0;

		private int currentRetryCount = 0;


		private int currentConnectionReTryCount = 0;

        /// <summary>
        /// 进程辅助对象
        /// </summary>
        private object lockHelper = new object();

		protected DataOperationCommand operationCommand = null;

        // 数据库相关抽象参数
        private DbTransaction dbTransaction = null;
	    //private DbTransaction dbTransaction = null;

        private DbConnection dbConnection = null;
	    //private DbConnection dbConnection = null;

        private DbParameter[] dbParameters = null;
	    //private DbParameter[] dbParameters = null;

        //数据库相关抽象参数

        //
        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        //抽象类
        private DbProviderFactory _dbProviderFactory;

        ///// <summary>
        ///// 数据接口
        ///// </summary>
        private IDbProvider _provider;

        /// <summary>
        /// 
        /// </summary>
        protected string _providerfactoryname = null;


        /// <summary>
        /// 
        /// </summary>
        private IDbProvider Provider
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
                                _provider = (IDbProvider)Activator.CreateInstance(Type.GetType(string.Format("DotNet.Data.{0}Provider", ProviderFactoryName), false, true));
                            }
                            catch
                            {
                                throw new Exception("Database,providerFactoryName错误");
                            }
                        }
                    }
                }
                return _provider;
            }
        }

        private string ProviderFactoryName
        {
            get
            {
                try
                {
                    _providerfactoryname=ConfigManager.GetConfig<ConnectionStringConfigs>().DatabaseList[
                        operationCommand.ConnectionStringName].ProviderFactoryName;
                }
                catch
                {
                    throw new Exception("Database,providerFactoryName错误");
                }
                return _providerfactoryname;
            }
        }

        /// <summary>
        /// DbProviderFactory实例
        /// </summary>
        //DbProviderFactory抽象类
        public DbProviderFactory DbFactory
        {
            get { return _dbProviderFactory ?? (_dbProviderFactory = Provider.Instance()); }
        }
        //
		private bool supportTransaction = false;

		private bool isParametersDirty = false;

		private const string LOG_CATEGORY_NAME = "DotNet.Data.DataCommand";

		#endregion

		#region [ Properites ]


		internal DataOperationCommand OperationCommand
		{
			get { return operationCommand; }
		}

		internal String[] ConnectionStrings
		{
			get;
			set;
		}

		public string CommandText
		{
			get
			{
				return this.OperationCommand.CommandText;
			}
			set
			{
				this.OperationCommand.CommandText = value;
			}
		}

		#endregion

		#region 构造函数

		internal DataCommand(DataOperationCommand command)
		{
			operationCommand = command;

			if (!string.IsNullOrEmpty(command.ConnectionStringName))
			{
				ConnectionStrings = GetConnectionStringByName(command.ConnectionStringName, command.DatabaseType);
			}

			supportTransaction = false;

			if (command.Parameters != null && command.Parameters.ParameterList != null
				&& command.Parameters.ParameterList.Length > 0)
			{
				dbParameters = new SqlParameter[command.Parameters.ParameterList.Length];
				for (int index = 0; index < command.Parameters.ParameterList.Length; index++)
				{
					DataOperationParameter param = command.Parameters.ParameterList[index];
					dbParameters[index] = (param.GetDbParameter());
				}
			}

		}

		private DataCommand()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public object Clone()
		{
			DataCommand cmd = new DataCommand();
			cmd.dbConnection = this.dbConnection;
			if (this.ConnectionStrings != null && this.ConnectionStrings.Length != 0)
			{
				cmd.ConnectionStrings = (string[])this.ConnectionStrings.Clone();
			}
			cmd.dbTransaction = this.dbTransaction;
			cmd.supportTransaction = this.supportTransaction;
			if (this.dbParameters != null && this.dbParameters.Length > 0)
			{
                DbParameter[] parms = new DbParameter[this.dbParameters.Length];
				for (int index = 0; index < this.dbParameters.Length; index++)
				{
                    DbParameter param = DbFactory.CreateParameter();
					param.ParameterName = this.dbParameters[index].ParameterName;
					param.DbType = this.dbParameters[index].DbType;
					param.Direction = this.dbParameters[index].Direction;

					if (this.dbParameters[index].Size != -1)
					{
						param.Size = this.dbParameters[index].Size;
					}

					parms[index] = param;
				}
				cmd.dbParameters = parms;
			}

			cmd.operationCommand = CloneCommand(this.operationCommand);

			return cmd;
		}

		/// <summary>
		/// Clones the command.
		/// </summary>
		/// <param name="command">The CMD.</param>
		/// <returns></returns>
		private static DataOperationCommand CloneCommand(DataOperationCommand cmd)
		{
			if (cmd == null)
			{
				return null;
			}

			if (cmd is ICloneable)
			{
				return ((ICloneable)cmd).Clone() as DataOperationCommand;
			}
			else
			{
				throw new ApplicationException("A class that implements IClonable is expected.");
			}
		}

		/// <summary>
		/// Copies the command from command while keeping the dbTransaction context.
		/// </summary>
		/// <param name="command">The command.</param>
		internal void CopyCommand(DataCommand command)
		{
			operationCommand = CloneCommand(command.operationCommand);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this command is executed in a support dbTransaction.
		/// If the command is executed in a dbTransaction and the dbTransaction is not spcifically committed using CommitTransaction(),
		/// the underlying dbTransaction will be rolled back.
		/// </summary>
		/// <value><c>true</c> if [support dbTransaction]; otherwise, <c>false</c>.</value>
		public bool SupportTransaction
		{
			get { return supportTransaction; }
			internal set
			{
				supportTransaction = value;
			}
		}

		#endregion

		#region [ Parameters ]

		/// <summary>
		/// get a parameter value
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns></returns>
		public object GetParameterValue(string paramName)
		{
			foreach (DbParameter param in dbParameters)
			{
				if (param.ParameterName == paramName)
				{
					return param.Value;
				}
			}
			return null;
		}

		public DataCommand SetParameterValue(string paramName, object val)
		{
			foreach (DbParameter param in dbParameters)
			{
				if (param.ParameterName == paramName)
				{
					param.Value = val;
				}
			}

			return this;
		}

		public void SetParameters(DbParameter[] parameters)
		{
			this.dbParameters = parameters;
		}

		#endregion

		#region [ Connection && Transaction ]

		private void PrepareConnectionAndTransaction()
		{
            dbConnection = DbFactory.CreateConnection();
			dbConnection.ConnectionString = this.ConnectionStrings[connectionStringIndex];

			if (supportTransaction && dbTransaction == null)
			{
				try
				{
					if (dbConnection.State != ConnectionState.Open)
					{
						dbConnection.Open();
					}
				}
				catch
				{
					dbConnection = null;
					return;
				}

				try
				{
					dbTransaction = dbConnection.BeginTransaction();
				}
				catch
				{
					dbConnection.Close();
					dbConnection = null;
					dbTransaction = null;
				}
			}

			if (isParametersDirty && this.dbParameters != null && this.dbParameters.Length > 0)
			{
                DbParameter[] parms = new DbParameter[this.dbParameters.Length];
				for (int index = 0; index < this.dbParameters.Length; index++)
				{
					DbParameter param = DbFactory.CreateParameter();
					param.ParameterName = this.dbParameters[index].ParameterName;
					param.DbType = this.dbParameters[index].DbType;
					param.Direction = this.dbParameters[index].Direction;
					param.Value = this.dbParameters[index].Value;

					if (this.dbParameters[index].Size != -1)
					{
						param.Size = this.dbParameters[index].Size;
					}

					parms[index] = param;
				}
				this.dbParameters = parms;
			}
		}

		/// <summary>
		/// Commits the dbTransaction.
		/// </summary>
		public void CommitTransaction()
		{
			try
			{
				if (dbTransaction != null)
				{
					if (dbConnection.State != ConnectionState.Open)
					{
						dbConnection.Open();
					}
					dbTransaction.Commit();
				}
			}
			finally
			{
				if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
				{
					dbConnection.Close();
				}

				dbConnection = null;
				dbTransaction = null;
			}
		}

		/// <summary>
		/// Rolls back the dbTransaction.
		/// </summary>
		public void RollbackTransaction()
		{
			try
			{
				dbTransaction.Rollback();
			}
			finally
			{
				if (supportTransaction && dbConnection != null)
				{
					dbConnection.Close();
				}
				dbConnection = null;
				dbTransaction = null;
			}
		}

		#endregion

		#region [ Execution ]

		#region [ ExecuteScalar ]

		public T ExecuteScalar<T>()
		{
			Exception ex = null;
			while (ProcessExceptionAndReTry(ex))
			{
				try
				{
					return TryExecuteScalar<T>();
				}
				catch (Exception iex)
				{
					ex = iex;
				}
			}

			LogExecutionError(ex);
			throw ex;

		}

		private T TryExecuteScalar<T>()
		{
			try
			{
				PrepareConnectionAndTransaction();
				if (dbTransaction == null)
				{
                    var result=DatabaseHelper.ExecuteScalar(DbFactory,this.dbConnection
						, operationCommand.CommandType
						, operationCommand.CommandText
						, dbParameters);
				    return result == null ? default(T) : (T)result;
				}
				else
				{
                    var result=DatabaseHelper.ExecuteScalar(DbFactory,this.dbTransaction, operationCommand.CommandType
						, operationCommand.CommandText, dbParameters);
                    return result == null ? default(T) : (T)result;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
				{
					dbConnection.Close();
				}

				dbConnection = null;
				dbTransaction = null;
			}
		}

		public object ExecuteScalar()
		{
			Exception ex = null;
			while (ProcessExceptionAndReTry(ex))
			{
				try
				{
					return TryExecuteScalar();
				}
				catch (Exception iex)
				{
					ex = iex;
				}
			}

			LogExecutionError(ex);
			throw ex;
		}

		private object TryExecuteScalar()
		{
			try
			{
				PrepareConnectionAndTransaction();
				if (dbTransaction == null)
				{
					return DatabaseHelper.ExecuteScalar(DbFactory,this.dbConnection
						, operationCommand.CommandType
						, operationCommand.CommandText
						, dbParameters);
				}
				else
				{
					return DatabaseHelper.ExecuteScalar(DbFactory,this.dbTransaction, operationCommand.CommandType
						, operationCommand.CommandText, dbParameters);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!SupportTransaction)
				{
					if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
					{
						dbConnection.Close();
					}

					dbConnection = null;
					dbTransaction = null;
				}
			}
		}

		#endregion

		#region [ ExecuteNonQuery ]

		public int ExecuteNonQuery()
		{
			Exception ex = null;
			while (ProcessExceptionAndReTry(ex))
			{
				try
				{
					return TryExecuteNonQuery();
				}
				catch (Exception iex)
				{
					ex = iex;
				}
			}

			LogExecutionError(ex);
			throw ex;

		}

		private int TryExecuteNonQuery()
		{
			try
			{
				PrepareConnectionAndTransaction();

				if (dbTransaction == null)
				{
					return DatabaseHelper.ExecuteNonQuery(DbFactory,this.dbConnection
						, operationCommand.CommandType
						, operationCommand.CommandText
						, dbParameters);
				}
				else
				{
					return DatabaseHelper.ExecuteNonQuery(DbFactory,this.dbTransaction, operationCommand.CommandType
						, operationCommand.CommandText, dbParameters);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!SupportTransaction)
				{
					if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
					{
						dbConnection.Close();
					}

					dbConnection = null;
					dbTransaction = null;
				}
			}
		}

		#endregion

		#region [ ExecuteDataReader ]

		public IDataReader ExecuteDataReader()
		{
			Exception ex = null;
			while (ProcessExceptionAndReTry(ex))
			{
				try
				{
					return TryExecuteDataReader();
				}
				catch (Exception iex)
				{
					ex = iex;
				}
			}

			LogExecutionError(ex);
			throw ex;

		}

		private IDataReader TryExecuteDataReader()
		{
			try
			{
				PrepareConnectionAndTransaction();

				if (dbTransaction == null)
				{
					IDataReader reader = DatabaseHelper.ExecuteReader(DbFactory,this.dbConnection,this.dbTransaction,operationCommand.CommandType
							, operationCommand.CommandText, dbParameters);

					return reader;
				}
				else
				{
					IDataReader reader = DatabaseHelper.ExecuteReader(DbFactory,this.dbTransaction, operationCommand.CommandType
							, operationCommand.CommandText, dbParameters);

					return reader;
				}
			}
			catch
			{

				if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
				{
					dbConnection.Close();
				}

				dbConnection = null;
				dbTransaction = null;

				throw;

			}
		}

		#endregion

		#region [ ExecuteDataSet ]

		public DataSet ExecuteDataSet()
		{
			Exception ex = null;
			while (ProcessExceptionAndReTry(ex))
			{
				try
				{
					return TryExecuteDataSet();
				}
				catch (Exception iex)
				{
					ex = iex;
				}
			}

			throw ex;
		}

		private DataSet TryExecuteDataSet()
		{
			try
			{
				PrepareConnectionAndTransaction();

                return DatabaseHelper.ExecuteDataSet(DbFactory,this.dbConnection
					, operationCommand.CommandType
					, operationCommand.CommandText
				   , dbParameters);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
				{
					dbConnection.Close();
				}

				dbConnection = null;
				dbTransaction = null;
			}
		}

		#endregion

		#region  [ ExecuteDataTable ]

		public DataTable ExecuteDataTable()
		{
			return ExecuteDataSet().Tables[0];
		}

		#endregion

		#endregion

		#region [ IDispose ]
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool isDisposing)
		{
			// if called in Dispose, commit dbTransaction.
			// otherwise, let the runtime perform GC.
			if (isDisposing)
			{
				CommitTransaction();
			}
		}
		#endregion

		#region [ Helpers ]

		private bool ProcessExceptionAndReTry(Exception ex)
		{
			if (ex == null)
			{
				isParametersDirty = false;
				currentRetryCount = 0;
				currentConnectionReTryCount = 0;
				currentRetryCount++;
			}
			else
			{
				isParametersDirty = true;
				if (currentConnectionReTryCount == 0)
				{
					LogExecutionReTry();
				}

				currentConnectionReTryCount++;

				bool isNextConnection = false;

				//一个链接字符串可以调用3次，如果三次都调用失败，则调用下一个Connectionstring
				if (currentConnectionReTryCount >= GetRetryTimes(operationCommand.ConnectionStringName))
				{
					currentRetryCount++;
					isNextConnection = true;
				}

				if (currentRetryCount >= ConnectionStrings.Length)
				{
					return false;
				}

				if (isNextConnection)
				{
					connectionStringIndex++;
					currentConnectionReTryCount = 0;

					if (connectionStringIndex >= ConnectionStrings.Length)
					{
						connectionStringIndex = 0;
					}
				}
			}

			return true;
		}

		private int GetRetryTimes(string connectionStringName)
		{
			try
			{
				return ConfigManager.GetConfig<ConnectionStringConfigs>().DatabaseList[operationCommand.ConnectionStringName].RetryTimes;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}

			return 0;
		}

		private string[] GetConnectionStringByName(string connectionStringName, DatabaseTypeEnum type)
		{
			try
			{
				return ConfigManager.GetConfig<ConnectionStringConfigs>().DatabaseList[connectionStringName].ConnectionStringList[type.ToString()].ConnectionStrings;
			}
			catch (Exception ex)
			{
				throw new Exception("Can't find connectionString key: [{0}] in connectionString config.", ex);
			}
		}

		private void LogExecutionError(Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("DataCommand Execution error, command text:");
			sb.AppendLine(operationCommand.CommandText);
			if (dbParameters != null && dbParameters.Length > 0)
			{
				sb.Append(System.Environment.NewLine);
				sb.Append("Command Parameters:");
				foreach (DbParameter parameter in dbParameters)
				{
					sb.Append(parameter.ParameterName);
					sb.Append("=");
					sb.Append(parameter.Value);
					sb.Append(",");
				}
			}
			sb.Append(System.Environment.NewLine);
			sb.AppendLine("Exception: ");
			sb.AppendLine(ex.ToString());

			DotNet.Common.Logging.Logger.Log(LOG_CATEGORY_NAME, LogLevel.Error, sb.ToString());

			throw ex;
		}

		private void LogExecutionReTry()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Database execution retry log, command text:");
			sb.AppendLine(operationCommand.CommandText);
			if (dbParameters != null && dbParameters.Length > 0)
			{
				sb.Append(System.Environment.NewLine);
				sb.Append("Command Parameters:");
				for (int index = 0; index < dbParameters.Length; index++)
				{
					DbParameter parameter = dbParameters[index];
					sb.Append(parameter.ParameterName);
					sb.Append("=");
					sb.Append(parameter.Value);
					if (index < dbParameters.Length)
					{
						sb.Append(",");
					}
				}
			}
			sb.AppendLine(System.Environment.NewLine);
			sb.AppendLine("Connection String Names :");
			sb.AppendLine(System.Environment.NewLine);
			for (int i = 0; i < ConnectionStrings.Length; i++)
			{
				//junyu [2011-7-12]
				//remove passwords
				string connString = this.ConnectionStrings[i];
				int idx = connString.IndexOf("Password=", StringComparison.OrdinalIgnoreCase);
				sb.AppendLine(connString.Substring(0, idx == -1 ? 50 : idx));
				sb.AppendLine(System.Environment.NewLine);
			}

			DotNet.Common.Logging.Logger.Log(LOG_CATEGORY_NAME, LogLevel.Info, sb.ToString());

		}

		#endregion
	}
}
