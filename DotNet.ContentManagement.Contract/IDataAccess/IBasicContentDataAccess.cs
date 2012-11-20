// -----------------------------------------------------------------------
// <copyright file="IBasicContentDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IBasicContentDataAccess
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        int Insert(BasicContentInfo model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(BasicContentInfo model);

        /// <summary>
        /// 根据id获取内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BasicContentInfo GetBasicContentById(int id);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        List<BasicContentInfo> GetBasicContents();
    }
}
