using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DotNet.Common.Utility;

namespace DotNet.Web.Core
{
	public class HttpNameValueCollection : Dictionary<string, string>
	{
		public static CustomRegex HttpNameValueFormat = new CustomRegex(@"^([^=]+)=(.*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public HttpNameValueCollection() :
			base(new CaseInsensitiveStringEqualityComparer())
		{ }

		public HttpNameValueCollection(IDictionary<string, string> nameValues) :
			base(nameValues, new CaseInsensitiveStringEqualityComparer())
		{ }

		public HttpNameValueCollection(NameValueCollection nameValues) :
			base(new CaseInsensitiveStringEqualityComparer())
		{
			this.Add(nameValues);
		}

		/// <summary>
		/// Create Http Name Values collection by parse a http Name Values string
		/// </summary>
		/// <param name="nameValuesString">a=b&b=c</param>
		public static HttpNameValueCollection Create(string nameValuesString)
		{
			HttpNameValueCollection self = new HttpNameValueCollection();

			if (!string.IsNullOrEmpty(nameValuesString))
			{
				string[] pairs = nameValuesString.Split('&');
				foreach (string pair in pairs)
				{
					if (!string.IsNullOrEmpty(pair))
					{
						if (HttpNameValueFormat.IsMatch(pair))
						{
							Match m = HttpNameValueFormat.Regex.Match(pair);
							self.Add(Decode(m.Result("$1")), Decode(m.Result("$2")));
						}
					}
				}
			}

			return self;
		}

		/// <summary>
		/// Create Http Name Values collection by parse a http Name Values string
		/// </summary>
		/// <param name="nameValuesString">a=b&b=c</param>
		/// <param name="encoding">encoding</param>
		public static HttpNameValueCollection Create(string nameValuesString, Encoding encoding)
		{
			HttpNameValueCollection self = new HttpNameValueCollection();

			if (!string.IsNullOrEmpty(nameValuesString))
			{
				string[] pairs = nameValuesString.Split('&');
				foreach (string pair in pairs)
				{
					if (!string.IsNullOrEmpty(pair))
					{
						if (HttpNameValueFormat.IsMatch(pair))
						{
							Match m = HttpNameValueFormat.Regex.Match(pair);
							self.Add(Decode(m.Result("$1")), Decode(m.Result("$2"), encoding));
						}
					}
				}
			}

			return self;
		}

		public void Deserialize(string nameValuesString)
		{
			this.Clear();
			this.Add(HttpNameValueCollection.Create(nameValuesString));
		}

		public void Add(NameValueCollection nameValues)
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (string key in nameValues.Keys)
			{
				this.Add(key, nameValues[key]);
			}
		}
		public void Add(IDictionary<string, string> nameValues)
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string> pair in nameValues)
			{
				this.Add(pair.Key, pair.Value);
			}
		}
		public void Add<TValue>(IDictionary<string, TValue> nameValues) where TValue : IConvertible
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (KeyValuePair<string, TValue> pair in nameValues)
			{
				this.Add<TValue>(pair.Key, pair.Value);
			}
		}
		public void Add(IDictionary<string, object> nameValues)
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> pair in nameValues)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Add<TValue>(string name, TValue value) where TValue : IConvertible
		{
			this.AddBase(name, value);
		}

		public void Add(string name, object value)
		{
			this.AddBase(name, value);
		}

		protected virtual void AddBase(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = Convert.ToString(value);
				value = string.Empty;
			}

			if (base.ContainsKey(name))
			{
				//T--ODO: operator +

			}

			this.Set(name, value);
		}

		public void Set(NameValueCollection nameValues)
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (string key in nameValues.Keys)
			{
				this.Set(key, nameValues[key]);
			}
		}

		public void Set<TValue>(IDictionary<string, TValue> nameValues) where TValue : IConvertible
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (KeyValuePair<string, TValue> pair in nameValues)
			{
				this.Set<TValue>(pair.Key, pair.Value);
			}
		}
		public void Set(IDictionary<string, object> nameValues)
		{
			if (nameValues == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> pair in nameValues)
			{
				this.Set(pair.Key, pair.Value);
			}
		}

		public void Set<TValue>(string name, TValue value) where TValue : IConvertible
		{
			this.SetBase(name, value);
		}

		public void Set(string name, object value)
		{
			this.SetBase(name, value);
		}

		protected virtual void SetBase(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = Convert.ToString(value);
				if (base.ContainsKey(name))
				{
					base.Remove(name);
				}

				base.Add(name, string.Empty);
			}
			else
			{
				if (base.ContainsKey(name))
				{
					base.Remove(name);
				}
				base.Add(name, value.ToString());
			}
		}

		public string Get(string name)
		{
			return Get<string>(name);
		}

		public TValue Get<TValue>(string name) where TValue : IConvertible
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (base.ContainsKey(name))
				{
					return Converter.Convert<TValue>(base[name]);
				}
			}
			return default(TValue);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ToString(false);
		}

		public string ToRawString()
		{
			return GetHttpNameValueCollectionString(false, false);
		}

		public string ToString(bool removeBlankValue)
		{
			return GetHttpNameValueCollectionString(removeBlankValue, true);
		}

		private string GetHttpNameValueCollectionString(bool removeBlankValue, bool encode)
		{
			StringBuilder sb = new StringBuilder();
			string key = string.Empty;
			string value = string.Empty;
			foreach (KeyValuePair<string, string> pair in this)
			{
				key = pair.Key;
				value = Converter.Convert<string>(pair.Value, string.Empty);
				if (removeBlankValue && (string.IsNullOrEmpty(value)))
				{
					continue;
				}

				sb.Append(encode ? Encode(key) : key);
				if (!string.IsNullOrEmpty(value))
				{
					sb.Append("=");
					sb.Append(encode ? Encode(value) : value);
				}
				sb.Append("&");
			}
			return sb.ToString().TrimEnd('&');
		}

		#region Encode and Decode
		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string Encode(object value)
		{
			string val = value.ToString();
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlEncode(val);
		}

		public static string Encode(object value, Encoding encoding)
		{
			string val = value.ToString();
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlEncode(val, encoding);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string Decode(string value)
		{
			string val = value.ToString();
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlDecode(val);
		}

		public static string Decode(string value, Encoding encoding)
		{
			string val = value.ToString();
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlDecode(val, encoding);
		}
		#endregion
	}
}
