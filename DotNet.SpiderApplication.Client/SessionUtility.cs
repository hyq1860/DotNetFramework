using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Windows.Forms;
using System.ServiceModel.Channels;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Client
{
    public static class SessionUtility
    {
        //static SessionUtility()
        //{
        //    Callback = new SessionCallback();
        //    Channel = new DuplexChannelFactory<ISessionManagement>(Callback, "sessionservice").CreateChannel();            
        //}

        public static ISessionManagement Channel
        { get; set; }

        public static ISessionCallback Callback
        { get; set; }

        public static DateTime LastActivityTime
        { get; set; }

        public static Guid SessionID
        { get; set; }

        public static TimeSpan Timeout
        { get; set; }

        public static void StartSession(SessionClientInfo clientInfo)
        {
            TimeSpan timeout;
            SessionID = Channel.StartSession(clientInfo, out timeout);
            Timeout = timeout;
        }

        public static IList<SessionInfo> GetActiveSessions()
        {
            return Channel.GetActiveSessions();
        }

        public static void KillSessions(IList<Guid> sessionIDs)
        {
            Channel.KillSessions(sessionIDs);
        }
    }

}
