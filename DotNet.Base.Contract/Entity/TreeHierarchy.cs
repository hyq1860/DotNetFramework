using System;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 功能: 实体类 ()
    /// 创建人：
    /// 创建日期：2011/11/2    
    /// </summary>
    [Serializable]
    public class TreeHierarchy
    {

        #region##TreeHierarchy实体


        /// <summary>
        /// 
        /// </summary>
        public int Ancestor { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int Descendant { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int PathLength { get; set; }


        #endregion
    }
}
