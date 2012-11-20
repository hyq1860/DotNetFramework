/********************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 12/23/2008 
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/

namespace DotNet.Common
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