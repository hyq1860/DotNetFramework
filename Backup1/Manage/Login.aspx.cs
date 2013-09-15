using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite.Manage
{
    public partial class Login : System.Web.UI.Page
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
    }
}