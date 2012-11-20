using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace DotNet.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DataOperationParameter : ICloneable
    {
        public DataOperationParameter()
        {
            Direction = ParameterDirection.Input;
            Size = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute( "name" )]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute( "dbType" )]
        public DbType DbType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute( "direction" )]
        public ParameterDirection Direction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute( "size" )]
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlParameter GetDbParameter()
        {
            SqlParameter param = new SqlParameter( );
            param.ParameterName = Name;
            param.DbType = DbType;
            param.Direction = (ParameterDirection)Enum.Parse( typeof( ParameterDirection ), Direction.ToString( ) );

            if ( Size != -1 )
            {
                param.Size = Size;
            }
            return param;
        }

        #region ICloneable Members

        public object Clone()
        {
            DataOperationParameter parm = new DataOperationParameter();
            parm.DbType = this.DbType;
            parm.Direction = this.Direction;
            parm.Name = this.Name;
            parm.Size = this.Size;
            return parm;
        }

        #endregion
    }

    /// <summary>
    /// Sql参数集合实体
    /// </summary>
    public class DataOperationsParametersList : ICloneable
    {
        [XmlElement( "param" )]
        public DataOperationParameter[] ParameterList
        {
            get;
            set;
        }

        #region ICloneable Members

        public object Clone()
        {
            if ( this != null )
            {
                DataOperationsParametersList list = new DataOperationsParametersList( );
                if ( ParameterList != null && ParameterList.Length > 0 )
                {
                    DataOperationParameter[] parms = new DataOperationParameter[ParameterList.Length];
                    for ( int index = 0 ; index < this.ParameterList.Length ; index++ )
                    {
                        parms[index] = (DataOperationParameter)ParameterList[index].Clone( );
                    }

                    list.ParameterList = (DataOperationParameter[])this.ParameterList.Clone( );
                }
                return list;
            }
            return null;
        }

        #endregion
    }
}
