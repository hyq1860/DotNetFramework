// -----------------------------------------------------------------------
// <copyright file="ProductInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// 产品实体类
    /// </summary>
    public class ProductInfo
    {
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
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(Required=Required.Default)]
        public ProductCategoryInfo Category { get; set; }

        ///<summary>
        ///
        ///</summary>
        public double Price { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string Desc { get; set; }

        /// <summary>
        /// 产品内容
        /// </summary>
        public string Content { get; set; }


        ///<summary>
        ///
        ///</summary>
        public bool IsOnline { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string BigPicture { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string SmallPicture { get; set; }


        ///<summary>
        ///
        ///</summary>
        public string MediumPicture { get; set; }


        ///<summary>
        ///规格参数
        ///</summary>
        public string Specifications { get; set; }


        ///<summary>
        ///售后服务
        ///</summary>
        public string AfterService { get; set; }

        public int UserId { get; set; }

        public DateTime InDate { get; set; }

        public string FileUrl { get; set; }

        public bool IsFocusPicture { get; set; }

        public string Keywords { get; set; }

        public string MetaDesc { get; set; }

        public Int64 DisplayOrder { get; set; }
    }
}
