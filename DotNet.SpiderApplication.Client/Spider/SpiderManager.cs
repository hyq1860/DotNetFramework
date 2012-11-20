// -----------------------------------------------------------------------
// <copyright file="SpiderManager.cs" company="">
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

    /// <summary>
    /// 采集管理
    /// </summary>
    public class SpiderManager
    {
        private Dictionary<int, IProductSpider> spiderDictionary;

        public SpiderManager()
        {
            spiderDictionary=new Dictionary<int, IProductSpider>();
            spiderDictionary.Add(1, new JinDongProductSpider());
            spiderDictionary.Add(2, new JinDongProductSpider());
            spiderDictionary.Add(3, new JinDongProductSpider());
            spiderDictionary.Add(4, new JinDongProductSpider());
            spiderDictionary.Add(5, new JinDongProductSpider());
        }

        /// <summary>
        /// 采集商品详情
        /// </summary>
        /// <param name="spiderProduct"></param>
        /// <returns></returns>
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            if (this.spiderDictionary.ContainsKey(spiderProduct.ECPlatformId))
            {
                return this.spiderDictionary[spiderProduct.ECPlatformId].SpiderProductDetail(spiderProduct);
            }

            return null;
        }

        /// <summary>
        /// 采集商品列表
        /// </summary>
        /// <param name="spiderCategory"></param>
        /// <returns></returns>
        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            if (this.spiderDictionary.ContainsKey(spiderCategory.ECPlatformId))
            {
                return this.spiderDictionary[spiderCategory.ECPlatformId].SpiderProductList(spiderCategory);
            }

            return null;
        }
    }
}
