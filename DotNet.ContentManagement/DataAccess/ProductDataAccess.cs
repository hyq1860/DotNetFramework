// -----------------------------------------------------------------------
// <copyright file="ProductDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductDataAccess:IProductDataAccess
    {
        public int Add(ProductInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProduct"))
            {
                cmd.SetParameterValue("@id", model.Id);
                cmd.SetParameterValue("@title", model.Title);
                cmd.SetParameterValue("@cid", model.CategoryId);
                cmd.SetParameterValue("@price", model.Price);
                cmd.SetParameterValue("@desc", model.Desc);
                cmd.SetParameterValue("@content", model.Content);
                
                cmd.SetParameterValue("@isonline", model.IsOnline);
                cmd.SetParameterValue("@bigpicture", model.BigPicture);
                cmd.SetParameterValue("@smallpicture", model.SmallPicture);
                cmd.SetParameterValue("@mediumpicture", model.MediumPicture);
                cmd.SetParameterValue("@specifications", model.Specifications);
                cmd.SetParameterValue("@afterservice", model.AfterService);
                cmd.SetParameterValue("@UserId", model.UserId);
                cmd.SetParameterValue("@InDate", model.InDate);
                cmd.SetParameterValue("@FileUrl", model.FileUrl);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(ProductInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateProductById"))
            {
                cmd.SetParameterValue("@id", model.Id);
                cmd.SetParameterValue("@title", model.Title);
                cmd.SetParameterValue("@cid", model.CategoryId);
                cmd.SetParameterValue("@price", model.Price);
                cmd.SetParameterValue("@desc", model.Desc);
                cmd.SetParameterValue("@content", model.Content);
                cmd.SetParameterValue("@isonline", model.IsOnline);
                cmd.SetParameterValue("@bigpicture", model.BigPicture);
                cmd.SetParameterValue("@smallpicture", model.SmallPicture);
                cmd.SetParameterValue("@mediumpicture", model.MediumPicture);
                cmd.SetParameterValue("@specifications", model.Specifications);
                cmd.SetParameterValue("@afterservice", model.AfterService);
                cmd.SetParameterValue("@FileUrl", model.FileUrl);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("DeleteProductById"))
            {
                cmd.SetParameterValue("@id", id);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 根据产品分类删除产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int DeleteProductByCategoryId(int categoryId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteProductByCategoryId"))
            {
                cmd.SetParameterValue("@CategoryId", categoryId);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<ProductInfo> GetList(string strWhere)
        {
            throw new NotImplementedException();
        }

        public int GetProductCount(string strWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductCount"))
            {
                if(!string.IsNullOrEmpty(strWhere))
                {
                    cmd.CommandText += strWhere;
                }
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public ProductInfo GetProductById(int id)
        {
            ProductInfo model = new ProductInfo();
            model.Category = new ProductCategoryInfo();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductById"))
            {
                cmd.SetParameterValue("@id", id);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["id"]))
                        {
                            model.Id = Convert.ToInt32(dr["id"]);
                        }
                        if (!Convert.IsDBNull(dr["title"]))
                        {
                            model.Title = dr["title"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["cid"]))
                        {
                            model.CategoryId = (int)dr["cid"];
                            model.Category.Id = Convert.ToInt32(dr["cid"]);
                        }
                        if (!Convert.IsDBNull(dr["price"]))
                        {
                            model.Price = (double)dr["Price"];
                        }
                        if (!Convert.IsDBNull(dr["desc"]))
                        {
                            model.Desc = dr["desc"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["content"]))
                        {
                            model.Content = dr["content"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["isonline"]))
                        {
                            model.IsOnline = (bool)dr["IsOnline"];
                        }
                        if (!Convert.IsDBNull(dr["bigpicture"]))
                        {
                            model.BigPicture = dr["BigPicture"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["smallpicture"]))
                        {
                            model.SmallPicture = dr["SmallPicture"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["mediumpicture"]))
                        {
                            model.MediumPicture = dr["MediumPicture"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["specifications"]))
                        {
                            model.Specifications = dr["Specifications"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["afterService"]))
                        {
                            model.AfterService = dr["afterService"].ToString();
                        }

                        if(!Convert.IsDBNull(dr["CategoryName"]))
                        {
                            model.CategoryName = dr["CategoryName"].ToString();
                        }
                        if(!Convert.IsDBNull(dr["InDate"]))
                        {
                            model.InDate = Convert.ToDateTime(dr["Indate"]);
                        }
                        if (!Convert.IsDBNull(dr["FileUrl"]))
                        {
                            model.FileUrl = dr["FileUrl"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["IsFocusPicture"]))
                        {
                            model.IsFocusPicture = Convert.ToBoolean(dr["IsFocusPicture"]);
                        }
                        if (!Convert.IsDBNull(dr["Keywords"]))
                        {
                            model.Keywords = dr["Keywords"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["MetaDesc"]))
                        {
                            model.MetaDesc = dr["MetaDesc"].ToString();
                        }
                        if(!Convert.IsDBNull(dr["DataStatus"]))
                        {
                            model.Category.DataStatus = Convert.ToInt32(dr["DataStatus"]);
                        }
                    }
                    
                }
            }
            return model;
        }

        public List<ProductInfo> GetProducts(int pageSize, int pageIndex, string where)
        {
            var data = new List<ProductInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                if(string.IsNullOrEmpty(where))
                {
                    cmd.CommandText += "  order by DisplayOrder desc limit " + (pageIndex - 1) * pageSize + "," + pageSize;
                }
                else
                {
                    cmd.CommandText += "  and " + where + " order by DisplayOrder desc limit " + (pageIndex - 1) * pageSize + "," + pageSize;
                }
               
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = GetModelFromDataReader(dr);
                        data.Add(model);
                    }
                }
            }
            return data;
        }

        public List<ProductInfo> GetProductsByCategoryId(int categoryId)
        {
            var data = new List<ProductInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductsByCategoryId"))
            {
                cmd.SetParameterValue("@categoryId", categoryId);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = GetModelFromDataReader(dr);
                        data.Add(model);
                    }
                }
            }
            return data;
        }

        private ProductInfo GetModelFromDataReader(IDataReader dr)
        {
            var model = new ProductInfo();
            model.Category=new ProductCategoryInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                model.Id = (int)dr["Id"];
            }
            if (!Convert.IsDBNull(dr["Title"]))
            {
                model.Title = (string)dr["Title"];
            }
            if (!Convert.IsDBNull(dr["CategoryName"]))
            {
                model.CategoryName = Convert.ToString(dr["CategoryName"]);
            }
            if (!Convert.IsDBNull(dr["CId"]))
            {
                model.CategoryId = (int)dr["CId"];
            }
            if (!Convert.IsDBNull(dr["Price"]))
            {
                model.Price = (double)dr["Price"];
            }
            if (!Convert.IsDBNull(dr["Desc"]))
            {
                model.Desc = (string)dr["Desc"];
            }
            if (!Convert.IsDBNull(dr["content"]))
            {
                model.Content = dr["content"].ToString();
            }
            if (!Convert.IsDBNull(dr["IsOnline"]))
            {
                model.IsOnline = (bool)dr["IsOnline"];
            }
            if (!Convert.IsDBNull(dr["BigPicture"]))
            {
                model.BigPicture = (string)dr["BigPicture"];
            }
            if (!Convert.IsDBNull(dr["SmallPicture"]))
            {
                model.SmallPicture = (string)dr["SmallPicture"];
            }
            if (!Convert.IsDBNull(dr["MediumPicture"]))
            {
                model.MediumPicture = (string)dr["MediumPicture"];
            }
            if (!Convert.IsDBNull(dr["Specifications"]))
            {
                model.Specifications = (string)dr["Specifications"];
            }
            if (!Convert.IsDBNull(dr["AfterService"]))
            {
                model.AfterService = (string)dr["AfterService"];
            }
            if (!Convert.IsDBNull(dr["InDate"]))
            {
                model.InDate = Convert.ToDateTime(dr["Indate"]);
            }
            if (!Convert.IsDBNull(dr["FileUrl"]))
            {
                model.FileUrl = dr["FileUrl"].ToString();
            }
            if (!Convert.IsDBNull(dr["IsFocusPicture"]))
            {
                model.IsFocusPicture = Convert.ToBoolean(dr["IsFocusPicture"]);
            }
            if (!Convert.IsDBNull(dr["Keywords"]))
            {
                model.Keywords = dr["Keywords"].ToString();
            }
            if (!Convert.IsDBNull(dr["MetaDesc"]))
            {
                model.MetaDesc = dr["MetaDesc"].ToString();
            }
            if (!Convert.IsDBNull(dr["DataStatus"]))
            {
                model.Category.DataStatus = Convert.ToInt32(dr["DataStatus"]);
            }
            if (!Convert.IsDBNull(dr["DisplayOrder"]))
            {
                model.DisplayOrder = Convert.ToInt64(dr["DisplayOrder"]);
            }
            return model;
        }
    }
}
