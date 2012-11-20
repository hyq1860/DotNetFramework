// -----------------------------------------------------------------------
// <copyright file="JinDongProductSpider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;

    #region 京东
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JinDongProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 易讯
    public class WuYiBuyProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            return new ProductInfo();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 当当
    public class DangDangProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 苏宁
    public class SuNingProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 亚马逊
    public class AmazonProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
