using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Web.AjaxHandler;

namespace DotNet.EnterpriseWebSite.Ajax
{
    /// <summary>
    /// Summary description for Validate
    /// </summary>
    public class Validate : WebHandler
    {
        [ResponseAnnotation(ResponseFormat = ResponseFormat.Json, CacheDurtion = 0, MethodDescription = "校验", HttpMethod = "POST")]
        public bool ValidateName()
        {
            return true;
        }
    }
}