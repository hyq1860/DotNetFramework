// -----------------------------------------------------------------------
// <copyright file="ProductInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 商品信息
    /// </summary>
    public class ProductInfo
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 当前系统已知最新价格
        /// </summary>
        public decimal Price { get; set; }

        public string Source { get; set; }

        public int ECPlatformId { get; set; }

        #region 非业务对象
        public List<string> HttpRequestUrls { get; set; }
        #endregion
    }
}
