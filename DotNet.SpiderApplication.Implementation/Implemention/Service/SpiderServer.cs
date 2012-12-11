// -----------------------------------------------------------------------
// <copyright file="SpiderServer.cs" company="">
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
    using System.Text;

    /// <summary>
    /// 获取采集任务
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SpiderServer:ISpiderServer
    {
        private SpiderTaskManager spiderTaskManager = new SpiderTaskManager();
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            File.WriteAllText("z:\\"+System.Guid.NewGuid().ToString(),spiderProduct.Url);
            return new ProductInfo() {Url = spiderProduct.Url};
        }

        public List<SpiderProductInfo> GetSpiderTask(int count)
        {
            return spiderTaskManager.Dequeue(count);
        }
    }
}
