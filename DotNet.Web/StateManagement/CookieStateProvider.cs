using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Web.StateManagement
{
    public class CookieStateProvider : IStateProvider
    {
        public object this[Enum name]
        {
            get
            {
                return CookieStateManager.GetCookieValue(name);
            }
            set
            {
                CookieStateManager.SetCookieValue(name, Convert.ToString(value));
            }
        }

        public void Add(Enum name, object value)
        {
            CookieStateManager.SetCookieValue(name, Convert.ToString(value));
        }

        public void Clear()
        {
            CookieStateManager.ClearAllCookie();
        }

        public void Remove(Enum name)
        {
            CookieStateManager.ClearCookie(name);
        }

        public void RemoveAll(Enum name)
        {
            CookieStateManager.ClearAllCookie(name);
        }

        public string GetStringValue(Enum name)
        {
            return CookieStateManager.GetCookieValue(name);
        }

        public string GetNotNullStringValue(Enum name)
        {
            string tempStr = CookieStateManager.GetCookieValue(name);
            if (tempStr == null) tempStr = string.Empty;

            return tempStr;
        }

        public int GetIntValue(Enum name)
        {
            string strValue = GetStringValue(name);

            if (string.IsNullOrEmpty(strValue)) return 0;

            int tempInt = 0;
            return (int.TryParse(strValue, out tempInt)) ? tempInt : 0;
        }

        public int? GetNullableIntValue(Enum name)
        {
            string strValue = GetStringValue(name);

            if (string.IsNullOrEmpty(strValue)) return null;

            int tempInt = 0;
            if (int.TryParse(strValue, out tempInt))
            {
                return tempInt;
            }
            else
            {
                return null;
            }
        }
    }
}
