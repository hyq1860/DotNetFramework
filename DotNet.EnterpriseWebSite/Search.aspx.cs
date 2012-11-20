using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite
{
    using DotNet.Search;

    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DotNet.Search.SearchEngine searchEngine = new SearchEngine(false);
          
            int resultCount = 0;
            if(!string.IsNullOrEmpty(TextBox1.Text))
            {
                searchEngine.Search(string.Empty, TextBox1.Text, 10, 1, out resultCount);
            }
            
        }
    }
}