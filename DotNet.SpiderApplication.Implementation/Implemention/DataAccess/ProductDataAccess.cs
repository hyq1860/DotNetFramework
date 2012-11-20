// -----------------------------------------------------------------------
// <copyright file="ProductDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Data;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductDataAccess:IProductDataAccess
    {
        public List<ProductInfo> GetProducts(string sqlWhere)
        {
            var data = new List<ProductInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText += sqlWhere;
                }

                using (var dataReader = cmd.ExecuteDataReader())
                {
                    while (dataReader.Read())
                    {
                        var product = new ProductInfo();
                        if (!Convert.IsDBNull(dataReader["ProductId"]))
                        {
                            product.ProductId = dataReader["ProductId"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["Name"]))
                        {
                            product.Name = dataReader["Name"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["Url"]))
                        {
                            product.Url = dataReader["Url"].ToString();
                        }

                        data.Add(product);
                    }
                }
            }

            return data;
        }
    }
}
