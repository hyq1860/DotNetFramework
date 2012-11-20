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
    using DotNet.ContentManagement.Service;

    public partial class ContentV2 : System.Web.UI.Page
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
                if (_basicContent == null)
                {

                    _basicContent = basicContentService.GetBasicContentById(int.Parse(Request.QueryString["id"]));
                }
                return _basicContent;
            }
        }
    }
}