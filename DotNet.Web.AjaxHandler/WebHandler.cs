using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

using DotNet.Web.Configuration;
using DotNet.Web.StateManagement;

namespace DotNet.Web.AjaxHandler
{
    public enum LoginCookieName
    {
        UserId,
        UserName,
        LoginId,
        UserInfo
    }
    public partial class WebHandler:IHttpHandler,IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// 取得当前的Application
        /// </summary>
        protected HttpApplicationState Application
        {
            get { return HttpContext.Current.Application; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            #region 获取所有的方法
            //IPermissionDataAccess permissionDataAccess = new PermissionDataAccess();
            //var methods = this.GetType().GetMethods();
            //foreach (MethodInfo method in methods)
            //{
            //    ResponseAnnotationAttribute[] resAnns = (ResponseAnnotationAttribute[])method.GetCustomAttributes(typeof(ResponseAnnotationAttribute), false);
            //    ResponseAnnotationAttribute ann = null;
            //    ann = resAnns.Length == 0 ? ResponseAnnotationAttribute.Default : resAnns[0];
            //    ann = (ann as ICloneable).Clone() as ResponseAnnotationAttribute;
            //    Permission a = new Permission();
            //    a.PermissionId = KeyGenerator.Instance.GetNextValue("Permission");
            //    a.Code = method.Name;
            //    a.Fullname = ann.MethodDescription;
            //    permissionDataAccess.Insert(a);
            //}
            #endregion

            string contextMethod = string.Empty;
            //解码:get或者post的参数解析
            WebRequestDecoder decoder;
            try
            {
                decoder = WebRequestDecoder.CreateInstance(context);
            }
            catch (NotImplementedException)
            {
                decoder = OnDecodeResolve(context);
                if (decoder==null)
                {
                    throw new NotSupportedException("无法为当前的请求提供适当的解码器");
                }
            }
            contextMethod = decoder.LogicalMethodName;
            //登录操作超时判断
            if (contextMethod != "Login" && contextMethod != "GetSvr" && !IsLogined)
            {
                context.Response.Write("-100");
                return;
            }
            //权限判断
            //if (!PermissionSingleton.Instance.CheckPermission(User.RoleId, contextMethod))
            if(false)
            {
                context.Response.Write("-101");
                return;
            }

            //反射调用，返回结果
            object result = Invoke(contextMethod, decoder.Deserialize());
            ResponseAnnotationAttribute resAnn = GetAnnation(contextMethod);
            //编码阶段
            WebResponseEncoder encoder;
            try
            {
                encoder = WebResponseEncoder.CreateInstance(context, resAnn.ResponseFormat);
            }
            catch (NotImplementedException)
            {
                encoder = OnEncoderResolve(context, contextMethod, resAnn);
                if(encoder==null)
                {
                    throw new NotSupportedException("无法为当前的请求提供适当的编码器");
                } 
            }
            OnSerializeHandler handler = OnGetCustomerSerializer(contextMethod);
            if (handler!=null)
            {
                encoder.OnSerialize += handler;
            }
            //回复
            encoder.Write(result);
            InitializeCachePolicy(context,resAnn);
        }

        /// <summary>
        /// 取得当前的Session
        /// </summary>
        protected HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        /// <summary>
        /// 取得自定义的序列化处理器。如果重写该方法，并且返回值不为空，则该返回值方法取代默认的编码器的序列化方法
        /// </summary>
        /// <param name="contextMethod"></param>
        /// <returns></returns>
        protected virtual OnSerializeHandler OnGetCustomerSerializer(string contextMethod)
        {
            return null;
        }

        /// <summary>
        /// 设置缓存策略
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resAnn"></param>
        private static void InitializeCachePolicy(HttpContext context, ResponseAnnotationAttribute resAnn)
        {
            int cacheDuration = resAnn.CacheDurtion;

            if (cacheDuration > 0)
            {
                context.Response.Cache.SetCacheability(HttpCacheability.Server);
                context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(cacheDuration));
                context.Response.Cache.SetSlidingExpiration(false);
                context.Response.Cache.SetValidUntilExpires(true);
                if (resAnn.Parameter > 0)
                {
                    context.Response.Cache.VaryByParams["*"] = true;
                }
                else
                {
                    context.Response.Cache.VaryByParams.IgnoreParams = true;
                }
            }
            else
            {
                context.Response.Cache.SetNoServerCaching();
                context.Response.Cache.SetMaxAge(TimeSpan.Zero);
            }
        }

        /// <summary>
        /// 取得方法的返回值标注属性
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private ResponseAnnotationAttribute GetAnnation(string methodName)
        {
            MethodInfo methInfo;
            if(ReflectionCache.GetInstance().Get(methodName)==null)
            {
                methInfo = this.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
                ReflectionCache.GetInstance().Add(methodName, methInfo);
            }
            else
            {
                methInfo = ReflectionCache.GetInstance().Get(methodName);
            }
            if (methInfo == null) 
            {
                throw new Exception("指定的逻辑方法不存在");
            }
            ResponseAnnotationAttribute[] resAnns = (ResponseAnnotationAttribute[])methInfo.GetCustomAttributes(typeof(ResponseAnnotationAttribute), false);
            ResponseAnnotationAttribute ann = null;
            ann = resAnns.Length == 0 ? ResponseAnnotationAttribute.Default : resAnns[0];
            ann = (ann as ICloneable).Clone() as ResponseAnnotationAttribute;
            ParameterInfo[] ps = methInfo.GetParameters();
            if (ps != null)
                ann.Parameter = ps.Length;
            return ann;
        }

        /// <summary>
        /// 调用本地的方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private object Invoke(string methodName, Dictionary<string, object> args)
        {
            MethodInfo methInfo;
            if (ReflectionCache.GetInstance().Get(methodName) == null) 
            {
                methInfo = this.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
                ReflectionCache.GetInstance().Add(methodName, methInfo);
            }
            else 
            {
                methInfo = ReflectionCache.GetInstance().Get(methodName);
            }
            if (methInfo == null)
            {
                throw new Exception("指定的逻辑方法不存在");
            }
            object[] parameters = this.GetArguments(methodName, args);
            //object result = methInfo.Invoke(this, parameters);
            object result = ReflectionCache.GetMethodInvoker(methInfo).Invoke(this, parameters);
            return result;
        }

        /// <summary>
        /// 取得参数列表
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private object[] GetArguments(string methodName, Dictionary<string, object> data)
        {
            MethodInfo methInfo;
            if (ReflectionCache.GetInstance().Get(methodName) == null) 
            {
                methInfo = this.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
                ReflectionCache.GetInstance().Add(methodName, methInfo);
            }
            else 
            {
                methInfo = ReflectionCache.GetInstance().Get(methodName);
            }
            if (methInfo == null) 
            {
                throw new Exception("指定的逻辑方法不存在");
            }
            List<object> args = new List<object>();
            if (methInfo != null)
            {
                ParameterInfo[] paraInfos = methInfo.GetParameters();
                foreach (ParameterInfo p in paraInfos)
                {
                    object obj = data[p.Name];
                    if (null != obj)
                    {
                        args.Add(Convert.ChangeType(obj, p.ParameterType));
                    }
                }
            }
            return args.ToArray();
        }

        /// <summary>
        /// 当默认的编码器配置无法提供的时候，根据上下文解析新的编码器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contextMethod"></param>
        /// <param name="resAnn"></param>
        /// <returns></returns>
        protected virtual WebResponseEncoder OnEncoderResolve(HttpContext context, string contextMethod, ResponseAnnotationAttribute resAnn)
        {
            if (contextMethod == "GetSvr")
            {
                return new JQueryScriptEncoder(context);
            }
            return null;
        }

        /// <summary>
        /// 当默认的解码器配置无法提供的时候，根据上下文解析新的解码器
        /// 可以进行重写
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual WebRequestDecoder OnDecodeResolve(HttpContext context)
        {
            return new SimpleUrlDecoder(context);
        }

        public bool IsLogined
        {
            get { return true; }
        }
    }
}
