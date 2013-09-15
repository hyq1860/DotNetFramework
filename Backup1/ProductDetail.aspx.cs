using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite
{
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.Service;

    public partial class ProductDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        IProductService productService = new ProductService();
        IProductCategoryService productCategoryService = new ProductCategoryService();

        private ProductInfo _product;

        public ProductInfo Product
        {
            get
            {
                if(_product==null)
                {
                    _product = productService.GetProductById(int.Parse(Request.QueryString["id"]));
                }
                return _product;
            }
        }

        private ProductCategoryInfo _productCategory;

        public ProductCategoryInfo ProductCategory
        {
            get
            {
                if (this._productCategory == null)
                {
                    _productCategory = productCategoryService.GetProductCategoryById(Product.CategoryId);
                }
                return _productCategory;
            }
        }

        public IEnumerable<ProductCategoryInfo> _firstCagegorys;

        public IEnumerable<ProductCategoryInfo> FirstCagegorys
        {
            get
            {
                if (this._firstCagegorys == null)
                {
                    this._firstCagegorys = productCategoryService.GetProductCategory().Where(item => item.Depth == 1);
                }
                return _firstCagegorys;
            }
        }

        private List<ArticleInfo> _focusPicture;
        public List<ArticleInfo> FocusArticle
        {
            get
            {
                if (_focusPicture == null)
                {
                    IArticleService articleService = new ArticleService();
                    _focusPicture = articleService.GetArticles(4, 1, "t2.Type=1 and t1.DataStatus=1 and t1.FocusPicture is not null ");
                }
                return _focusPicture;
            }
        }

        private List<ProductCategoryInfo> _allCagegorys;

        public List<ProductCategoryInfo> AllCagegorys
        {
            get
            {
                if (_allCagegorys == null)
                {
                    _allCagegorys = this.productCategoryService.GetProductCategory().Where(s => s.Depth > 0 && s.DataStatus == 1).ToList();
                }
                return _allCagegorys;
            }
        }
    }
}