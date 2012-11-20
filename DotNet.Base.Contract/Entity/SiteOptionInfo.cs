// -----------------------------------------------------------------------
// <copyright file="SiteOptionInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteOptionInfo
    {
        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int OptionId { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int SiteId { get; set; }


        ///<summary>
        ///
        ///</summary>
        
        [DataMember(Name = "name")]
        [JsonProperty(PropertyName = "name")]
        public string OptionName { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string OptionKey { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember(Name = "value")]
        [JsonProperty(PropertyName = "value")]
        public string OptionValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "group")]
        [JsonProperty(PropertyName = "group")]
        public string OptionGroup { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int HtmlType { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string AutoLoad { get; set; }

        [JsonProperty(PropertyName = "editor")]
        public string Editor { get; set; }
    }
}
