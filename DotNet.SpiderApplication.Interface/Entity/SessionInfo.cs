// -----------------------------------------------------------------------
// <copyright file="SessionInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Serialization;

namespace DotNet.SpiderApplication.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [DataContract]
    [KnownType(typeof(SessionClientInfo))]
    public class SessionInfo
    {
        [DataMember]
        public Guid SessionID { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime LastActivityTime { get; set; }

        [DataMember]
        public SessionClientInfo ClientInfo { get; set; }

        public bool IsTimeout { get; set; }
    }
}
