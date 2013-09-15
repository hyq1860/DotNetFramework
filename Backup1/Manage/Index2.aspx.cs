using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Base;
using DotNet.Base.Contract;
using DotNet.Common.Utility;

namespace DotNet.EnterpriseWebSite.Manage
{
    public partial class Index2 : PageBase
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