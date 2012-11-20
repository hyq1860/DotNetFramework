// -----------------------------------------------------------------------
// <copyright file="SpiderProductInfo.cs" company="">
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
    /// 商品采集参数
    /// </summary>
    public class SpiderProductInfo : IComparable<SpiderProductInfo>
    {
        public string ProductId { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 当前系统已知最新价格
        /// </summary>
        public decimal Price { get; set; }

        public int ECPlatformId { get; set; }

        /// <summary>
        /// 权重
        /// 权重的计算：根据商城排名+评论数+等等
        /// </summary>
        public int Priority { get; set; }

        public int CompareTo(SpiderProductInfo other)
        {
            if (this.Priority < other.Priority) return -1;
            else if (this.Priority > other.Priority) return 1;
            else return 0;
        }
    }
}
