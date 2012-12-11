// -----------------------------------------------------------------------
// <copyright file="SpiderTaskManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using DotNet.IoC;
using DotNet.SpiderApplication.Contract;

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

        public SpiderTaskManager()
        {
            if (SpiderUrlQueue==null)
            {
                SpiderUrlQueue=new PriorityQueue<SpiderProductInfo>();
            }
            var data = CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().GetProducts(" where Supplier=1 limit 0,1000");
            foreach (var productInfo in data)
            {
               SpiderUrlQueue.Enqueue(new SpiderProductInfo(){ProductId = productInfo.ProductId,Url=productInfo.Url,ECPlatformId = productInfo.ECPlatformId}); 
            }
        }

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
