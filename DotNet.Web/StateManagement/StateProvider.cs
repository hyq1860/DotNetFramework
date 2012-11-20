using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using DotNet.Common.Reflection;

namespace DotNet.Web.StateManagement
{
    public class StateProvider
    {
        private static object _syncObject = new object();

        public static IStateProvider _current = null;

        public static IStateProvider Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_syncObject)
                    {
                        if (_current == null)
                        {
                            ProviderSetting currentProviderSetting = null;
                            ProviderConfigurationSection section = (ProviderConfigurationSection)System.Configuration.ConfigurationManager.GetSection("StateProvider");
                            ProviderCollection providers = section.Providers;

                            foreach (ProviderSetting ps in providers)
                            {
                                if (ps.Name == section.DefaultProvider)
                                {
                                    currentProviderSetting = ps;
                                    break;
                                }
                            }

                            if (currentProviderSetting != null)
                            {
                                Type t = Type.GetType(currentProviderSetting.Type);
                                _current = MethodCaller.CreateInstance(t) as IStateProvider;                                
                            }
                        }
                    }
                }

                if (_current == null)
                    throw new InvalidOperationException("No availabe state provider to access.");

                return _current;
            }
        }

        public static void Add(Enum name, object value)
        {
            Current.Add(name, value);
        }

        public static void Clear()
        {
            Current.Clear();
        }

        public void Remove(Enum name)
        {
            Current.Remove(name);
        }

        public static void RemoveAll(Enum name)
        {
            Current.RemoveAll(name);
        }

        public object this[Enum name]
        {
            get { return Current[name]; }
            set { Current[name] = value; }
        }
    }
}
