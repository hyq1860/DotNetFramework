using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DotNet.Web.Core
{
	public class HttpCookieSetting
	{
		[XmlAttribute("alias")]
		public string Alias { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("domain")]
		public string Domain { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }

		[XmlAttribute("expires")]
		public string InternalExpires
		{
			get { return null; }
			set { Expires = TimeSpan.Parse(value); }
		}

		[XmlIgnore]
		public TimeSpan Expires { get; set; }

		[XmlAttribute("secure")]
		public bool Secure { get; set; }

		[XmlIgnore]
		public bool AutoExpires
		{
			get
			{
				return Expires.Ticks == 0;
			}
		}
		[XmlAttribute("httpOnly")]
		public bool HttpOnly
		{
			get;
			set;
		}
	}
}
