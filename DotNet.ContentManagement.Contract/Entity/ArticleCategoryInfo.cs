// -----------------------------------------------------------------------
// <copyright file="ArticleCategoryInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleCategoryInfo
    {
        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Id { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Title { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Name { get; set; }

        public string UrlPath { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Keywords { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string MetaDesc { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int ParentId { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Lft { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Rgt { get; set; }

        public int Depth { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Description { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public long DisplayOrder { get; set; }

        public int DataStatus { get; set; }

        public int InUserId { get; set; }

        public int EditUserId { get; set; }

        public DateTime InDate { get; set; }

        public DateTime EditDate { get; set; }

        /// <summary>
        /// 分类类型
        /// 0：默认
        /// 1：图文
        /// </summary>
        public int CategoryType { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
