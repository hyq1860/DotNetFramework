namespace DotNet.Common.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 配置文件参数接口
    /// </summary>
    public interface IConfigParameter
    {
        /// <summary>
        /// 用于缓存Key。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 用于监控配置改变。
        /// </summary>
        string GroupName { get; }
    }
}