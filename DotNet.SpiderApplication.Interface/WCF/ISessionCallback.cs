using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Contract
{    
    /// <summary>
    /// Callback when Session is killed or timeout.
    /// </summary>
    public interface ISessionCallback
    {
        [OperationContract]
        TimeSpan Renew();

        [OperationContract(IsOneWay = true)]
        void OnSessionKilled(SessionInfo sessionInfo);

        [OperationContract(IsOneWay = true)]
        void OnSessionTimeout(SessionInfo sessionInfo);
    }
}
