using System;
using System.Collections.Generic;
using System.ComponentModel;

using DotNet.Common.Diagnostics;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 配置提供基类。
    /// </summary>
    public abstract class AbstractConfigSource : IConfigSource, IDisposable
    {
        private static object synObj = new object();
        private static Dictionary<string, IConfigChangeWatcher> watcherMappings = new Dictionary<string, IConfigChangeWatcher>();
        private static Dictionary<string, object> cachedConfigs = new Dictionary<string, object>();
        private readonly object eventHandlersLock = new object();
        private EventHandlerList eventHandles = new EventHandlerList();
        private object syncLock = new object();

        /// <summary>
        /// 开始监控。
        /// </summary>
        public void StartWatching(IConfigParameter getParameter)
        {
            IConfigChangeWatcher configWatcher = GetConfigWatcher(getParameter);
            if (configWatcher != null)
            {
                configWatcher.StartWatching();
            }
        }

        /// <summary>
        /// 停止监控。
        /// </summary>
        public void StopWatching(IConfigParameter getParameter)
        {
            IConfigChangeWatcher configWatcher = GetConfigWatcher(getParameter);
            if (configWatcher != null)
            {
                configWatcher.StopWatching();
            }
        }

        /// <summary>
        /// 安装监控。
        /// </summary>
        /// <param name="watcher">监控。</param>
        protected void SetupWacher(IConfigChangeWatcher watcher)
        {
            string configWatchKey = watcher.GetParameter.GroupName;
            if (!watcherMappings.ContainsKey(configWatchKey))
            {
                lock (synObj)
                {
                    if (!watcherMappings.ContainsKey(configWatchKey))
                    {
                        watcherMappings.Add(configWatchKey, watcher);
                        watcher.Changed += new ConfigChangedEventHandler(OnConfigChanged);
                        watcher.StartWatching();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getParameter"></param>
        /// <param name="handler"></param>
        public void AddChangeHandler(IConfigParameter getParameter, ConfigChangedEventHandler handler)
        {
            ConfigChangedEventHandler callback = eventHandles[getParameter.GroupName] as ConfigChangedEventHandler;
            if (callback == null)
            {
                lock (eventHandlersLock)
                {
                    callback = eventHandles[getParameter.GroupName] as ConfigChangedEventHandler;
                    if (callback == null)
                    {
                        eventHandles.AddHandler(getParameter.GroupName, handler);
                    }
                }
            }
        }

        /// <summary>
        /// 处理配置文件更改。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">包含事件数据的 <see cref="ConfigChangedEventArgs"/>。</param>
        private void OnConfigChanged(object sender, ConfigChangedEventArgs e)
        {
            bool appDomainRestarted = false;
            if (e.RestartAppDomainOnChange)
            {
                appDomainRestarted = FrameworkConfig.RestartAppDomain();
            }
            
            if (!appDomainRestarted)
            {
                RemoveConfigFromCache(e.GetParameter);
                NotifyConfigChanged(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void NotifyConfigChanged(ConfigChangedEventArgs e)
        {
            Delegate[] invocationList = null;

            lock (eventHandlersLock)
            {
                ConfigChangedEventHandler handler = eventHandles[e.GetParameter.GroupName] as ConfigChangedEventHandler;
                if (handler != null)
                {
                    invocationList = handler.GetInvocationList();
                }
            }

            try
            {
                if (invocationList != null)
                {
                    foreach (ConfigChangedEventHandler callback in invocationList)
                    {
                        if (callback != null)
                        {
                            callback(this, e);
                        }
                    }
                }
            }
            catch // (Exception e)
            {
                //EventLog.WriteEntry(GetEventSourceName(), Resources.ExceptionEventRaisingFailed + GetType().FullName + " :" + e.Message);
            }            
        }

        /// <summary>
        /// 获取某入口参数对应配置的监控。
        /// </summary>
        protected IConfigChangeWatcher GetConfigWatcher(IConfigParameter getParameter)
        {
            Check.Argument.IsNotNull("getParameter", getParameter);

            IConfigChangeWatcher configWatcher = null;
            watcherMappings.TryGetValue(getParameter.GroupName, out configWatcher);
            return configWatcher; 
        }

        /// <summary>
        /// 释放wather资源
        /// </summary>
        void IDisposable.Dispose()
        {
            foreach (KeyValuePair<string, IConfigChangeWatcher> watcher in watcherMappings)
            {
                watcher.Value.Dispose();
            }
        }

        /// <summary>
        /// 根据配置类上标识的属性信息获取对应的配置信息。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetConfig<T>() where T : class, new()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getParameter"></param>
        /// <param name="restartAppDomainOnChange"></param>
        /// <returns></returns>
        public virtual T GetConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange) where T : class, new()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取配置列表。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <returns>配置</returns>
        public virtual List<T> GetConfigList<T>() where T : class, new()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        public virtual List<T> GetConfigList<T>(IConfigParameter getParameter, bool restartAppDomainOnChange) where T : class, new()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetMergedConfig<T>() where T : class, IMergableConfig, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        public virtual T GetMergedConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange)
            where T : class, IMergableConfig, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从缓存中获取配置信息。
        /// </summary>
        /// <param name="getParameter"></param>
        /// <returns></returns>
        protected T GetConfigFromCache<T>(IConfigParameter getParameter) where T : class, new()
        {
            object cachedObj = null;
            if (cachedConfigs.ContainsKey(getParameter.Name))
            {
                cachedObj = cachedConfigs[getParameter.Name];
            }

            return cachedObj as T;
        }

        /// <summary>
        /// 从缓存中获取配置信息列表。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getParameter"></param>
        /// <returns></returns>
        protected List<T> GetConfigListFromCache<T>(IConfigParameter getParameter) where T : class, new()
        {
            object cachedObj = null;
            if (cachedConfigs.ContainsKey(getParameter.Name))
            {
                cachedObj = cachedConfigs[getParameter.Name];
            }

            return cachedObj as List<T>;
        }

        /// <summary>
        /// 添加配置到缓存中。
        /// </summary>
        /// <param name="getParameter">获取配置入口参数。</param>
        /// <param name="config">要缓存的配置。</param>
        protected void AddConfigToCache(IConfigParameter getParameter, object config)
        {
            string cachedKey = getParameter.Name;
            object cachedObj = null;
            if (!cachedConfigs.TryGetValue(cachedKey, out cachedObj))
            {
                lock (synObj)
                {
                    if (!cachedConfigs.TryGetValue(cachedKey, out cachedObj))
                    {
                        cachedConfigs.Add(cachedKey, config);
                    }
                }
            }
        }

        /// <summary>
        /// 从缓存中移除配置。
        /// </summary>
        /// <param name="getParameter">获取配置入口参数。</param>
        protected void RemoveConfigFromCache(IConfigParameter getParameter)
        {
            if (cachedConfigs.ContainsKey(getParameter.Name))
            {
                lock (synObj)
                {
                    if (cachedConfigs.ContainsKey(getParameter.Name))
                    {
                        cachedConfigs.Remove(getParameter.Name);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected object SyncLock
        {
            get { return this.syncLock; }
        }
    }
}
