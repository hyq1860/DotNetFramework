using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DotNet.Common.Configuration
{
    [ConfigFile("Params.config")]
    [XmlRoot("webParams")]
    public class ParamsConfiguration
    {
        [XmlElement("params")]
        public ParamsCollection ParamsCollection { get; set; }

        public string GetParamValue(string name)
        {
            Param param = ParamsCollection.Where(c => c.Key.ToLower() == name.ToLower()).SingleOrDefault();
            if (param != null)
            {
                return param.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        public int GetParamIntValue(string name)
        {
            string tempResult = GetParamValue(name);
            if (string.IsNullOrEmpty(tempResult)) return -999;

            int tempInt = -999;
            return (int.TryParse(tempResult, out tempInt)) ? tempInt : -999;
        }
    }
}
