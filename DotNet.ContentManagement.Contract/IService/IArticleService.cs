// -----------------------------------------------------------------------
// <copyright file="IArticleService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.IService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Common.Collections;
    using DotNet.ContentManagement.Contract;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IArticleService
    {
        int Insert(ArticleInfo model);

        int Update(ArticleInfo model);

        int Delete(int id);

        /// <summary>
        /// 根据文章分类删除文章
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        int DeleteByCategoryId(int categoryId);

        PagedList<ArticleInfo> GetArticles(int pageSize, int pageIndex, string strWhere);

        ArticleInfo GetArticleById(int id);
    }
}
