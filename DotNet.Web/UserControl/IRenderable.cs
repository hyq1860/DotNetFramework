using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Web
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRenderable<T>
    {
        /// <summary>
        /// 用户控件绑定数据
        /// </summary>
        /// <param name="data"></param>
        void PopulateData(T data);
    }
}
