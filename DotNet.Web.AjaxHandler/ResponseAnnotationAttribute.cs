using System;

namespace DotNet.Web.AjaxHandler
{
    /// <summary>
    /// 返回数据类型枚举 XML  html、json、jsonp、script,text。
    /// </summary>
    public enum ResponseFormat
    {
        Json,
        Xml,
        Script,
        Html,
        Text,
        Jsonp
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ResponseAnnotationAttribute : Attribute, ICloneable
    {
        /// <summary>
        /// 默认的返回值处理标注
        /// </summary>
        internal static ResponseAnnotationAttribute Default = new ResponseAnnotationAttribute();

        private ResponseFormat _responseFormat;
        /// <summary>
        /// 返回值类型
        /// </summary>
        public ResponseFormat ResponseFormat
        {
            get { return _responseFormat; }
            set { _responseFormat = value; }
        }

        /// <summary>
        /// 当前ajax的请求类型 GET POST
        /// </summary>
        public string HttpMethod { get; set; }

        private int _cacheDuration;
        /// <summary>
        /// 设置或取得服务端缓存的时间，单位为秒。默认为0，即没有任何缓存。
        /// </summary>
        public int CacheDurtion
        {
            get { return _cacheDuration; }
            set { _cacheDuration = value; }
        }

        private int _parameter = 0;
        /// <summary>
        /// 设置或取得方法的参数个数
        /// </summary>
        public int Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        private string _methodDescription = "无";
        /// <summary>
        /// 设置或取得方法描述信息
        /// </summary>
        public string MethodDescription
        {
            get { return _methodDescription; }
            set { _methodDescription = value; }
        }

        public object Clone()
        {
            ResponseAnnotationAttribute responseAnnotationAttribute = new ResponseAnnotationAttribute();
            responseAnnotationAttribute.ResponseFormat = ResponseFormat;
            responseAnnotationAttribute.HttpMethod = HttpMethod;
            responseAnnotationAttribute.CacheDurtion = CacheDurtion;
            responseAnnotationAttribute.Parameter = Parameter;
            responseAnnotationAttribute.MethodDescription = MethodDescription;
            return responseAnnotationAttribute;
        }

    }
}
