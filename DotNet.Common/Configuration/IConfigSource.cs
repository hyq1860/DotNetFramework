using System.Collections.Generic;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 配置文件接口
    /// </summary>
    public interface IConfigSource
    {
        /// <summary>
        /// 开始监控。
        /// </summary>
        /// <param name="getParameter"></param>
        void StartWatching(IConfigParameter getParameter);

        /// <summary>
        /// 停止监控。
        /// </summary>
        /// <param name="getParameter"></param>
        void StopWatching(IConfigParameter getParameter);

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <returns></returns>
        T GetConfig<T>() where T : class, new();        

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        T GetConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange) where T : class, new();

        /// <summary>
        /// 获取配置列表。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <returns></returns>
        List<T> GetConfigList<T>() where T : class, new();

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        List<T> GetConfigList<T>(IConfigParameter getParameter, bool restartAppDomainOnChange) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetMergedConfig<T>() where T : class, IMergableConfig, new();

        /// <summary>
        /// 获取配置。
        /// </summary>
        /// <typeparam name="T">配置类型。</typeparam>
        /// <param name="getParameter">获取配置参数。</param>
        /// <param name="restartAppDomainOnChange">当配置改变时重新启动应用程序域。</param>
        /// <returns>配置。</returns>
        T GetMergedConfig<T>(IConfigParameter getParameter, bool restartAppDomainOnChange)
            where T : class, IMergableConfig, new();
    }
}