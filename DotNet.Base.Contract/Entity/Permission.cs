using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 功能: 实体类 ()
    /// 创建人：Wilson     
    /// 创建日期：2011/11/2    
    /// </summary>
    [Serializable]
    public class Permission 
    {

        #region##Permission实体


        /// <summary>
        /// 
        /// </summary>
        public int PermissionId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Fullname { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int SortCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }


        #endregion
    }
}
