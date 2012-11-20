// -----------------------------------------------------------------------
// <copyright file="ArticleService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Common.Collections;
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleService:IArticleService
    {
        IArticleDataAccess _dataAccess=new ArticleDataAccess();

        public int Insert(ArticleInfo model)
        {
            return _dataAccess.Insert(model);
        }

        public int Update(ArticleInfo model)
        {
            return _dataAccess.Update(model);
        }

        public int Delete(int id)
        {
            return _dataAccess.Delete(id);
        }

        /// <summary>
        /// 根据文章分类删除文章
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int DeleteByCategoryId(int categoryId)
        {
            return _dataAccess.DeleteByCategoryId(categoryId);
        }

        public PagedList<ArticleInfo> GetArticles(int pageSize, int pageIndex, string strWhere)
        {
            return _dataAccess.GetArticles(pageSize, pageIndex, strWhere);
        }

        public ArticleInfo GetArticleById(int id)
        {
            return _dataAccess.GetArticleById(id);
        }
    }
}
