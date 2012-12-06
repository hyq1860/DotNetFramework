// -----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductService:IProductService
    {

        private IProductDataAccess productDataAccess;

        public ProductService(IProductDataAccess productDataAccess)
        {
            this.productDataAccess = productDataAccess;
        }

        public List<ProductInfo> GetProducts(string sqlWhere)
        {
            return productDataAccess.GetProducts(sqlWhere);
        }

        public bool Insert(ProductInfo model)
        {
            return productDataAccess.Insert(model);
        }

        public bool Update(ProductInfo product)
        {
            return productDataAccess.Update(product);
        }
    }
}
