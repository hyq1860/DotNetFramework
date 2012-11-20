using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Base.Contract
{
    [Serializable]
    [DataContract]
    public class OrganizationInfo
    {



        /// <summary>
        /// 组织id
        /// </summary>
        [JsonProperty(PropertyName = "OrganizationId")]
        public int OrganizationId { get; set; }


        /// <summary>
        /// 全称
        /// </summary>
        [JsonProperty(PropertyName = "FullName")]
        public string FullName { get; set; }


        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 父编号
        /// </summary>
        [JsonProperty(PropertyName = "_parentId")]
        public int ParentId { get; set; }


        /// <summary>
        /// 法人代表，负责人
        /// </summary>
        [JsonProperty(PropertyName = "CorporateRepresentative")]
        public string CorporateRepresentative { get; set; }


        /// <summary>
        /// 可用
        /// </summary>
        public bool Enabled { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int SortCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

    }
}
