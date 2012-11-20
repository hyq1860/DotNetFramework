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

    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string _rootPath;

        public string RootPath
        {
            get
            {
                if (string.IsNullOrEmpty(this._rootPath))
                {
                    var HttpCurrent = HttpContext.Current;

                    if (HttpCurrent != null)
                    {
                        string UrlAuthority = HttpCurrent.Request.Url.GetLeftPart(UriPartial.Authority);
                        if (HttpCurrent.Request.ApplicationPath == null || HttpCurrent.Request.ApplicationPath == "/")
                        {
                            // 直接安装在Web站点   
                            _rootPath = UrlAuthority;
                        }
                        else
                        {
                            // 安装在虚拟子目录下   
                            _rootPath = UrlAuthority + HttpCurrent.Request.ApplicationPath;
                        }
                    }
                }

                return _rootPath;
            }
        }

        private List<ProductInfo> products;

        public List<ProductInfo> Products
        {
            get
            {
                if (products == null)
                {
                    IProductService productService = new ProductService();

                    products = productService.GetProducts(20, 1, "IsFocusPicture=0").Where(item => !string.IsNullOrEmpty(item.MediumPicture) && item.Category.DataStatus == 1).ToList();

                }
                return products;
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

        private BasicContentInfo _basicContent;
        private IBasicContentService basicContentService = new BasicContentService();
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