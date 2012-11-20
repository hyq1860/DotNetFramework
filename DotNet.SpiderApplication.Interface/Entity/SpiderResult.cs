// -----------------------------------------------------------------------
// <copyright file="SpiderResult.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// 采集数据结果
    /// </summary>
    [DataContract]
    public class SpiderResult
    {
        [DataMember]
        public string ProductId { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 促销信息
        /// </summary>
        [DataMember]
        public string PromotionInfo { get; set; }

        /// <summary>
        /// 最后采集修改时间
        /// </summary>
        [DataMember]
        public DateTime LastModify { get; set; }

        /// <summary>
        /// 采集消耗时间
        /// </summary>
        [DataMember]
        public double Elapse { get; set; }

        /// <summary>
        /// 此次采集数据是否成功
        /// </summary>
        [DataMember]
        public bool IsSucceed { get; set; }
    }

}
