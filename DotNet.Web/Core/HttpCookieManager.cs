using System;
using System.Web;

using DotNet.Common.Core;
using DotNet.Common.Utility;

namespace DotNet.Web.Core
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// </summary>
    public class HttpCookieManager : HttpCookieManagerBase
    {
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed",
            Justification = "Reviewed. Suppression is OK here.")]
        private HttpCookieSettingCollection _cookieSettings =
            SingletonProvider<HttpCookieSettingCollection>.UniqueInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCookieManager"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public HttpCookieManager(HttpContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get cookie string value
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public string Get(Enum alias)
        {
            return Get<string>(alias).ToString();
        }

        /// <summary>
        /// Get HttpCookieValue
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public HttpCookieValue<T> Get<T>(Enum alias) where T : IConvertible
        {
            HttpCookieValue<T> cookieValue = new HttpCookieValue<T>();
            if (_cookieSettings.ContainsKey(alias))
            {
                HttpCookieSetting setting = _cookieSettings[alias];

                HttpCookie cookie = Get(setting.Name);
                if (cookie != null)
                {
                    if (cookie.HasKeys)
                    {
                        cookieValue.ValueCollection.Deserialize(cookie.Value);
                    }
                    else
                    {
                        cookieValue.Value = Converter.Convert<T>(Decode(cookie.Value));
                    }
                }

                return cookieValue;
            }

            return cookieValue;
        }

        /// <summary>
        /// Get sub cookie string value
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Get(Enum alias, string name)
        {
            return Get<string>(alias, name);
        }

        /// <summary>
        /// Get sub cookie value
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name">Sub Cookie Name</param>
        /// <returns></returns>
        public T Get<T>(Enum alias, string name) where T : IConvertible
        {
            HttpCookieValue<T> cookieValue = Get<T>(alias);
            if (cookieValue != null)
            {
                if (cookieValue.ValueCollection.ContainsKey(name))
                {
                    return cookieValue.ValueCollection.Get<T>(name);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="value"></param>
        public void Set(Enum alias, string value)
        {
            Set(alias, value, TimeSpan.MinValue);
        }

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public void Set(Enum alias, string value, TimeSpan expires)
        {
            HttpCookieValue cookieValue = new HttpCookieValue();
            cookieValue.Value = value;
            Set(alias, cookieValue, expires);
        }

        /// <summary>
        /// Set sub cookie
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name">Sub Cookie Name</param>
        /// <param name="value"></param>
        public void Set(Enum alias, string name, string value)
        {
            Set(alias, name, value, TimeSpan.MinValue);
        }

        /// <summary>
        /// Set sub cookie
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public void Set(Enum alias, string name, string value, TimeSpan expires)
        {
            HttpCookieValue cookieValue = new HttpCookieValue();
            cookieValue.ValueCollection.Add(name, value);
            Set(alias, cookieValue, expires);
        }

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expires">MinValue means use default expires date from configuration</param>
        public void Set(Enum alias, HttpCookieValue cookieValue, TimeSpan expires)
        {
            if (_cookieSettings.ContainsKey(alias))
            {
                HttpCookieSetting setting = _cookieSettings[alias];
                expires = expires == TimeSpan.MinValue
                              ? (setting.AutoExpires ? TimeSpan.Zero : setting.Expires)
                              : expires;
                if (cookieValue.HasKeys)
                {
                    Set(
                        setting.Name,
                        cookieValue.ValueCollection,
                        setting.Domain,
                        setting.Path,
                        expires,
                        setting.Secure,
                        setting.HttpOnly);
                }
                else
                {
                    Set(
                        setting.Name,
                        cookieValue.ToString(),
                        setting.Domain,
                        setting.Path,
                        expires,
                        setting.Secure,
                        setting.HttpOnly);
                }
            }
        }

        /// <summary>
        /// Clear cookie
        /// </summary>
        /// <param name="alias"></param>
        public void Clear(Enum alias)
        {
            if (_cookieSettings.ContainsKey(alias))
            {
                HttpCookieSetting setting = _cookieSettings[alias];
                Clear(setting.Name, setting.Domain);
                Clear(setting.Name, string.Empty);
            }
        }

        public void ClearFix(Enum alias)
        {
            if (_cookieSettings.ContainsKey(alias))
            {
                HttpCookieSetting setting = _cookieSettings[alias];
                Clear(setting.Name, string.Empty);
            }
        }
    }
}