// -----------------------------------------------------------------------
// <copyright file="ArticleCategoryService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleCategoryService : IArticleCategoryService
    {
        IArticleCategoryDataAccess _dataAccess=new ArticleCategoryDataAccess();

        public ArticleCategoryInfo GetArticleCategoryById(int id)
        {
            return _dataAccess.GetArticleCategoryById(id);
        }

        public int Insert(ArticleCategoryInfo model)
        {
            return _dataAccess.Insert(model);
        }

        public int Update(ArticleCategoryInfo model)
        {
            return _dataAccess.Update(model);
        }

        public int Delete(int id)
        {
            return _dataAccess.Delete(id);
        }

        public List<ArticleCategoryInfo> GetAllArticleCategory()
        {
            return _dataAccess.GetAllArticleCategory();
        }
    }
}
