// -----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="">
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
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductService:IProductService
    {
        private IProductDataAccess _dataAccess=new ProductDataAccess();
        public int Add(ProductInfo model)
        {
            model.Id = KeyGenerator.Instance.GetNextValue("Product");
            return _dataAccess.Add(model);
        }

        public int Update(ProductInfo model)
        {
            return _dataAccess.Update(model);
        }

        public int DeleteById(int id)
        {
            return _dataAccess.Delete(id);
        }

        public int DeleteProductByCategoryId(int categoryId)
        {
            return _dataAccess.DeleteProductByCategoryId(categoryId);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public ProductInfo GetProductById(int id)
        {
            return _dataAccess.GetProductById(id);
        }

        public int GetProductCount(string strWhere)
        {
            return _dataAccess.GetProductCount(strWhere);
        }

        public List<ProductInfo> GetProducts(int pageSize, int pageIndex, string strWhere)
        {
            return _dataAccess.GetProducts(pageSize, pageIndex, strWhere);
        }

        public List<ProductInfo> GetProductsByCategoryId(int categoryId)
        {
            return _dataAccess.GetProductsByCategoryId(categoryId);
        }
    }
}
