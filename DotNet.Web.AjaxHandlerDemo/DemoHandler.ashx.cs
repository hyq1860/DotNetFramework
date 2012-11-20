using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using DotNet.Web.AjaxHandler;

namespace DotNet.Web.AjaxHandlerDemo
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DemoHandler : WebHandler
    {
        [ResponseAnnotation(ResponseFormat = ResponseFormat.Html,CacheDurtion = 30, MethodDescription = "演示返回动态对象和服务端缓存")]
        public string GetVar(string str, int i)
        {
            //var obj = new { total = i, str = str };

            //return obj;
            //return str + ":" + i;
            return "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        }

        [ResponseAnnotation(ResponseFormat = ResponseFormat.Text, CacheDurtion = 30, MethodDescription = "演示返回动态对象和服务端缓存",HttpMethod = "get")]
        public object GetVar2(string str, int i)
        {
            var obj = new { total = i, str = str };

            return obj;
        }

    }
}
