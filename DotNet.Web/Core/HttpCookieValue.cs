using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Web.Core
{
	public class HttpCookieValue<T> where T : IConvertible
	{
		private T _value;
		private HttpNameValueCollection _valueCollection = new HttpNameValueCollection();

		/// <summary>
		/// Cookie Value
		/// </summary>
		public T Value
		{
			get
			{
				return _value;
			}
			set
			{
				_valueCollection.Clear();
				_value = value;
			}
		}

		/// <summary>
		/// Cookie Value Collection (name=value) pairs
		/// </summary>
		public HttpNameValueCollection ValueCollection
		{
			get
			{
				return _valueCollection;
			}
		}

		/// <summary>
		/// Has (name=value) cookies
		/// </summary>
		public bool HasKeys
		{
			get
			{
				return _valueCollection.Count > 0;
			}
		}

		public override string ToString()
		{
			if (HasKeys)
			{
				return _valueCollection.ToRawString();
			}
			if (_value != null)
			{
				return _value.ToString();
			}
			return string.Empty;
		}
	}

	public class HttpCookieValue : HttpCookieValue<string>
	{ }
}
