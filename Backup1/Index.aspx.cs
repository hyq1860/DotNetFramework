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

    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private IProductCategoryService productCategoryService = new ProductCategoryService();

        private List<ProductCategoryInfo> _allCagegorys;

        public List<ProductCategoryInfo> AllCagegorys
        {
            get
            {
                if (_allCagegorys==null)
                {
                    _allCagegorys = this.productCategoryService.GetProductCategory().Where(s=>s.Depth>0&&s.DataStatus==1).ToList();
                }
                return _allCagegorys;
            }
        }

        private List<ProductCategoryInfo> _firstCagegorys;

        public List<ProductCategoryInfo> FirstCagegorys
        {
            get
            {
                if (_firstCagegorys == null)
                {
                    _firstCagegorys = this.productCategoryService.GetProductCategory().Where(s => s.Depth == 1 && s.DataStatus == 1).ToList();
                }
                return _firstCagegorys;
            }
        }

        public List<ProductInfo> Products
        {
            get
            {
                IProductService productService = new ProductService();

                return productService.GetProducts(20, 1, "IsFocusPicture=1").Where(item => !string.IsNullOrEmpty(item.MediumPicture) && item.Category.DataStatus == 1).ToList();
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

        private IBasicContentService basicContentService = new BasicContentService();

        private BasicContentInfo _basicContent;

        public BasicContentInfo BasicContent
        {
            get
            {
                if (_basicContent == null)
                {

                    _basicContent = basicContentService.GetBasicContentById(1);
                }
                return _basicContent;
            }
        }
    }
}