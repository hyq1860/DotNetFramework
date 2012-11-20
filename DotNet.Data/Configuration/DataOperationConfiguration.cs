using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DotNet.Common.Configuration;

namespace DotNet.Data.Configuration
{
    [ConfigFile("Data/DataCommand_*.config")]
    [Serializable]
	[XmlRoot("dataOperations", IsNullable = false)]
	public class DataOperationConfiguration
	{

		/// <remarks/>
        [XmlElement( "dataCommand" )]
        public DataOperationCommand[] DataCommandList
        {
            get;
            set;
        }

		/// <summary>
		/// returns a list of command names this object contains.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetCommandNames()
		{
			if (DataCommandList == null || DataCommandList.Length == 0)
			{
				return new string[0];
			}

			List<string> result = new List<string>(DataCommandList.Length);

			for (int i = 0; i < DataCommandList.Length; i++)
			{
				result.Add(DataCommandList[i].Name);
			}
			return result;
		}
	}
}
