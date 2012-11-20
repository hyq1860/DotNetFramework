// -----------------------------------------------------------------------
// <copyright file="ArticleInfo.cs" company="">
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

    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleInfo
    {
        ///<summary>
        ///文章编号
        ///</summary>
        [DataMember]
        public int Id { get; set; }

        ///<summary>
        ///文章分类
        ///</summary>
        [DataMember]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string TypeName { get; set; }

        public int Type { get; set; }

        [JsonProperty(Required = Required.Default)]
        public ArticleCategoryInfo Category { get; set; }


        ///<summary>
        ///文章主标题
        ///</summary>
        [DataMember]
        public string Title { get; set; }


        ///<summary>
        ///文章副标题
        ///</summary>
        [DataMember]
        public string SubTitle { get; set; }

        /// <summary>
        /// 焦点图
        /// </summary>
        public string FocusPicture { get; set; }

        ///<summary>
        ///摘要
        ///</summary>
        [DataMember]
        public string Summary { get; set; }


        ///<summary>
        ///内容
        ///</summary>
        [DataMember]
        public string Content { get; set; }


        ///<summary>
        ///seo关键字
        ///</summary>
        [DataMember]
        public string Keywords { get; set; }


        ///<summary>
        ///seo描述
        ///</summary>
        [DataMember]
        public string MetaDesc { get; set; }


        ///<summary>
        ///来源
        ///</summary>
        [DataMember]
        public string Source { get; set; }


        ///<summary>
        ///是否允许评论
        ///</summary>
        [DataMember]
        public bool AllowComments { get; set; }


        ///<summary>
        ///点击数
        ///</summary>
        [DataMember]
        public int Clicks { get; set; }


        ///<summary>
        ///阅读密码
        ///</summary>
        [DataMember]
        public string ReadPassword { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int UserId { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public Int64 DisplayOrder { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public DateTime PublishDate { get; set; }


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

        public int DataStatus { get; set; }
    }
}
