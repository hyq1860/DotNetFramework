// -----------------------------------------------------------------------
// <copyright file="ISpiderServer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.ServiceModel;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Contract.WCF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ServiceContract]
    public interface ISpiderServer
    {
        [OperationContract]
        ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct);

        [OperationContract]
        List<SpiderProductInfo> GetSpiderTask(int count);
    }
}
