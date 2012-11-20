using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Common.Collections
{
    /// <summary>
    /// 表示带有属性<b>Name</b>的对象。 
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// 获取当前实例的名称。
        /// </summary>
        /// <value>当前实例的名称。</value>
        string Name { get; }
    }
}
