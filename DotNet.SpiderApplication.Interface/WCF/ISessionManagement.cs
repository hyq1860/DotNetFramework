using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Contract
{
    [ServiceContract(CallbackContract = typeof(ISessionCallback))]
    public interface ISessionManagement
    {
        [OperationContract]
        Guid StartSession(SessionClientInfo clientInfo, out TimeSpan timeout);

        [OperationContract]
        void EndSession(Guid sessionID);

        [OperationContract]        
        IList<SessionInfo> GetActiveSessions();

        [OperationContract]
        void KillSessions(IList<Guid> sessionIDs);
    }
}
