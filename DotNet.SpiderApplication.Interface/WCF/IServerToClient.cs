// -----------------------------------------------------------------------
// <copyright file="IServerToClient.cs" company="">
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
    /// 采集客户端到采集浏览器
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IServerToClient
    {
        [OperationContract(IsOneWay = true)]
        void Register(Guid clientID);

        [OperationContract]
        void ReportStatus(SpiderState state);
    }
}
