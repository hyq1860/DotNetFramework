using System;
using System.Collections.Generic;

namespace DotNet.Web.Core
{
	public class HttpCookieSettingCollection : Dictionary<Enum, HttpCookieSetting>
	{
		private HttpCookieSettingCollection() { }
	}
}
