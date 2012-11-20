using System;

namespace DotNet.Web.StateManagement
{
    /// <summary>
    /// Cookie Name Format: 系统全部采用二维Cookie
    /// Enum Name = CookieName
    /// Enum FieldName = CookieSubName
    /// </summary>
    public class CookieNameHelper
    {
        public static string GetName(Enum cookieName)
        {
            string typeName = cookieName.GetType().Name;
            return typeName.Substring(0, typeName.Length - 10);
        }

        public static string GetSubName(Enum cookieName)
        {
            return cookieName.ToString();
        }
    }
}
