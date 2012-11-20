// -----------------------------------------------------------------------
// <copyright file="IArticleCategoryService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IArticleCategoryService
    {
        ArticleCategoryInfo GetArticleCategoryById(int id);

        int Insert(ArticleCategoryInfo model);

        int Update(ArticleCategoryInfo model);

        int Delete(int id);

        List<ArticleCategoryInfo> GetAllArticleCategory();
    }
}
