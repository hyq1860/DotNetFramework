// -----------------------------------------------------------------------
// <copyright file="IProductCategoryService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.IService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProductCategoryService
    {
        List<ProductCategoryInfo> GetProductCategory();

        int Insert(ProductCategoryInfo model,int parentId);

        ProductCategoryInfo GetProductCategoryById(int id);

        int DeleteById(int id);

        int DeleteCurrentNodeOnlyById(int id);

        int Update(ProductCategoryInfo model);
    }
}
