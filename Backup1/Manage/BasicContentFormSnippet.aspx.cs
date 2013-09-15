using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Service;

    public partial class BasicContentFormSnippet : PageBase
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

        private IBasicContentService basicContentService = new BasicContentService();

        public BasicContentInfo BasicContent
        {
            get
            {
                int id;
                if (int.TryParse(Id, out id))
                {
                    return basicContentService.GetBasicContentById(id);
                }
                return null;
            }
        }
    }
}