// -----------------------------------------------------------------------
// <copyright file="IProductCategoryDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.IDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProductCategoryDataAccess
    {
        List<ProductCategoryInfo> GetProductCategory();

        int Insert(ProductCategoryInfo model, int parentId);

        ProductCategoryInfo GetProductCategoryById(int id);

        int DeleteById(int id);

        int DeleteCurrentNodeOnlyById(int id);

        int Update(ProductCategoryInfo model);
    }
}
