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

    public partial class ProductCenter : System.Web.UI.Page
    {
        private IProductCategoryService productCategoryService=new ProductCategoryService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<ProductCategoryInfo>  FirstCagegorys
        {
            get
            {
                return productCategoryService.GetProductCategory().Where(item => item.Depth == 1&&item.DataStatus==1);
            }
        }

        public int Id
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["id"])) return 0;
                return int.Parse(Request.QueryString["id"]);
            }
        }

        public IEnumerable<ProductCategoryInfo> Series
        {
            get
            {
                if (Id==0)
                {
                    return productCategoryService.GetProductCategory().Where(item => item.Depth == 2 && item.DataStatus == 1);
                }
                else
                {
                    var parent = productCategoryService.GetProductCategoryById(Id);
                    return productCategoryService.GetProductCategory().Where(item => item.Lft > parent.Lft&&item.Rgt<parent.Rgt&&item.DataStatus==1);
                }
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