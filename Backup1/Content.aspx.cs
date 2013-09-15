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

    public partial class Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private IBasicContentService basicContentService = new BasicContentService();

        private BasicContentInfo _basicContent;

        public BasicContentInfo BasicContent
        {
            get
            {
                if(_basicContent==null)
                {
                   
                    _basicContent = basicContentService.GetBasicContentById(int.Parse(Request.QueryString["id"]));
                }
                return _basicContent;
            }
        }

        private List<BasicContentInfo> _basicContentList; 
        public List<BasicContentInfo> BasicContentList
        {
            get
            {
                if(_basicContentList==null)
                {
                    _basicContentList = basicContentService.GetBasicContents();
                }
                return _basicContentList;
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
    }
}