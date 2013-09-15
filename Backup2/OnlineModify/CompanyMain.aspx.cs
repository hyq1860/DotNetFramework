using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;

namespace yujiajun.Admin.CompanyFileManage
{
    public partial class CompanyMain : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string a = Request.QueryString["path"];
                using (StreamReader sr = new StreamReader(a))
                {
                    txtContent.Value = sr.ReadToEnd();
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string path = Request.QueryString["path"];
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(txtContent.Value);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('修改成功')", true);
        }
    }
}
