using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.Service;

    public partial class ArticleForm : PageBase
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

        private ArticleInfo _article;

        public ArticleInfo Article
        {
            get
            {
                int id;
                if(_article==null&&int.TryParse(Id,out id))
                {
                    IArticleService articleService = new ArticleService();
                    _article = articleService.GetArticleById(id);
                }
                return _article;
            }
        }
    }
}