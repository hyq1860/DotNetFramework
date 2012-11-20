// -----------------------------------------------------------------------
// <copyright file="SessionClientInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [DataContract]
    public class SessionClientInfo
    {
        [DataMember]
        public string IPAddress { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public IDictionary<string, string> ExtendedProperties{ get; set; }
    }
}
