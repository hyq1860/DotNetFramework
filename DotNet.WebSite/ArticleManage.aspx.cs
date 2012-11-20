using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.WebSite
{
    using System.Data;
    using System.Text;

    using DotNet.Data;

    public partial class ArticleManage : System.Web.UI.Page
    {
        public string Html { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            int categoryId = 2;

            //找出属性
            using (DataCommand cmd = DataCommandManager.GetDataCommand("MetaType"))
            {
                cmd.SetParameterValue("@CategoryId", categoryId);
                DataTable dt = cmd.ExecuteDataTable();
                StringBuilder sb=new StringBuilder();

                foreach (DataRow dataRow in dt.Rows)
                {
                    string name = dataRow["Name"].ToString();
                    string htmlContorlType = dataRow["HtmlContorlType"].ToString();
                    switch (htmlContorlType)
                    {
                        case "password":
                            sb.AppendFormat("  <input id=\"{0}\" type=\"password\" /><br/>", name);
                            break;
                        case "input":
                            sb.AppendFormat("  <input id=\"{0}\" type=\"text\" /><br/>", name);
                            break;
                        default:
                            break;
                    }
                }
                Html = sb.ToString();
            }
        }
    }
}