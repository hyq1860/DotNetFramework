using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Data;

namespace DotNet.WebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("TestSqlite"))
            {
                DataTable dt = cmd.ExecuteDataTable();
            }
        }
    }
}