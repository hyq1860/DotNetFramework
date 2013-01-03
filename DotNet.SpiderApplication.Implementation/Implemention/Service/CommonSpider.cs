// -----------------------------------------------------------------------
// <copyright file="CommonSpider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.ServiceModel;
using DotNet.SpiderApplication.Contract.Entity;
using DotNet.SpiderApplication.Contract.WCF;

namespace DotNet.SpiderApplication.Service.Implemention.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 获取采集任务
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CommonSpider:ICommonSpider
    {
        /// <summary>
        /// </summary>
        private static SpiderTaskManager spiderTaskManager;
        
        static CommonSpider()
        {
            spiderTaskManager = new SpiderTaskManager();
        }

        public List<SpiderTask> GetSpiderTask(int count)
        {
            var data = new List<SpiderTask>();
            var originalData = spiderTaskManager.Dequeue(count);
            foreach (var spiderProductInfo in originalData)
            {
                data.Add(new SpiderTask(){Guid = string.Empty,Url = spiderProductInfo.Url});
            }
            return data;
        }
    }
}
