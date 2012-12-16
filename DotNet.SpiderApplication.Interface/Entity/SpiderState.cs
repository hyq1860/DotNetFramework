// -----------------------------------------------------------------------
// <copyright file="SpiderState.cs" company="">
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
    /// 采集状态
    /// </summary>
    public class SpiderState
    {
        /// <summary>
        /// 采集总任务数
        /// </summary>
        public int TaskCount { get; set; }

        /// <summary>
        /// 当前完成的数
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 当前采集完成的url
        /// </summary>
        public string Url { get; set; }
    }
}
