// -----------------------------------------------------------------------
// <copyright file="CategoryBase.cs" company="">
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
    /// 嵌套集合分类base
    /// </summary>
    public class CategoryBase
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父编号
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 左值
        /// </summary>
        public int Lft { get; set; }

        /// <summary>
        /// 右值
        /// </summary>
        public int Rgt { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        [JsonProperty(PropertyName = "leval")]
        public int Depth { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [JsonProperty(PropertyName = "isLeaf")]
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "parent")]
        public string Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "expanded")]
        public bool Expanded { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "loaded")]
        public bool Loaded { get; set; }
    }
}
