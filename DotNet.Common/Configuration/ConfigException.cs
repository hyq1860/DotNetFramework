using System;
using System.Runtime.Serialization;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 配置异常。
    /// </summary>
    public class ConfigException : Exception
    {
        /// <summary>
        /// 初始化 <see cref="ConfigException"/> 类的新实例。 
        /// </summary>
        public ConfigException()
            : base()
        {
        }      
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConfigException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConfigException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 用序列化数据初始化 <see cref="ConfigException"/> 类的新实例。 
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">对象，描述序列化数据的源或目标。</param>
        public ConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 设置带有键值和附加异常信息的 <see cref="SerializationInfo"/> 对象。 
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}