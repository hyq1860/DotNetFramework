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
    public class Tree 
    {


        #region##Base_Tree实体


        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }


        #endregion
    }
}
