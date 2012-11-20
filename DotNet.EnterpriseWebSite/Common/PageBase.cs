using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DotNet.Base.Contract;
using DotNet.Web.Configuration;
using DotNet.Web.StateManagement;
using DotNet;

namespace DotNet.EnterpriseWebSite
{
    public abstract class PageBase : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            CheckIsLogin();
            base.OnLoad(e);
        }
        /// <summary>
        /// 状态管理
        /// </summary>
        public IStateProvider State
        {
            get { return StateProvider.Current; }
        }

        public bool IsMainPage { get; set; }

        public bool IsLogined
        {
            get 
            { 
                string loginId = State.GetStringValue(LoginCookieName.LoginId);
                string encryptLoginId = StateProvider.Current.GetStringValue(LoginCookieName.UserName);
                if (string.IsNullOrEmpty(loginId) || string.IsNullOrEmpty(encryptLoginId))
                    return false;
                string key = ConfigHelper.ParamsConfig.GetParamValue("Key");
                string iv = ConfigHelper.ParamsConfig.GetParamValue("IV");
                string decryptLoginId = DotNet.Common.CryptographyHelper.AESDecrypt(encryptLoginId, key, iv);
                return (!string.IsNullOrEmpty(loginId) && !string.IsNullOrEmpty(encryptLoginId) &&
                        loginId == decryptLoginId);
            }
        }

        /// <summary>
        /// 校验是否登陆
        /// </summary>
        public void CheckIsLogin()
        {
            if (!IsLogined)
            {
                if (this.IsMainPage)
                {
                    Response.Write("<script>document.location='Login.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>parent.document.location='Login.aspx'</script>");
                }
            }
        }

        public string  UserName
        {
            get
            {
                string userName = State.GetStringValue(LoginCookieName.UserName);
                string key = ConfigHelper.ParamsConfig.GetParamValue("Key");
                string iv = ConfigHelper.ParamsConfig.GetParamValue("IV");
                if (!string.IsNullOrEmpty(userName))
                {
                    return DotNet.Common.CryptographyHelper.AESDecrypt(userName, key, iv);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string UserId
        {
            get
            {
                string userId = State.GetStringValue(LoginCookieName.UserId);
                string key = ConfigHelper.ParamsConfig.GetParamValue("Key");
                string iv = ConfigHelper.ParamsConfig.GetParamValue("IV");
                if(!string.IsNullOrEmpty(userId))
                {
                    return DotNet.Common.CryptographyHelper.AESDecrypt(userId, key, iv);
                }
                else
                {
                    return string.Empty;
                }
            }
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
