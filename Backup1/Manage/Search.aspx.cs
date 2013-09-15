using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.Search;

    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DotNet.Search.SearchEngine searchEngine=new SearchEngine(true);
            searchEngine.CreateIndex();
            int resultCount = 0;
            searchEngine.Search(string.Empty, "神舟", 10, 1, out resultCount);
        }
    }
}