using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Web.StateManagement
{
    /// <summary>
    /// 状态管理接口
    /// </summary>
    public interface IStateProvider
    {
        object this[Enum name] { get; set; }   

        // 添加一个状态
        void Add(Enum name, object value);
        // 移除一个SubKey
        void Remove(Enum name);
        // 移除 Key
        void RemoveAll(Enum name);
        // 清除
        void Clear();
        //获取状态值，返回值为string
        string GetStringValue(Enum name);
        //获取状态值，返回值为string，返回值不可为null
        string GetNotNullStringValue(Enum name);
        //获取状态值，返回值为int
        int GetIntValue(Enum name);
        //获取状态值，返回值为int?
        int? GetNullableIntValue(Enum name);
    }
}