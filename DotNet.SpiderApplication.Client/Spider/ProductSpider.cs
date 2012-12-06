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

    using DotNet.BasicSpider;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;
    using DotNet.Web.Http;

    using csExWB;

    #region 京东
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JinDongProductSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            var html=WebBrowerManager.Instance.Run(spiderProduct.Url);
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            //标题
            var title = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[6]/div[1]/div[1]/h1[1]");
            var price = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='p-price']/img");
            // 文字价格
            var priceText = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[6]/div[1]/div[2]/ul[1]/li[2]/script[1]");

            // 产品图片
            var defaultImage = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[5]/div[1]/div[2]/div[1]");

            // 促销信息是ajax
            if (title != null && price != null && priceText != null)
            {
                var beginIndex = priceText.InnerText.IndexOf("京东价：￥");

                var endIndex = priceText.InnerText.IndexOf("。", beginIndex);
                var readPrice = priceText.InnerText.Substring(beginIndex + "京东价：￥".Length, endIndex - beginIndex - "京东价：￥".Length);
                decimal decimalRealPrice = 0;
                if (decimal.TryParse(readPrice, out decimalRealPrice))
                {
                    //UpdateProduct(productId, title.InnerText, decimal.Parse(readPrice));
                }

            }

            return new ProductInfo() { Source = html, ProductId = spiderProduct.ProductId };
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
    public class DangDangSpider:IProductSpider
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
    public class SuNingSpider:IProductSpider
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

    #region 一号店
    public class YiHaoDianSpider:IProductSpider
    {
        public ProductInfo SpiderProductDetail(SpiderProductInfo spiderProduct)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> SpiderProductList(SpiderCategoryInfo spiderCategory)
        {
            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.TimeOut = 15;
            var html = WebBrowerManager.Instance.Run(spiderCategory.CategoryUrl);
            var htmlDocument = HtmlAgilityPackHelper.GetHtmlDocument(html);

            htmlDocument.GetElementbyId("search_result");
            return new List<ProductInfo>();
        }
    }
    #endregion
}
