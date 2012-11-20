using System;
using System.Text;
using System.Web.UI;
using System.IO;

namespace DotNet.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">绑定数据类型</typeparam>
        /// <param name="path">用户控件路径</param>
        /// <param name="dataToBind">绑定的数据</param>
        /// <returns></returns>
        public static string RenderView<T>(string path,T dataToBind)
        {
            //#region 第一种
            //Page pageHolder = new Page();
            //UserControl viewControl = (UserControl)pageHolder.LoadControl(path);
            //if (viewControl is IRenderable<T>)
            //{
            //    if (dataToBind != null)
            //    {
            //        ((IRenderable<T>)viewControl).PopulateData(dataToBind);
            //    }
            //}
            //pageHolder.Controls.Add(viewControl);
            //using (StringWriter output = new StringWriter())
            //{
            //    HttpContext.Current.Server.Execute(pageHolder, output, false);
            //    return output.ToString();
            //}
            //#endregion

            #region 第二种
            Page pageHolder = new Page();
            Control viewControl = pageHolder.LoadControl(path);
            if (viewControl is IRenderable<T>)
            {
                if (dataToBind != null)
                {
                    ((IRenderable<T>)viewControl).PopulateData(dataToBind);
                }
            }
            StringBuilder sb = new StringBuilder();
            using (StringWriter output = new StringWriter(sb))
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(output))
                {
                    viewControl.RenderControl(htw);
                    return sb.ToString();
                }
            }
            #endregion 
        }

        #region 第三种
        public static string RangerUsControl(string controlName)
        {
            StringBuilder build = new StringBuilder();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(new StringWriter(build));
            UserControl uc = new UserControl();
            Control ctrl = uc.LoadControl(controlName + ".ascx");//加载用户定义控件
            htmlWriter.Flush();
            string result=string.Empty;
            try 
            {
                ctrl.RenderControl(htmlWriter);
            }
            catch(Exception ex)
            {
                //记日志
                throw;
            }
            finally 
            {
                htmlWriter.Flush();
                result = build.ToString();
            }
            return result;//返回控件的HTML代码
        }
        #endregion
    }
}
