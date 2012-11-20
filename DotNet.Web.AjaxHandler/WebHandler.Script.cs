using System;
using System.Web;
using System.Reflection;
using System.IO;
using System.Text;

namespace DotNet.Web.AjaxHandler
{
    public partial class WebHandler
    {
        /// <summary>
        /// 取得服务的客户端代理的脚本86400
        /// </summary>
        /// <returns></returns>
        [ResponseAnnotation(CacheDurtion = 0, ResponseFormat = ResponseFormat.Script)]
        public StringBuilder GetSvr()
        {
            Type type = this.GetType();
            Uri url = HttpContext.Current.Request.Url;
            string script = string.Empty;
            if(!string.IsNullOrEmpty(ReflectionCache.GetInstance().JsFile))
            {
                script = ReflectionCache.GetInstance().JsFile;
            }
            else
            {
                ReflectionCache.GetInstance().JsFile = GetScriptTemplete();
                script = ReflectionCache.GetInstance().JsFile;
            }
            script = script.Replace("%H_DESC%", "通过jQuery.ajax完成服务端函数调用");
            script = script.Replace("%H_DATE%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            script = script.Replace("%URL%", url.Query.Length > 0 ? url.ToString().Replace(url.Query, "") : url.ToString());
            script = script.Replace("%CLS%", type.Name);

            StringBuilder scriptBuilder = new StringBuilder(script);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo m in methods)
            {
                ResponseAnnotationAttribute resAnn = this.GetAnnation(m.Name);

                scriptBuilder.AppendLine("/*");
                scriptBuilder.AppendLine("功能说明:" + resAnn.MethodDescription);
                scriptBuilder.AppendLine("附加说明:缓存时间 " + resAnn.CacheDurtion.ToString() + " 秒");
                scriptBuilder.AppendLine("输出类型 " + resAnn.ResponseFormat.ToString());
                scriptBuilder.AppendLine("*/");
                if (string.IsNullOrEmpty(resAnn.HttpMethod))
                    resAnn.HttpMethod = "POST";
                string func = GetFunctionTemplete(m, resAnn.ResponseFormat, resAnn.HttpMethod);
                scriptBuilder.AppendLine(func);
            }
            return scriptBuilder;
        }

        /// <summary>
        /// 取得嵌入的脚本模版
        /// </summary>
        /// <returns></returns>
        private static string GetScriptTemplete()
        {
            Type type = typeof(WebHandler);
            AssemblyName asmName = new AssemblyName(type.Assembly.FullName);
            using (Stream stream = type.Assembly.GetManifestResourceStream(asmName.Name + ".net.js"))
            {
                if (stream != null)
                {
                    byte[] buffer = new byte[stream.Length];
                    int len = stream.Read(buffer, 0, (int)stream.Length);
                    string temp = Encoding.UTF8.GetString(buffer, 0, len);
                    return temp;
                }
                throw new FileNotFoundException("模版未找到");
            }
        }

        /// <summary>
        /// 取得函数的模版
        /// </summary>
        /// <param name="method"></param>
        /// <param name="format"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private static string GetFunctionTemplete(MethodInfo method, ResponseFormat format,string httpMethod)
        {
            StringBuilder func = new StringBuilder(method.DeclaringType.Name);
            func.Append(".prototype." + method.Name);
            func.Append("=function");
            func.Append("(");
            foreach (ParameterInfo p in method.GetParameters())
            {
                func.Append(p.Name + ",");
            }

            func.Append("callback)");
            func.AppendLine("{");
            {
                func.Append("\tvar args = {");
                foreach (ParameterInfo p in method.GetParameters())
                {
                    func.Append(p.Name + ":" + p.Name + ",");
                }
                //func.AppendLine("ajax:'jquery1.4.2'};");
                func.AppendLine("};");
                switch (format)
                {
                    case ResponseFormat.Html:
                        //func.AppendLine("\tvar options={dataType:'html'};");
                        func.AppendFormat("\tvar options={{dataType:'html',type:'{0}'}};",httpMethod);
                        break;
                    case ResponseFormat.Xml:
                        //func.AppendLine("\tvar options={dataType:'xml'};");
                        func.AppendFormat("\tvar options={{dataType:'xml',type:'{0}'}};", httpMethod);
                        break;
                    case ResponseFormat.Json:
                        //func.AppendLine("\tvar options={dataType:'json'};");
                        func.AppendFormat("\t var options={{dataType:'json',type:'{0}'}};", httpMethod);
                        break;
                    case ResponseFormat.Script:
                        //func.AppendLine("\tvar options={dataType:'script'};");
                        func.AppendFormat("\tvar options={{dataType:'script',type:'{0}'}};",httpMethod);
                        break;
                    case ResponseFormat.Text:
                        //func.AppendLine("\tvar options={dataType:'text'};");
                        func.AppendFormat("\tvar options={{dataType:'text',type:'{0}'}};",httpMethod);
                        break;
                    default:
                        //func.AppendLine("\tvar options={dataType:'text'};");
                        //func.AppendFormat("\tvar options={{dataType:'text',type:'{0}'}};",httpMethod);
                        break;
                }
                func.AppendLine("\t$.extend(true,options,{},this.Options);");
                func.AppendFormat("\t$.DotNet.CallWebMethod(options,'{0}', args, callback);", method.Name);
                func.AppendLine();
            }
            func.AppendLine("}\t\t");
            return func.ToString();
        }
    }
}
