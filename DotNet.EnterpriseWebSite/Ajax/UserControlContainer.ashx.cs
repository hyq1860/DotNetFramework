using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Web;

namespace DotNet.EnterpriseWebSite.Ajax
{
    /// <summary>
    /// Summary description for UserControlContainer
    /// </summary>
    public class UserControlContainer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string controlName = context.Request.QueryString["control"];
            var html = ViewManager.RangerUsControl(controlName);

            context.Response.Write(html);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}