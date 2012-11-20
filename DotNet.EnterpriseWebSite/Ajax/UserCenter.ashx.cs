using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet.EnterpriseWebSite.Ajax
{
    using DotNet.Base;
    using DotNet.Base.Contract;
    using DotNet.Web.AjaxHandler;
    using DotNet.Web.Configuration;
    using DotNet.Web.StateManagement;

    /// <summary>
    /// Summary description for UserCenter
    /// </summary>
    public class UserCenter : WebHandler
    {
        [ResponseAnnotation(ResponseFormat = ResponseFormat.Text, MethodDescription = "用户登录")]
        public int Login(string nickName, string password)
        {
            IUserService userService = new UserService();
            //string tempPassword = DotNet.Common.CryptographyHelper.MD5EncryptHash(DotNet.Common.CryptographyHelper.MD5EncryptHash(password) + nickName);
            int result = userService.Exists(nickName, password);
            if (result > 0)
            {
                User user = userService.GetUser(result);
                //DataTable dt = userService.GetUserInfo(result);
                //UserExtention userExtention = userService.GetUserExtention(result);
                string key = ConfigHelper.ParamsConfig.GetParamValue("Key");
                string iv = ConfigHelper.ParamsConfig.GetParamValue("IV");
                StateProvider.Current.Add(LoginCookieName.LoginId, nickName);
                StateProvider.Current.Add(LoginCookieName.UserId, DotNet.Common.CryptographyHelper.AESEncrypt(user.UserId.ToString(), key, iv));
                StateProvider.Current.Add(LoginCookieName.UserName, DotNet.Common.CryptographyHelper.AESEncrypt(user.NickName, key, iv));
            }
            return result;
        }
    }
}