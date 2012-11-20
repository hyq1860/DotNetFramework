using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DotNet.Web.AjaxHandler
{
    /// <summary>
    /// 解码
    /// </summary>
    public abstract class WebRequestDecoder
    {
        protected HttpContext Context;
        protected WebRequestDecoder(HttpContext httpContext)
        {
            Context = httpContext;
        }

        /// <summary>
        /// 创建一个解码器
        /// </summary>
        /// <returns></returns>
        public static WebRequestDecoder CreateInstance(HttpContext context)
        {
            string contentType = context.Request.ContentType.ToLower();
            if (contentType.IndexOf("application/json")>=0)
            {
                return new JsonDecoder(context);
            }
            else if (contentType.IndexOf("application/contract") >= 0) 
            {
                return new JsonContractDecoder(context);
            }
            else if (contentType.IndexOf("application/x-www-form-urlencoded")>=0)
            {
                return new SimpleUrlDecoder(context);
            }
            else
            {
                throw new NotImplementedException("未实现");
                //return new SimpleUrlDecoder(context);
            }
        }

        /// <summary>
        /// 取得逻辑方法名称
        /// </summary>
        public virtual string LogicalMethodName
        {
            get
            {
                //Context.Request.Url.Segments:获取包含构成指定 URI 的路径段的数组。
                int length = Context.Request.Url.Segments.Length;
                string methodName = Context.Request.Url.Segments[length - 1];
                return methodName;
            }
        }
        /// <summary>
        /// 反序列化请求中的数据
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, object> Deserialize();
    }

    /// <summary>
    /// json格式的解码器
    /// </summary>
    internal class JsonDecoder:WebRequestDecoder
    {
        public JsonDecoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override Dictionary<string, object> Deserialize()
        {
            string input = null;
            using (StreamReader sr = new StreamReader(Context.Request.InputStream))
            {
                //Encoding encoding = Context.Request.ContentEncoding;
                input = sr.ReadToEnd();
            }

            //Stream stream = Context.Request.InputStream;//获取传入的 HTTP 实体主体的内容
            //stream.Position = 0;
            //byte[] buffer = new byte[stream.Length];
            //stream.Read(buffer, 0, (int)stream.Length);
            //Encoding encoding = Context.Request.ContentEncoding;
            //string streamString = encoding.GetString(buffer);
            object obj = JsonConvert.DeserializeObject(input);
            //转换json到字典
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());
            //Dictionary<string, object> result = obj as Dictionary<string, object>;
            NameValueCollection queryString = Context.Request.QueryString;
            foreach (string item in queryString)
            {
                result.Add(item, queryString[item]);
            }
            return result;
        }
    }

    internal class JsonContractDecoder:WebRequestDecoder
    {
        public JsonContractDecoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override Dictionary<string, object> Deserialize()
        {
            Stream stream = Context.Request.InputStream;
            stream.Position = 0;
            byte[] buffer=new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);
            Encoding encoding = Context.Request.ContentEncoding;
            string streamString = encoding.GetString(buffer);
            JavaScriptSerializer jser = new JavaScriptSerializer();
            object obj = jser.DeserializeObject(streamString);
            Dictionary<string, object> result = obj as Dictionary<string,object>;
            NameValueCollection queryString = Context.Request.QueryString;
            if (result!=null)
            {
                foreach (string item in queryString) 
                {
                    result.Add(item, queryString[item]);
                }
            }
            return result;
        }
    }

    internal class SimpleUrlDecoder:WebRequestDecoder
    {
        public SimpleUrlDecoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override Dictionary<string, object> Deserialize()
        {
            Dictionary<string,object> data=new Dictionary<string, object>();
            NameValueCollection queryString = Context.Request.Params;
            //NameValueCollection form = Context.Request.Form;
            //NameValueCollection nameValues=new NameValueCollection();
            //nameValues.Add(queryString);
            //nameValues.Add(form);
            foreach (string item in queryString)
            {
                if(item=="ALL_HTTP")
                    break;
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                data.Add(item, queryString[item]);
            }
            return data;
        }

        public override string LogicalMethodName
        {
            get
            {
                int length = Context.Request.Url.Segments.Length;
                string methodName = Context.Request.Url.Segments[length - 1];
                if (methodName.ToLower().LastIndexOf(".ashx") >= 0)
                {
                    return "GetSvr";
                }
                return methodName;
            }
        }
    }
}
