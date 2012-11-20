using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;

namespace DotNet.Data.Configuration
{
    /// <summary>
    /// sql配置文件对应实体
    /// </summary>
    [Serializable]
	public class DataOperationCommand : ICloneable
	{
		private string commandText;

		private DataOperationsParametersList parametersField;

		private string name;

		private string connectionStringName;

        private CommandType commandType;

        private DatabaseTypeEnum databaseType;

		private int timeOut;

        /// <summary>
        /// 
        /// </summary>
		public DataOperationCommand()
		{
			commandType = CommandType.Text;
			timeOut = 300;
            databaseType = DatabaseTypeEnum.Query;
		}

        /// <summary>
        /// 
        /// </summary>
		[XmlElement("commandText")]
		public string CommandText
		{
			get
			{
				return commandText;
			}
			set
			{
				if(string.IsNullOrEmpty(value))
				{
					return;
				}
				commandText = value.Trim();
			}
		}

        /// <summary>
        /// 
        /// </summary>
		[XmlElement("parameters")]
		public DataOperationsParametersList Parameters
		{
			get
			{
				return parametersField;
			}
			set
			{
				parametersField = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
		[XmlAttribute("connectionStringName")]
        public string ConnectionStringName
		{
			get
			{
                return connectionStringName;
			}
			set
			{
                connectionStringName = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
		[DefaultValueAttribute(CommandType.Text)]
		[XmlAttribute("commandType")]
		public CommandType CommandType
		{
			get
			{
				return commandType;
			}
			set
			{
				commandType = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        [DefaultValueAttribute(DatabaseTypeEnum.Transaction)]
        [XmlAttribute("databaseType")]
        public DatabaseTypeEnum DatabaseType
        {
            get
            {
                return databaseType;
            }
            set
            {
                databaseType = value;
            }
        }
        
		[DefaultValueAttribute(300)]
		[XmlAttribute("timeOut")]
		public int TimeOut
		{
			get
			{
				return timeOut;
			}
			set
			{
				timeOut = value;
			}
		}

		public  DataCommand GetDataCommand()
		{
            return new DataCommand( this );
		}

        //private DbCommand GetDbCommandByFactory()
        //{
        //    DbCommand command = DbCommandFactory.CreateDbCommand();
            
        //    command.CommandText = CommandText;
        //    command.CommandTimeout = TimeOut;
        //    command.CommandType = (CommandType)Enum.Parse(typeof(CommandType), CommandType.ToString());

        //    // todo: populate command
        //    if (Parameters != null && Parameters.ParameterList != null && Parameters.ParameterList.Length > 0)
        //    {
        //        foreach (DataOperationParameter param in Parameters.ParameterList)
        //        {
        //            command.Parameters.Add(param.GetDbParameter());
        //        }
        //    }

        //    return command;
        //}

        #region ICloneable Members

        public object Clone()
        {
            DataOperationCommand cmd = new DataOperationCommand();
            if (!string.IsNullOrEmpty(this.ConnectionStringName))
            {
                cmd.ConnectionStringName = this.ConnectionStringName.Clone().ToString();
            }
            cmd.CommandType = this.CommandType;
            cmd.CommandText = this.CommandText.Clone( ).ToString( );
            cmd.Name = this.Name.Clone( ).ToString( );
            cmd.TimeOut = this.TimeOut;
            if ( this.Parameters != null )
            {
                cmd.Parameters = (DataOperationsParametersList)this.Parameters.Clone( );
            }

            return cmd;
        }

        #endregion
    }

}
