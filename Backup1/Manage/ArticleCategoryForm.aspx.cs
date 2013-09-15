using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Service;

    public partial class ArticleCategoryForm : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string Id
        {
            get
            {
                return Request.QueryString["id"];
            }
        }

        private IArticleCategoryService articleCategoryService = new ArticleCategoryService();

        private ArticleCategoryInfo _articleCategory;

        public ArticleCategoryInfo ArticleCategory
        {
            get
            {
                int id;
                if (_articleCategory == null)
                {
                    if (int.TryParse(Id, out id))
                    {
                        _articleCategory = articleCategoryService.GetArticleCategoryById(id);
                    }
                }

                return _articleCategory;
            }
        }
    }
}