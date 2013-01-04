// -----------------------------------------------------------------------
// <copyright file="CommonSpiderServiceProxy.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.WebBrowerInstance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Common;
    using DotNet.SpiderApplication.Contract.Entity;
    using DotNet.SpiderApplication.Contract.WCF;

    /// <summary>
    /// 通用采集任务
    /// </summary>
    public class CommonSpiderServiceProxy : ServiceProxyBase<ICommonSpider>, ICommonSpider
    {
        public CommonSpiderServiceProxy(): base("CommonSpider")
        {
        }

        public List<SpiderTask> GetSpiderTask(int count)
        {
            return this.Invoker.Invoke<List<SpiderTask>>(proxy => proxy.GetSpiderTask(count));
        }
    }

    /// <summary>
    /// 上传采集数据
    /// </summary>
    public class SpiderClientToManageClientProxy:ServiceProxyBase<ISpiderClientToManageClient>,ISpiderClientToManageClient
    {
        public SpiderClientToManageClientProxy(): base("ISpiderClientToManageClient")
        {
        }

        public void TransferData(SpiderResult result)
        {
            this.Invoker.Invoke(proxy => proxy.TransferData(result));
        }

        public void ReportIEVersion(string ieVersion)
        {
            this.Invoker.Invoke(proxy=>proxy.ReportIEVersion(ieVersion));
        }
    }
}
