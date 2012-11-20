// -----------------------------------------------------------------------
// <copyright file="FriendLinkInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FriendLinkInfo
    {
        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Id { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Name { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Type { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Description { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Url { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public string Logo { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int DisplayOrder { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public int Status { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public DateTime InDate { get; set; }


        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public DateTime EditDate { get; set; }
    }
}
