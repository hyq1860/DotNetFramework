namespace DotNet.Common.Configuration.DotNetConfig
{
    /// <summary>
    /// 表示将处理 <typeparamref name="DotNetConfigSourceWatcher"/> 类的 Changed、Created 或 Deleted 事件的方法。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">包含事件数据的 <typeparamref name="DotNetConfigChangedEventArgs"/>。</param>
    /// <remarks>
    /// 当创建 <typeparamref name="XmlConfigChangedEventHandler"/> 委托时，将标识处理事件的方法。
    /// 若要使该事件与事件处理程序相关联，请将该委托的一个实例添加到事件中。
    /// 除非移除了该委托，否则每当发生该事件时就调用事件处理程序。
    /// </remarks>
    public delegate void DotNetConfigChangedEventHandler(object sender, DotNetConfigChangedEventArgs e);

    /// <summary>
    /// 提供目录事件的数据。
    /// </summary>
    /// <remarks>
    /// <para><b>FileSystemEventArgs</b> 类作为参数传递给这些事件的事件处理程序： </para>
    /// <para>当对 <typeparamref name="DotNetConfigSourceWatcher"/> 的指定 Path 中的文件或目录的大小、系统属性、最近写入时间、最近访问时间或安全权限进行更改时，会发生 Changed 事件。</para>
    /// <para>当在 <typeparamref name="DotNetConfigSourceWatcher"/> 的指定 Path 中创建文件或目录时，会发生 Created 事件。</para>
    /// <para>当删除 <typeparamref name="DotNetConfigSourceWatcher"/> 的指定 Path 中的某个文件或目录时，会发生 Deleted 事件。有关更多信息，请参见 <typeparamref name="XmlConfigSourceWatcher"/>。</para> 
    /// </remarks>
    public class DotNetConfigChangedEventArgs : ConfigChangedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getParameter"></param>
        public DotNetConfigChangedEventArgs(DotNetConfigGetParameter getParameter)
            : base(getParameter)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getParameter"></param>
        /// <param name="restartAppDomainOnChange"></param>
        public DotNetConfigChangedEventArgs(DotNetConfigGetParameter getParameter, bool restartAppDomainOnChange)
            : base(getParameter, restartAppDomainOnChange)
        {
        }
    }
}