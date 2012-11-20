// -----------------------------------------------------------------------
// <copyright file="ProductCategoryInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.Entity
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductCategoryInfo:CategoryBase
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        ///<summary>
        ///小图
        ///</summary>
        public string SmallPicture { get; set; }


        ///<summary>
        ///中图
        ///</summary>
        public string MediumPicture { get; set; }


        ///<summary>
        ///大图
        ///</summary>
        public string BigPicture { get; set; }

        public int CategoryId { get; set; }

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
        public int DisplayOrder { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public DateTime InDate { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public DateTime EditDate { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int InUserId { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int EditUserId { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int DataStatus { get; set; }
    }
}
