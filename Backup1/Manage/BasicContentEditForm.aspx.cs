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

    public partial class BasicContentEditForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public BasicContentInfo BasicContent
        {
            get
            {
                IBasicContentService basicContentService=new BasicContentService();
                return basicContentService.GetBasicContentById(int.Parse(Request.QueryString["id"]));
            }
        }
    }
}