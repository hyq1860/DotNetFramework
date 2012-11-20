// -----------------------------------------------------------------------
// <copyright file="SpiderParameter.cs" company="">
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

    [DataContract]
    public class SpiderParameter
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ProductId { get; set; }
    }
}
