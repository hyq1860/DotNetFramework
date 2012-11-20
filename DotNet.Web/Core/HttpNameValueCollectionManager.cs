using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DotNet.Common.Core;
using DotNet.Common.Utility;

namespace DotNet.Web.Core
{
	public class HttpNameValueCollectionManager
	{
		private HttpNameValueCollection _nameValueCollection = new HttpNameValueCollection();

		public HttpNameValueCollectionManager(NameValueCollection nameValueCollection)
		{
			if (nameValueCollection != null)
			{
				_nameValueCollection = new HttpNameValueCollection(nameValueCollection);
			}
		}

		/// <summary>
		/// Get request input value <T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public string Get(string name)
		{
			return GetRaw(name);
		}

		/// <summary>
		/// Get request input value <T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public T Get<T>(string name) where T : IConvertible
		{
			return Converter.Convert<T>(GetRaw(name));
		}

		public IList<string> GetList(string name)
		{
			string[] arr = GetRaw(name).Split(',');
			IList<string> list = new List<string>();
			foreach (string o in arr)
			{
				if (!string.IsNullOrEmpty(o))
				{
					list.Add(o.Trim());
				}
			}
			return list;
		}

		/// <summary>
		/// Get request input value by List<T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public IList<T> GetList<T>(string name) where T : IConvertible
		{
			string[] arr = GetRaw(name).Split(',');
			IList<T> list = new List<T>();
			bool isDefaultIsNull = default(T) == null;
			foreach (string o in arr)
			{
				if (!string.IsNullOrEmpty(o))
				{
					T v = Converter.Convert<T>(o.Trim());

					if (isDefaultIsNull)
					{
						if (v != null && string.IsNullOrEmpty(System.Convert.ToString(v)))
						{
							list.Add(v);
						}
					}
					else
					{
						if (!default(T).Equals(v))
						{
							list.Add(v);
						}
					}
				}
			}
			return list;
		}

		/// <summary>
		/// Get request input value RAW string 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetRaw(string name)
		{
			string raw = string.Empty;
			string val = _nameValueCollection.Get(name);

			if (!string.IsNullOrEmpty(val))
			{
				raw = val.Trim();
			}

			return raw;
		}

		public HttpNameValueCollection All
		{
			get
			{
				return _nameValueCollection;
			}
		}
	}
}
