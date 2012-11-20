using System;
using System.Collections.Generic;
using DotNet.Common.Configuration;
using DotNet.Data.Configuration;
using DotNet.Common.Logging;

namespace DotNet.Data
{
    /// <summary>
    /// DataCommand Manager
    /// Lite data access 
    /// </summary>
	public static class DataCommandManager
	{

		#region [ fields ]

        private static readonly object CommandFileListSyncObject = new object();

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, DataCommand> dataCommands;


		#endregion

        static DataCommandManager()
        {
            try
            {
                EnsureCommandLoaded();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private static void EnsureCommandLoaded()
        {
            lock (CommandFileListSyncObject)
            {
                // copy from existing hashtable
                Dictionary<string, DataCommand> newCommands = new Dictionary<string, DataCommand>();

                DataOperationConfiguration commands = ConfigManager.GetConfig<DataOperationConfiguration>();
                if (commands == null)
                {
                    throw new DataCommandFileLoadException("");
                }

                if (commands.DataCommandList != null && commands.DataCommandList.Length > 0)
                {
                    foreach (DataOperationCommand cmd in commands.DataCommandList)
                    {
                        if (newCommands.ContainsKey(cmd.Name))
                        {
                            throw new Exception(string.Format("The DataCommand configuration file has a same key {0} now.", cmd.Name));
                        }
                        try
                        {
                            newCommands.Add(cmd.Name, cmd.GetDataCommand());
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("Create '{0}' data command.", cmd.Name), e);
                        }
                    }
                }
                dataCommands = newCommands;
            }
        }

		/// <summary>
		/// Get DataCommand corresponding to the given command name.
		/// </summary>
		/// <param name="name">Name of the DataCommand </param>
		/// <returns>DataCommand</returns>
		/// <exception cref="KeyNotFoundException">the specified DataCommand does not exist.</exception>
		public static DataCommand GetDataCommand(string name)
		{
            if (!dataCommands.ContainsKey(name))
            {
                throw new Exception(string.Format("Command Name [{0}] doesn't exists.", name));
            }

			return dataCommands[name].Clone() as DataCommand;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dataBaseName">DataBase.config里面的Customer</param>
        /// <returns></returns>
        public static DataCommand GetDataCommand(string name, string dataBaseName)
        {
            return GetDataCommand(name, dataBaseName, false);
        }

        public static DataCommand GetDataCommand(string name, string dataBaseName, bool supportTran)
        {
            DataCommand cmd = dataCommands[name].Clone() as DataCommand;
            cmd.ConnectionStrings = ConfigManager.GetConfig<ConnectionStringConfigs>().DatabaseList[dataBaseName].ConnectionStringList[DatabaseTypeEnum.Query.ToString()].ConnectionStrings;
            cmd.SupportTransaction = supportTran;
            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dataBaseName">DataBase.config里面的Customer</param>
        /// <returns></returns>
        public static DataCommand GetDataCommand(string name, string dataBaseName, DatabaseTypeEnum dataBasetype)
        {
            return GetDataCommand(name, dataBaseName, dataBasetype, false);
        }

        public static DataCommand GetDataCommand(string name, string dataBaseName, DatabaseTypeEnum dataBasetype, bool supportTran)
        {
            DataCommand cmd = dataCommands[name].Clone() as DataCommand;
            cmd.ConnectionStrings = ConfigManager.GetConfig<ConnectionStringConfigs>().DatabaseList[dataBaseName].ConnectionStringList[dataBasetype.ToString()].ConnectionStrings;
            cmd.SupportTransaction = supportTran;
            return cmd;
        }

		/// <summary>
		/// Get DataCommand corresponding to the given command name.
		/// </summary>
		/// <param name="name">Name of the DataCommand </param>
		/// <returns>DataCommand</returns>
		/// <exception cref="KeyNotFoundException">the specified DataCommand does not exist.</exception>
		public static DataCommand GetDataCommand(string name, bool supportTran)
		{
			DataCommand command = dataCommands[name].Clone() as DataCommand;
			command.SupportTransaction = supportTran;
			return command;
		}

		/// <summary>
		/// Refreshes the data command while remaining the command's execution context (db connection, transaction, etc).
		/// </summary>
		/// <param name="command">The CMD.</param>
		/// <param name="name">The name.</param>
		public static void RefreshDataCommand(DataCommand command, string name)
		{
			DataCommand cmd = GetDataCommand(name);
			command.CopyCommand(cmd);
		}

		/// <summary>
		/// Refreshes the data command while remaining the command's execution context (db connection, transaction, etc).
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="fromCommand">From command.</param>
		public static void RefreshDataCommand(DataCommand command, DataCommand fromCommand)
		{
			command.CopyCommand(fromCommand);
		}
	}
}
