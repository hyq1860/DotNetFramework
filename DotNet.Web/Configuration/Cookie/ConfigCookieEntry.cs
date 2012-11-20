using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DotNet.Web.Configuration.Cookie
{
    [DataContract]
    public class ConfigCookieEntry
    {
        private TimeSpan expiresAfter;

        /// <summary>
        /// Gets or sets the name of a cookie.
        /// </summary>
        [XmlAttribute("name")]
        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public string SubName { get; set; }

        /// <summary>
        /// Gets or sets the domain to associate the cookie with.
        /// </summary>
        /// <value>The name of the domain to associate the cookie with. The default value is the current domain. </value>
        [XmlAttribute("domain")]
        [DataMember]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the virtual path to transmit with the current cookie.
        /// </summary>
        [XmlAttribute("path")]
        [DataMember]
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// GGets or sets the expiration for the cookie.
        /// If the cookie never expires, the ExpiresAfter.Ticks = 0.
        /// </summary>        
        [XmlIgnore]
        public TimeSpan ExpiresAfter
        {
            get { return expiresAfter; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("expiresAfter")]
        [DataMember]
        public string ExpiresAfterInner
        {
            get { return null; }
            set { expiresAfter = TimeSpan.Parse(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only. 
        /// </summary>
        /// <value><c>true</c> to transmit the cookie over an SSL connection (HTTPS); otherwise, <c>false</c>. The default value is <c>false</c>. </value>
		[XmlAttribute("secureOnly")]
		[DataMember]
		public bool SecureOnly
		{
			get;
			set;
		}

        /// <summary>
        /// Gets or sets a value that specifies whether a cookie is accessible by client-side script.  
        /// </summary>
        /// <value><c>true</c> if the cookie has the HttpOnly attribute and cannot be accessed through a client-side script; otherwise, <c>false</c>. The default is <c>false</c>. </value>
        [XmlAttribute("hasSubKey")]
        [DataMember]
        public bool HasSubKey
        {
           get;set;
        }

		[XmlAttribute("httpOnly")]
		[DataMember]
		public bool HttpOnly
		{
			get;
			set;
		}

        /// <summary>
        /// Gets a value indicating whether expiration is enabled for the current cookie.
        /// </summary>
        /// <value><c>true</c> if expiration is enabled; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool EnableExpiration
        {
            get { return ExpiresAfter.Ticks != 0; }
        }
    }

    public class ConfigCookieEntryCollection : KeyedCollection<string, ConfigCookieEntry>
    {
        protected override string GetKeyForItem(ConfigCookieEntry item)
        {
            return item.Name.ToUpper();
        }
    }
}