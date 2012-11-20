using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace DotNet.Web.Configuration.Resource
{
    public class Param
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("text")]
        public string Text { get; set; }
    }

    public class ParamsCollection : KeyedCollection<string, Param>
    {
        protected override string GetKeyForItem(Param item)
        {
            return item.Key.ToUpper();
        }
    }
}
