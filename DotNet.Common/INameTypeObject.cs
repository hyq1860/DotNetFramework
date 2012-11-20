using System;

namespace DotNet.Common
{
    /// <summary>
    /// 表示带有属性<b>Name</b>和<b>Type</b>的对象。 
    /// </summary>
    public interface INameTypeObject : INamedObject
    {
        /// <summary>
        /// 获取当前实例数据的类型。
        /// </summary>
        /// <value>当前实例数据的类型。</value>
        Type Type { get; }
    }
}