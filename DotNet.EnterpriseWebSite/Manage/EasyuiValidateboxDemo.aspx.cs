using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Web.Configuration;

namespace DotNet.EnterpriseWebSite.Manage
{
    public partial class EasyuiValidateboxDemo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var data= ConfigHelper.ListConfig.GetListItems("SiteMapGroup");
            if(Request.HttpMethod.ToLower()=="post")
            {
                    Response.Write("true");
                    Response.End();
            }
            
        }
    }
}