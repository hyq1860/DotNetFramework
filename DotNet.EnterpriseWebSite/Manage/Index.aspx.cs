using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DotNet.Common.Utility;

namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.Base;
    using DotNet.Base.Contract;

    public partial class Index : PageBase
    {

        protected string json = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IModuleService moduleService = new ModuleService();
            List<Module> menus = moduleService.GetTreeModules();
            json = menus.ToJson();
            IsMainPage = true;
        }
    }
}