using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DotNet.Common.Utility;
using System.Text.RegularExpressions;

namespace DotNet.Web.Core
{
	public class HttpCookieManagerBase
	{
		private static readonly DateTime _expirationDate = DateTime.Now;
		private Dictionary<string, HttpCookie> _requestCookies = new Dictionary<string, HttpCookie>(new CaseInsensitiveStringEqualityComparer());
		private Dictionary<string, HttpCookie> _responseCookies = new Dictionary<string, HttpCookie>(new CaseInsensitiveStringEqualityComparer());
		private Dictionary<string, HttpCookie> _requestXCookies = new Dictionary<string, HttpCookie>(new CaseInsensitiveStringEqualityComparer());
		private HttpContext _context = null;

		#region constructor
		public HttpCookieManagerBase(HttpContext context)
		{
			_context = context;

			if (_context != null)
			{
				string[] names = _context.Request.Cookies.AllKeys;
				foreach (string name in names)
				{
					if (_requestCookies.ContainsKey(name))
					{
						_requestCookies[name] = _context.Request.Cookies.Get(name);
					}
					else
					{
						_requestCookies.Add(name, _context.Request.Cookies.Get(name));
					}
				}

				names = _context.Response.Cookies.AllKeys;
				foreach (string name in names)
				{
					if (_responseCookies.ContainsKey(name))
					{
						_responseCookies[name] = _context.Response.Cookies.Get(name);
					}
					else
					{
						_responseCookies.Add(name, _context.Response.Cookies.Get(name));
					}
				}

				#region Disable X-Cookie
				/*
				string xCookies = Converter.Convert<string>(_context.Request.Headers["X-Cookie"]);
				if (!string.IsNullOrEmpty(xCookies))
				{
					string[] cookies = xCookies.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string cookie in cookies)
					{
						if (HttpNameValueCollection.HttpNameValueFormat.IsMatch(cookie))
						{
							Match m = HttpNameValueCollection.HttpNameValueFormat.Regex.Match(cookie);
							string name = m.Result("$1");
							string value = m.Result("$2");

							if (_requestXCookies.ContainsKey(name))
							{
								_requestXCookies[name] = new HttpCookie(name, value);
							}
							else
							{
								_requestXCookies.Add(name, new HttpCookie(name, value));
							}
						}
					}
				}*/
				#endregion
			}
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public HttpCookie Get(string name)
		{
			name = Encode(name);
			if (_responseCookies.ContainsKey(name))
			{
				return _responseCookies[name];
			}

			if (_requestXCookies.ContainsKey(name))
			{
				return _requestXCookies[name];
			}

			if (_requestCookies.ContainsKey(name))
			{
				return _requestCookies[name];
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="values"></param>
		/// <param name="domain"></param>
		/// <param name="path"></param>
		/// <param name="expires"></param>
		/// <param name="secure"></param>
		public void Set(string name, HttpNameValueCollection values, string domain, string path, TimeSpan expires, bool secure, bool httpOnly)
		{
			name = Encode(name);

			//check if already exsits in response cookie?
			if (!_responseCookies.ContainsKey(name))
			{
				if (_requestCookies.ContainsKey(name))
				{
					if (_requestCookies[name].HasKeys)
					{
						foreach (string key in _requestCookies[name].Values.Keys)
						{
							if (!values.ContainsKey(key))
							{
								values.Add(key, _requestCookies[name].Values[key]);
							}
						}
					}
				}
			}

			Save(name, values.ToString(), domain, path, expires, secure, httpOnly);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="domain"></param>
		/// <param name="path"></param>
		/// <param name="expires"></param>
		/// <param name="secure"></param>
		public void Set(string name, string value, string domain, string path, TimeSpan expires, bool secure, bool httpOnly)
		{
			Save(Encode(name), Encode(value), domain, path, expires, secure, httpOnly);
		}

		private void Save(string name, string value, string domain, string path, TimeSpan expires, bool secure, bool httpOnly)
		{
			if (!_responseCookies.ContainsKey(name))
			{
				_responseCookies.Add(name, new HttpCookie(name));
			}
			HttpCookie cookie = _responseCookies[name];

			cookie.Value = value;
			if (!string.IsNullOrEmpty(domain))
			{
				cookie.Domain = domain;
			}
			cookie.Path = path.TrimEnd('/') + "/";
			if (expires.Ticks != 0)
			{
				cookie.Expires = DateTime.Now.Add(expires);
			}
			cookie.Secure = secure;
			cookie.HttpOnly = httpOnly;

			ResponseCookie(cookie);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="domain"></param>
		public void Clear(string name, string domain)
		{
			name = Encode(name);

			//clear cookie, only if exists in request cookies
			if (_requestCookies.ContainsKey(name))
			{
				if (!_responseCookies.ContainsKey(name))
				{
					_responseCookies.Add(name, new HttpCookie(name));
				}
				HttpCookie cookie = _responseCookies[name];

				cookie.Expires = _expirationDate;
				cookie.Value = string.Empty;

				if (!string.IsNullOrEmpty(domain))
				{
					cookie.Domain = domain;
				}

				ResponseCookie(cookie);
			}
		}

		private void ResponseCookie(HttpCookie cookie)
		{
			_context.Response.Cookies.Set(cookie);
		}

		#region Encode and Decode
		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string Encode(string val)
		{
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlEncode(val);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string Decode(string val)
		{
			return string.IsNullOrEmpty(val) ? val : HttpUtility.UrlDecode(val);
		}
		#endregion
	}
}
