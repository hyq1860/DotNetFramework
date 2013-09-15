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

    public partial class ProductList : System.Web.UI.Page
    {
        IProductService productService=new ProductService();
        IProductCategoryService productCategoryService=new ProductCategoryService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int CategoryId
        {
            get
            {
                return int.Parse(Request.QueryString["id"]);
            }
        }

        public ProductCategoryInfo Parent
        {
            get
            {
                return null;
            }
        }

        public IEnumerable<ProductCategoryInfo> _firstCagegorys;

        public IEnumerable<ProductCategoryInfo> FirstCagegorys
        {
            get
            {
                if(this._firstCagegorys==null)
                {
                    this._firstCagegorys = productCategoryService.GetProductCategory().Where(item => item.Depth == 1&&item.DataStatus==1);
                }
                return _firstCagegorys;
            }
        }

        private ProductCategoryInfo _productCategory;

        public ProductCategoryInfo ProductCategory
        {
            get
            {
                if(this._productCategory==null)
                {
                    _productCategory=productCategoryService.GetProductCategoryById(CategoryId);
                }
                return _productCategory;
            }
        }

        private List<ProductInfo> _seriesProducts;

        public List<ProductInfo> SeriesProducts
        {
            get
            {
                if (this._seriesProducts==null)
                {
                    _seriesProducts=productService.GetProductsByCategoryId(CategoryId).Where(s=>!string.IsNullOrEmpty(s.MediumPicture)&&s.Category.DataStatus==1).ToList();
                }
                return _seriesProducts;
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