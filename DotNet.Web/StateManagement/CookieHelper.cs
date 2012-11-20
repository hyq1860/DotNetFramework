using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using DotNet.Web.Configuration;
using DotNet.Common.Utility;

namespace DotNet.Web.StateManagement
{
    /// <summary>
    /// ASP.NET Cookie 概述
    /// http://msdn.microsoft.com/zh-cn/library/ms178194.aspx
    /// </summary>
    public class CookieHelper
    {
        #region Get Cookie

        /// <summary>
        /// 读取单值Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            HttpCookie cookie;
            cookieName = Encode(cookieName);
            cookie = HttpContext.Current.Request.Cookies[cookieName];

            return GetCookie(cookie);
        }

        /// <summary>
        /// 读取多值Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="valueKey"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string subKey)
        {
            HttpCookie cookie;
            cookieName = Encode(cookieName);
            subKey = Encode(subKey);
            cookie = HttpContext.Current.Request.Cookies[cookieName];

            return GetCookie(cookie, subKey);
        }

        private static string GetCookie(HttpCookie cookie)
        {
            if (cookie == null)
            {
                return null;
            }

            return Decode(cookie.Value);
        }

        private static string GetCookie(HttpCookie cookie, string subKey)
        {
            if (cookie == null)
            {
                return null;
            }

            return Decode(cookie[subKey]);
        }

        #endregion

        #region Set Cookie

        public static void SetCookie(string cookieName, string value)
        {
            SetCookie(cookieName, value, TimeSpan.FromTicks(0L));
        }

        public static void SetCookie(string cookieName, string value, TimeSpan expireDate)
        {
            SetCookie(cookieName, value, "", "/", false, false, expireDate);
        }
        
        public static void SetCookie(string cookieName, string subKey, string value)
        {
            SetCookie(cookieName, subKey, value, TimeSpan.FromTicks(0L));
        }

        public static void SetCookie(string cookieName, string subKey, string value, TimeSpan expireDate)
        {
            SetCookie(cookieName, subKey, value, "", "/", false, false, expireDate);
        }

        public static void SetCookie( string cookieName , string value , string domain , string path , bool httpOnly 
            , bool requireSSL , TimeSpan expireDate )
        {
            cookieName = Encode( cookieName );
            value = Encode( value );

            HttpCookie cookie = new HttpCookie( cookieName );
            cookie.Value = value;
            cookie.Domain = domain;
            cookie.Path = path;
            cookie.HttpOnly = httpOnly;
            cookie.Secure = requireSSL;
            if ( expireDate.Ticks != 0L )
            {
                cookie.Expires = DateTime.Now.Add( expireDate );
            }

            HttpContext.Current.Response.Cookies.Add( cookie );
        }

        public static void SetCookie( string cookieName , string subKey , string value , string domain , string path
            , bool httpOnly , bool requireSSL , TimeSpan expireDate )
        {

            cookieName = Encode( cookieName );
            value = Encode( value );
            HttpCookie cookie;

            if ( HttpContext.Current.Request.Cookies[cookieName] == null )
            {
                cookie = new HttpCookie( cookieName );
            }
            else
            {
                cookie = HttpContext.Current.Request.Cookies[cookieName];
            }

            cookie.Domain = domain;
            cookie.Path = path;
            cookie.HttpOnly = httpOnly;
            cookie.Secure = requireSSL;
            cookie[subKey] = value;
            if ( expireDate.Ticks != 0L )
            {
                cookie.Expires = DateTime.Now.Add( expireDate );
            }

            HttpContext.Current.Response.Cookies.Add( cookie );
        }

        #endregion

        #region Clear Cookie

        /// <summary>
        /// 清除Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void ClearCookie(string cookieName, string domain, string path)
        {
            cookieName = Encode(cookieName);

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Domain = domain;
            cookie.Path = path;
            cookie.Expires = DateTime.Now.AddDays(-360);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 从多值Cookie移除个subKey
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="subKey"></param>
        public static void ClearCookie(string cookieName, string subKey, string domain, string path)
        {
            cookieName = Encode(cookieName);
            subKey = Encode(subKey);

            HttpCookie cookie;

            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                cookie = new HttpCookie(cookieName);
            }
            else
            {
                cookie = HttpContext.Current.Request.Cookies[cookieName];
            }
            cookie.Domain = domain;
            cookie.Path = path;

            cookie.Values.Remove(subKey);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion   

        private static string Encode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
           // return AntiXssEncoderTwo.UrlEncode(input);
            return HttpUtility.UrlEncode(input);//.Replace("_", "%5F").Replace("&", "%26").Replace("+", "%2B");
        }

        private static string Decode( string input )
        {
            return HttpUtility.UrlDecode( input );
        }
    }
}
