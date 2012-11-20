// -----------------------------------------------------------------------
// <copyright file="BasicContentInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BasicContentInfo
    {
        #region##BasicContent实体


        ///<summary>
        ///
        ///</summary>
        public int Id { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string Title { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string Category { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string LinkPath { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime InDate { get; set; }

        #endregion
    }
}
