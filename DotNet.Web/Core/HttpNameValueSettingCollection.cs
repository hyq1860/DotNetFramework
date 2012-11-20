using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Web.Core
{
	public class HttpNameValueSettingCollection : Dictionary<Enum, string>
	{
		private HttpNameValueSettingCollection() { }

		public string Get(Enum name)
		{
			if (this.ContainsKey(name))
			{
				return this[name];
			}
			return string.Empty;
		}
	}
}
