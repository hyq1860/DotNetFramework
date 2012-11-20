using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotNet.Web.AjaxHandler
{
    /// <summary>
    /// 编码器委托
    /// </summary>
    /// <param name="defaultEncoder"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public delegate string OnSerializeHandler(WebResponseEncoder defaultEncoder, object obj);
    public abstract class WebResponseEncoder
    {
        protected HttpContext Context;

        /// <summary>
        /// 序列化之前
        /// </summary>
        protected OnSerializeHandler _onSerialize;

        internal event OnSerializeHandler OnSerialize
        {
            add
            {
                _onSerialize += value;
            }
            remove
            {
                _onSerialize -= value;
            }
        }

        public static WebResponseEncoder CreateInstance(HttpContext httpContext,ResponseFormat responseFormat)
        {
            switch (responseFormat)
            {
                case ResponseFormat.Json:
                    return new JsonEncoder(httpContext);
                case ResponseFormat.Xml:
                    return new XmlEncoder(httpContext);
                case ResponseFormat.Script:
                    return new JQueryScriptEncoder(httpContext);
                case ResponseFormat.Html:
                    return new HtmlEncoder(httpContext);
                case ResponseFormat.Text:
                    return new TextEncoder(httpContext);
                default:
                    return new TextEncoder(httpContext); 
            }
        }

        protected WebResponseEncoder(HttpContext httpContext)
        {
            List<string> accepts=new List<string>();
            accepts.AddRange(httpContext.Request.AcceptTypes);
            Context = httpContext;
        }

        /// <summary>
        /// 输出对象的类型
        /// </summary>
        public abstract string MimeType { get; }

        /// <summary>
        /// 序列化输出对象
        /// </summary>
        /// <param name="srcObject"></param>
        /// <returns></returns>
        public abstract string Seralize(object srcObject);

        /// <summary>
        /// 将输出对象写到输出流中
        /// </summary>
        /// <param name="srcObject"></param>
        public virtual void Write(object srcObject)
        {
            HttpResponse httpResponse = Context.Response;
            string str = Seralize(srcObject);
            httpResponse.ContentType = MimeType;
            httpResponse.ContentEncoding = Context.Request.ContentEncoding;
            httpResponse.Write(str);
        }
    }

    internal class JsonEncoder:WebResponseEncoder
    {
        public override string MimeType
        {
            get
            {
                return "text/html;charset=utf-8";
                //return "application/json";
            }
        }

        public JsonEncoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override string Seralize(object srcObject)
        {
            if (base._onSerialize!=null)
            {
                string result = base._onSerialize(this, srcObject);
                return result;
            }
            if (srcObject==null)
            {
                return "{}";
            }
            IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter() {DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"};
            return JsonConvert.SerializeObject(srcObject, Formatting.Indented, dateTimeConverter);
        }
    }

    internal class HtmlEncoder:WebResponseEncoder
    {
        public HtmlEncoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override string MimeType
        {
            get
            {
                return "text/html";
            }
        }
        public override string Seralize(object srcObject)
        {
            if(base._onSerialize!=null)
            {
                string result = base._onSerialize(this, srcObject);
                return result;
            }
            if(srcObject==null)
            {
                return "<html><body><h3>服务器执行成功!</h3><body></html>";
            }
            else
            {
                //return "<html><body><div id='result'>" + JsonConvert.SerializeObject(srcObject) + "</div><body></html>";
                return JsonConvert.SerializeObject(srcObject);
            }
        }
    }

    internal class JQueryScriptEncoder:WebResponseEncoder
    {
        public JQueryScriptEncoder(HttpContext httpContext) : base(httpContext)
        {

        }

        public override string MimeType
        {
            get
            {
                return "text/javascript";
            }
        }

        public override string Seralize(object srcObject)
        {
            if(base._onSerialize!=null)
            {
                string result = base._onSerialize(this, srcObject);
                return result;
            }
            return srcObject==null ? string.Empty : srcObject.ToString();
        }
    }

    internal class XmlEncoder:WebResponseEncoder
    {
        public XmlEncoder(HttpContext httpContext) : base(httpContext)
        {
        }

        #region Overrides of WebResponseEncoder

        /// <summary>
        /// 输出对象的类型
        /// </summary>
        public override string MimeType
        {
            get { return @"text/xml"; }
        }

        /// <summary>
        /// 序列化输出对象
        /// </summary>
        /// <param name="srcObject"></param>
        /// <returns></returns>
        public override string Seralize(object srcObject)
        {
            if (base._onSerialize != null) 
            {
                string result = base._onSerialize(this, srcObject);
                return result;
            }
            if (srcObject == null) {
                return string.Empty;
            }
            return "序列化为xml";
        }

        #endregion
    }

    internal class TextEncoder:WebResponseEncoder
    {
        public TextEncoder(HttpContext httpContext) : base(httpContext)
        {
        }

        #region Overrides of WebResponseEncoder

        /// <summary>
        /// 输出对象的类型
        /// </summary>
        public override string MimeType
        {
            get { return @"text/plain"; }
        }

        /// <summary>
        /// 序列化输出对象
        /// </summary>
        /// <param name="srcObject"></param>
        /// <returns></returns>
        public override string Seralize(object srcObject)
        {
            if (srcObject != null)
                return srcObject.ToString();
            return string.Empty;
        }

        #endregion
    }
}
