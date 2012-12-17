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

    /*
     * 协同推荐SlopeOne 算法
     http://www.cnblogs.com/huangxincheng/archive/2012/11/22/2782647.html
     */

    /// <summary>
    /// 商品采集参数
    /// </summary>
    public class SpiderProductInfo : IComparable<SpiderProductInfo>
    {
        public string ProductId { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// 对应的html
        /// </summary>
        public string HtmlSource { get; set; }

        /// <summary>
        /// 当前系统已知最新价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 电商编号
        /// </summary>
        public int ECPlatformId { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentNumber { get; set; }

        /// <summary>
        /// 权重
        /// 权重的计算：根据商城排名+评论数+等等
        /// </summary>
        public int Priority { get; set; }

        public int CompareTo(SpiderProductInfo other)
        {
            if (this.Priority < other.Priority)
            {
                return -1;
            }
            else if (this.Priority > other.Priority)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
