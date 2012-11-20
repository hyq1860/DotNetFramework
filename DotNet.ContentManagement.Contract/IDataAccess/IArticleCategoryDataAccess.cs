// -----------------------------------------------------------------------
// <copyright file="IArticleCategoryDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Common.Collections;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IArticleCategoryDataAccess
    {
        int Insert(ArticleCategoryInfo model);

        int Update(ArticleCategoryInfo model);

        ArticleCategoryInfo GetArticleCategoryById(int id);

        int Delete(int id);

        PagedList<ArticleCategoryInfo> GetArticleCategory(int pageSize, int pageIndex, string strWhere);

        List<ArticleCategoryInfo> GetAllArticleCategory();
    }
}
