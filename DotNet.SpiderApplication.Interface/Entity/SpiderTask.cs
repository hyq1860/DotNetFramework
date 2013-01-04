// -----------------------------------------------------------------------
// <copyright file="SpiderTask.cs" company="">
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
    /// 采集任务实体
    /// </summary>
    public class SpiderTask
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
    }
}
