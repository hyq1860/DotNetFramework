// -----------------------------------------------------------------------
// <copyright file="SpiderTaskManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Service.Implemention.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Common.Collections;
    using DotNet.SpiderApplication.Contract.Entity;

    /// <summary>
    /// 采集任务管理
    /// </summary>
    public class SpiderTaskManager
    {
        private PriorityQueue<SpiderProductInfo> SpiderUrlQueue { get; set; }

        public void Enqueue(SpiderProductInfo spiderProduct)
        {
            SpiderUrlQueue.Enqueue(spiderProduct);
        }

        /// <summary>
        /// 领取抓取任务
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<SpiderProductInfo> Dequeue(int count)
        {
            var data = new List<SpiderProductInfo>();
            for (int i = 0; i < count; i++)
            {
                data.Add(this.SpiderUrlQueue.Dequeue());
            }

            return data;
        }
    }
}
