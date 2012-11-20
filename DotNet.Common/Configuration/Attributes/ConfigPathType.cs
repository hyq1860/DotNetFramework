namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    public enum ConfigPathType
    {
        /// <summary>
        /// 
        /// </summary>
        Default,

        /// <summary>
        /// 服务器端路径： Path.Combine(AppDomain.CurrentDomain.BaseDirectory, attribute.FileName.Replace("~/", ""))
        /// 或者HttpContext.Current.Server.MapPath(attribute.FileName)
        /// </summary>
        ServerPath,

        /// <summary>
        /// 绝对物理路径
        /// </summary>
        FullPhysicalPath
    }
}
