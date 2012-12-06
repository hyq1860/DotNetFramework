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

    using DotNet.Common;
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

                        if (!Convert.IsDBNull(dataReader["Supplier"]))
                        {
                            product.ECPlatformId = Convert.ToInt32(dataReader["Supplier"]);
                        }

                        data.Add(product);
                    }
                }
            }

            return data;
        }

        public bool Insert(ProductInfo product)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProduct"))
            {
                cmd.SetParameterValue("@ProductId", PrimaryKeyGenerator.NewComb().ToString().Replace("-", ""));
                cmd.SetParameterValue("@Name", product.Name);
                cmd.SetParameterValue("@Url", product.Url);
                cmd.SetParameterValue("@ListImage", 0);
                cmd.SetParameterValue("@CategoryId", 0);
                cmd.SetParameterValue("@Supplier", 1);
                //cmd.SetParameterValue("@Inventory", );
                cmd.SetParameterValue("@CommentNumber", 0);
                cmd.SetParameterValue("@InDate", DateTime.Now);
                cmd.SetParameterValue("@EditDate", DateTime.Now);
                return cmd.ExecuteNonQuery()>0;
            }
        }

        public bool Update(ProductInfo product)
        {

            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateProduct"))
            {
                cmd.SetParameterValue("@ProductId", product.ProductId);
                cmd.SetParameterValue("@Name", product.Name);
                //cmd.SetParameterValue("@Price", price);
                //cmd.SetParameterValue("@IsProcess", isProcess);
                //cmd.SetParameterValue("@Elapse", );
                cmd.SetParameterValue("@EditDate", DateTime.Now);
                cmd.SetParameterValue("@Source", product.Source);
                return cmd.ExecuteNonQuery()>0;
            }
        }
    }
}
