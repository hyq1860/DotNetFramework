using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.EnterpriseWebSite
{
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Service;

    public partial class ProductListV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private List<ProductInfo> products;

        public List<ProductInfo> Products
        {
            get
            {
                if(products==null)
                {
                    IProductService productService = new ProductService();

                    products= productService.GetProducts(20, 1, "IsFocusPicture=0").Where(item => !string.IsNullOrEmpty(item.MediumPicture) && item.Category.DataStatus == 1).ToList();
                    
                }
                return products;
            }
        }
    }
}