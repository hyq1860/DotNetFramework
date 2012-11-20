// -----------------------------------------------------------------------
// <copyright file="JQGridDataResult.cs" company="">
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
    /// TODO: Update summary.
    /// </summary>
    public class JQGridDataResult
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty(PropertyName = "pagecount")]
        public int PageCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty(PropertyName = "pageindex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "rows")]
        public object Data { get; set; }
    }
}
