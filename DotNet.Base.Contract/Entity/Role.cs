using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 功能: 实体类 ()
    /// 创建人：Wilson     
    /// 创建日期：2011/11/2    
    /// </summary>
    [Serializable]
    public class Role //: NamedObject
    {
        //private NamedProperty<int> _roleId = NamedProperty<int>.Create("RoleId", 0);

        //private NamedProperty<string> _roleName = NamedProperty<string>.Create("RoleName", "");
        #region##Base_Role实体

        //public Role(Dictionary<string,object> data):base(data)
        //{
            
        //}

        public Role()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public int RoleId
        {
            get; set; //get { return GetValue<int>(_roleId); }
            //set{SetValue<int>(_roleId,value);}
        }


        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            get; set; //get { return GetValue<string>(_roleName); }
            //set{SetValue<string>(_roleName,value);}
        }


        /// <summary>
        /// 
        /// </summary>
        public int SortCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }


        #endregion
    }

    [Serializable]
    [DataContract]
    public class RoleExtension
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }
    }
}
