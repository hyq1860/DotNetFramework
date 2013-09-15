using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet.EnterpriseWebSite.ViewModel
{
    using Newtonsoft.Json;

    public class ComboTree
    {
        public ComboTree()
        {
            IconClass = "icon-ok";
        }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "iconCls")]
        public string IconClass { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<ComboTree> Children { get; set; } 
    }
}