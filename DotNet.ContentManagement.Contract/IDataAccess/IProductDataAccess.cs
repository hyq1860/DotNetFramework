// -----------------------------------------------------------------------
// <copyright file="IProductDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProductDataAccess
    {
        int Add(ProductInfo model);

        int Update(ProductInfo model);

        int Delete(int id);

        /// <summary>
        /// 根据产品分类删除产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        int DeleteProductByCategoryId(int categoryId);

        List<ProductInfo> GetList(string strWhere);

        //获取商品数量
        int GetProductCount(string strWhere);

        ProductInfo GetProductById(int id);

        List<ProductInfo> GetProducts(int pageSize, int pageIndex, string where);

        //根据产品分类id获取产品分类
        List<ProductInfo> GetProductsByCategoryId(int categoryId);
    }
}
