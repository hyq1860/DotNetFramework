using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Runtime.CompilerServices;
using DotNet.Common.Configuration;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;

namespace DotNet.SpiderApplication.Service
{
    public static class SessionManager
    {
        private static object _syncHelper = new object();

        internal static TimeSpan Timeout
        { get; set; }

        /// <summary>
        /// A list containing all of the current session related information.
        /// </summary>
        public static IDictionary<Guid, SessionInfo> CurrentSessionList
        { get; set; }

        /// <summary>
        /// A list of session renew callback, which will be invoked when session timeout.
        /// </summary>
        public static IDictionary<Guid, ISessionCallback> CurrentCallbackList
        { get; set; }

        static SessionManager()
        {
            
            string sessionTimeout = ConfigHelper.ParamsConfig.GetParamValue("SessionTimeout");//ConfigurationManager.AppSettings["SessionTimeout"];
            if (string.IsNullOrEmpty(sessionTimeout))
            {
                throw new Exception("The session timeout application setting is missing");
            }

            double timeoutMinute;
            if (!double.TryParse(sessionTimeout, out timeoutMinute))
            {
                throw new Exception("The session timeout application setting should be of doubdle type.");
            }           

            Timeout = new TimeSpan(0, 0, (int)(timeoutMinute * 60));
            CurrentSessionList = new Dictionary<Guid, SessionInfo>();
            CurrentCallbackList = new Dictionary<Guid, ISessionCallback>();
        }

        /// <summary>
        /// Start a new session.
        /// </summary>
        /// <param name="clientInfo">The client information regarding the current session.</param>
        /// <returns>The session ID in the form of Guid.</returns>
        public static Guid StartSession(SessionClientInfo clientInfo)
        {
            Guid sessionID = Guid.NewGuid();
            ISessionCallback callback = OperationContext.Current.GetCallbackChannel<ISessionCallback>();
            SessionInfo sesionInfo = new SessionInfo() { SessionID = sessionID, StartTime = DateTime.Now, LastActivityTime = DateTime.Now, ClientInfo = clientInfo };
            lock (_syncHelper)
            {
                CurrentSessionList.Add(sessionID, sesionInfo);
                CurrentCallbackList.Add(sessionID, callback);
            }
            return sessionID;
        }

        /// <summary>
        /// Close an existing session.
        /// </summary>
        /// <param name="sessionID">The session ID which uniquly identify a given session.</param>
        public static void EndSession(Guid sessionID)
        {
            if (!CurrentSessionList.ContainsKey(sessionID))
            {
                return;
            }

            lock (_syncHelper)
            {
                CurrentCallbackList.Remove(sessionID);
                CurrentSessionList.Remove(sessionID);
            }
        }

        public delegate void RenewSession(KeyValuePair<Guid, SessionInfo> session);

        /// <summary>
        /// Iterate each of timeout session, and invoke the corresponding callback.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RenewSessions()
        {
            IList<WaitHandle> waitHandleList = new List<WaitHandle>();

            foreach (var session in CurrentSessionList)
            {
                RenewSession renewsession = delegate(KeyValuePair<Guid, SessionInfo> sessionInfo)
                {
                    if (DateTime.Now - sessionInfo.Value.LastActivityTime < Timeout)
                    {
                        return;
                    }
                    try
                    {
                        TimeSpan renewDuration = CurrentCallbackList[sessionInfo.Key].Renew();
                        if (renewDuration.TotalSeconds > 0)
                        {
                            sessionInfo.Value.LastActivityTime += renewDuration;
                        }
                        else
                        {
                            sessionInfo.Value.IsTimeout = true;
                            CurrentCallbackList[session.Key].OnSessionTimeout(sessionInfo.Value);
                        }
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        sessionInfo.Value.IsTimeout = true;
                        return;
                    }
                    
                };

                IAsyncResult result = renewsession.BeginInvoke(session, null, null);
                waitHandleList.Add(result.AsyncWaitHandle);
            }

            if (waitHandleList.Count == 0)
            {
                return;
            }
            WaitHandle.WaitAll(waitHandleList.ToArray<WaitHandle>());
            ClearSessions();
        }

        /// <summary>
        /// Kill one or more sessions.
        /// </summary>
        /// <param name="sessionIDs">The session ID list.</param>
        public static void KillSessions(IList<Guid> sessionIDs)
        {
            lock (_syncHelper)
            {
                foreach (Guid sessionID in sessionIDs)
                {
                    if (!CurrentSessionList.ContainsKey(sessionID))
                    {
                        continue;
                    }

                    SessionInfo sessionInfo = CurrentSessionList[sessionID];
                    CurrentSessionList.Remove(sessionID);
                    CurrentCallbackList[sessionID].OnSessionKilled(sessionInfo);
                    CurrentCallbackList.Remove(sessionID);
                }
            }
        }

        private static void ClearSessions()
        {
            IList<Guid> sessionIDList = new List<Guid>();
            foreach (SessionInfo sessionInfo in CurrentSessionList.Values)
            {
                if (sessionInfo.IsTimeout)
                {
                    sessionIDList.Add(sessionInfo.SessionID);
                }
            }

            lock (_syncHelper)
            {
                Array.ForEach<Guid>(sessionIDList.ToArray<Guid>(), delegate(Guid sessionID)
                {
                    CurrentSessionList.Remove(sessionID);
                    CurrentCallbackList.Remove(sessionID);
                });
            }
        }
    }
}