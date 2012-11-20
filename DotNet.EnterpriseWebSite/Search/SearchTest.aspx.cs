using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Search;

namespace DotNet.EnterpriseWebSite.Search
{
    public partial class SearchTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var path = @"D:\SkyDrive\文档\PanGu4Lucene_V2.3.1.0\PanGu4Lucene\WebDemo\Bin\NewsIndex";
            SearchBase search = new SearchBase(path);
            
            int count = 0;
            
            search.IndexFilePath = path;
            search.Search(path, TextBox1.Text, 1, 1, out count);
        }
    }
}