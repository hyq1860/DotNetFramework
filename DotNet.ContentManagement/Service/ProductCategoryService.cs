// -----------------------------------------------------------------------
// <copyright file="ProductCategoryService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Base;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Contract.IDataAccess;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductCategoryService : IProductCategoryService
    {
        private static IProductCategoryDataAccess _dataAccess = new ProductCategoryDataAccess();
        public List<ProductCategoryInfo> GetProductCategory()
        {
            return _dataAccess.GetProductCategory();
        }

        public int Insert(ProductCategoryInfo model,int parentId)
        {
            model.Id = KeyGenerator.Instance.GetNextValue("ProductCategory");
            return _dataAccess.Insert(model, parentId);
        }

        public ProductCategoryInfo GetProductCategoryById(int id)
        {
            return _dataAccess.GetProductCategoryById(id);
        }

        public int DeleteById(int id)
        {
            return _dataAccess.DeleteById(id);
        }

        public int DeleteCurrentNodeOnlyById(int id)
        {
            return _dataAccess.DeleteCurrentNodeOnlyById(id);
        }

        public int Update(ProductCategoryInfo model)
        {
            return _dataAccess.Update(model);
        }
    }
}
