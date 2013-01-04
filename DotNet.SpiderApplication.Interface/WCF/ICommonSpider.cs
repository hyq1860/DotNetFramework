// -----------------------------------------------------------------------
// <copyright file="ICommonSpider.cs" company="">
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
    public interface ICommonSpider
    {
        [OperationContract]
        List<SpiderTask> GetSpiderTask(int count);
    }
}
