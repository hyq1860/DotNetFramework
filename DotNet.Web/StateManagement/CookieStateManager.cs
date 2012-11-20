using System;

using DotNet.Web.Configuration;
using DotNet.Web.Configuration.Cookie;

namespace DotNet.Web.StateManagement
{
	/// <summary>
	/// Cookie状态操作管理 
	/// </summary>
	public class CookieStateManager
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cookieName"></param>
		/// <returns></returns>
		public static string GetCookieValue(Enum cookieName)
		{
			string cookieStringName = CookieNameHelper.GetName(cookieName);
			var cookieConfig = GetConfigCookieEntry(cookieStringName);
			if (cookieConfig.HasSubKey)
			{
				return CookieHelper.GetCookie(cookieStringName, CookieNameHelper.GetSubName(cookieName));
			}
			else
			{
				return CookieHelper.GetCookie(cookieStringName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cookieName"></param>
		/// <param name="value"></param>
		public static void SetCookieValue(Enum cookieName, string value)
		{
			ConfigCookieEntry cookieInfo = GetConfigCookieEntry(CookieNameHelper.GetName(cookieName));
			if (cookieInfo.HasSubKey)
			{
				string subName = CookieNameHelper.GetSubName(cookieName);
				CookieHelper.SetCookie(cookieInfo.Name, subName, value, cookieInfo.Domain, cookieInfo.Path, cookieInfo.HttpOnly, cookieInfo.SecureOnly, cookieInfo.ExpiresAfter);
			}
			else
			{
				CookieHelper.SetCookie(cookieInfo.Name, value, cookieInfo.Domain, cookieInfo.Path, cookieInfo.HttpOnly, cookieInfo.SecureOnly, cookieInfo.ExpiresAfter);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cookieName"></param>
		public static void ClearCookie(Enum cookieName)
		{
			ConfigCookieEntry cookieInfo = GetConfigCookieEntry(CookieNameHelper.GetName(cookieName));
			CookieHelper.ClearCookie(CookieNameHelper.GetName(cookieName), CookieNameHelper.GetSubName(cookieName)
				, cookieInfo.Domain, cookieInfo.Path);
		}

		/// <summary>
		/// 清除所有指定的Cookie
		/// </summary>
		/// <param name="cookieName"></param>
		public static void ClearAllCookie(Enum cookieName)
		{
			ConfigCookieEntry cookieInfo = GetConfigCookieEntry(CookieNameHelper.GetName(cookieName));
			CookieHelper.ClearCookie(CookieNameHelper.GetName(cookieName), cookieInfo.Domain, cookieInfo.Path);
		}

		/// <summary>
		/// 清除所有的自定议Cookie
		/// </summary>
		public static void ClearAllCookie()
		{
			foreach (ConfigCookieEntry entry in ConfigHelper.CookieConfig.ConfigCookieEntrys)
			{
				CookieHelper.ClearCookie(entry.Name, entry.Domain, entry.Path);
			}
		}

		private static ConfigCookieEntry GetConfigCookieEntry(string cookieName)
		{
			ConfigCookieEntry entity = ConfigHelper.CookieConfig.GetConfigCookieEntry(cookieName);
			if (entity == null)
			{
				entity = new ConfigCookieEntry();
			}
			return entity;
		}
	}
}
