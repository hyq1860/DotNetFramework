using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// Base_Module:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataContract]
    public class Module
    {

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "menuid")]
        public int ModuleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "menuname")]
        public string ModuleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "_parentId")]
        public int ParentId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "isIframe")]
        public bool IsIframe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Enable { get; set; }

        [DataMember(Name = "menus")]
        public List<Module> Child { get; set; }  

        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember(Name = "id")]
        //public int ModuleId { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember(Name = "text")]
        //public string ModuleName { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember]
        //public int ParentId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember(Name = "url")]
        //public string Url { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[DataMember(Name = "icon")]
        //public string Icon { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public int Sort { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool Enable { get; set; }

        //[DataMember(Name = "children")]
        //public List<Module> Child { get; set; }  
    }
}
