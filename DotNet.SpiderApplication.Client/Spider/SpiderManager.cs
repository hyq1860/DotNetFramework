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

    using DotNet.BasicSpider;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;

    using csExWB;

    /// <summary>
    /// 采集管理
    /// </summary>
    public class SpiderManager
    {
        private static Dictionary<int, IProductSpider> spiderDictionary;

        static SpiderManager()
        {
            spiderDictionary=new Dictionary<int, IProductSpider>();
            spiderDictionary.Add(1, new JinDongProductSpider());
            spiderDictionary.Add(2, new YiHaoDianSpider());
            spiderDictionary.Add(3, new WuYiBuyProductSpider());
            spiderDictionary.Add(4, new SuNingSpider());
            spiderDictionary.Add(5, new DangDangSpider());

            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.TimeOut = 15;

        }

        /// <summary>
        /// 采集商品详情
        /// </summary>
        /// <param name="spiderProduct"></param>
        /// <returns></returns>
        public static ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            if (spiderDictionary.ContainsKey(spiderProduct.ECPlatformId))
            {
                return spiderDictionary[spiderProduct.ECPlatformId].SpiderProductDetail(spiderProduct);
            }

            return null;
        }

        /// <summary>
        /// 采集商品列表
        /// </summary>
        /// <param name="spiderCategory"></param>
        /// <returns></returns>
        public static List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            if (spiderDictionary.ContainsKey(spiderCategory.ECPlatformId))
            {
                return spiderDictionary[spiderCategory.ECPlatformId].SpiderProductList(spiderCategory);
            }

            return null;
        }
    }
}
