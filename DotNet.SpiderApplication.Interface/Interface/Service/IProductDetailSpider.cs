// -----------------------------------------------------------------------
// <copyright file="IProductDetailSpider.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.SpiderApplication.Contract.Entity;

    /// <summary>
    /// 商品采集接口
    /// </summary>
    public interface IProductSpider
    {
        ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct);

        List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory);
    }
}
