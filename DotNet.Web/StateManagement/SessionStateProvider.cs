using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace DotNet.Web.StateManagement
{
    public class SessionStateProvider : IStateProvider
    {
        private HttpSessionState _session;

        public SessionStateProvider()
        {
            _session = HttpContext.Current.Session;
        }

        public void Add(Enum name, object value)
        {
            if (_session != null)
            {
                _session.Add(name.ToString(), value);
            }
        }

        public void Clear()
        {
            if (_session != null)
            {
                _session.Clear();
            }
        }

        public void Remove(Enum name)
        {
            if (_session != null)
            {
                _session.Remove(name.ToString());
            }
        }

        public void RemoveAll(Enum name)
        {
            if (_session != null)
            {
                _session.Remove(name.ToString());
            }
        }

        public object this[Enum name]
        {
            get
            {
                object obj = null;
                try
                {
                    obj = _session[name.ToString()];
                }
                catch { }

                return obj;
            }
            set
            {
                _session[name.ToString()] = value;
            }
        }

        public string GetStringValue(Enum name)
        { 
            string sessionName = name.ToString();
            if (_session[sessionName] != null)
            {
                return _session[sessionName].ToString();
            }
            else
            {
                return null;
            }
        }

        public string GetNotNullStringValue(Enum name)
        {
            string sessionName = name.ToString();
            if (_session[sessionName] != null)
            {
                return _session[sessionName].ToString();
            }
            else
            {
                return string.Empty;
            }
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