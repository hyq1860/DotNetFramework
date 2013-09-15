using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite.Manage
{
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Contract.IService;
    using DotNet.ContentManagement.Service;

    public partial class ProductCategoryEditForm : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected ProductCategoryInfo ProductCategory
        {
            get
            {
                IProductCategoryService  productCategoryService=new ProductCategoryService();
                return productCategoryService.GetProductCategoryById(int.Parse(Request.QueryString["id"]));
            }
            
        }
    }
}