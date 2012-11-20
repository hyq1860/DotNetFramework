// -----------------------------------------------------------------------
// <copyright file="ProductCategoryDataAccess.cs" company="">
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

    using DotNet.ContentManagement.Contract.Entity;
    using DotNet.ContentManagement.Contract.IDataAccess;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductCategoryDataAccess : IProductCategoryDataAccess
    {
        public List<ProductCategoryInfo> GetProductCategory()
        {
            var data = new List<ProductCategoryInfo>();
            using(DataCommand cmd=DataCommandManager.GetDataCommand("GetProductCategory"))
            {
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = new ProductCategoryInfo();
                        if (!Convert.IsDBNull(dr["Id"]))
                        {
                            model.Id = (int)dr["Id"];
                            model.CategoryId = model.Id;
                        }
                        if (!Convert.IsDBNull(dr["Name"]))
                        {
                            model.Name = (string)dr["Name"];
                        }
                        if (!Convert.IsDBNull(dr["Depth"]))
                        {
                            model.Depth = Convert.ToInt32(dr["Depth"]);
                        }
                        if (!Convert.IsDBNull(dr["Lft"]))
                        {
                            model.Lft = Convert.ToInt32(dr["Lft"]);
                        }
                        if (!Convert.IsDBNull(dr["Rgt"]))
                        {
                            model.Rgt = Convert.ToInt32(dr["Rgt"]);
                        }
                        if (model.Lft + 1 == model.Rgt)
                        {
                            model.IsLeaf = true;
                        }
                        if(!Convert.IsDBNull(dr["Content"]))
                        {
                            model.Content = Convert.ToString(dr["Content"]);
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            model.Desc = Convert.ToString(dr["Description"]);
                        }
                        if (!Convert.IsDBNull(dr["MediumPicture"]))
                        {
                            model.MediumPicture = Convert.ToString(dr["MediumPicture"]);
                        }
                        if(!Convert.IsDBNull(dr["ParentId"]))
                        {
                            model.ParentId = Convert.ToInt32(dr["ParentId"]);
                        }
                        if(!Convert.IsDBNull(dr["DataStatus"]))
                        {
                            model.DataStatus = Convert.ToInt32(dr["DataStatus"]);
                        }
                        data.Add(model);
                    }
                }
            }
            return data;
        }

        public int Insert(ProductCategoryInfo model,int parentId)
        {
            var parent = this.GetProductCategoryById(model.ParentId);
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProductCategory"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@Title", model.Name);
                cmd.SetParameterValue("@Name", model.Name);
                cmd.SetParameterValue("@MyLeft", parent.Lft);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Desc", model.Desc);
                cmd.SetParameterValue("@SmallPicture", model.SmallPicture);
                cmd.SetParameterValue("@MediumPicture", model.MediumPicture);
                cmd.SetParameterValue("@BigPicture", model.BigPicture);
                cmd.SetParameterValue("@ParentId", model.ParentId);
                cmd.SetParameterValue("@DataStatus", model.DataStatus);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                return cmd.ExecuteNonQuery();
            }
        }

        public ProductCategoryInfo GetProductCategoryById(int id)
        {
            var model = new ProductCategoryInfo();
            using(DataCommand cmd=DataCommandManager.GetDataCommand("GetProductCategoryById"))
            {
                cmd.SetParameterValue("@Id", id);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["Id"]))
                        {
                            model.Id = (int)dr["Id"];
                        }
                        if (!Convert.IsDBNull(dr["Name"]))
                        {
                            model.Name = (string)dr["Name"];
                        }
                        if (!Convert.IsDBNull(dr["Lft"]))
                        {
                            model.Lft = Convert.ToInt32(dr["Lft"]);
                        }
                        if (!Convert.IsDBNull(dr["Rgt"]))
                        {
                            model.Rgt = Convert.ToInt32(dr["Rgt"]);
                        }
                        if (!Convert.IsDBNull(dr["ParentId"]))
                        {
                            model.ParentId = Convert.ToInt32(dr["ParentId"]);
                        }
                        if (!Convert.IsDBNull(dr["Content"]))
                        {
                            model.Content = Convert.ToString(dr["Content"]);
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            model.Desc = Convert.ToString(dr["Description"]);
                        }
                        if (!Convert.IsDBNull(dr["MediumPicture"]))
                        {
                            model.MediumPicture = Convert.ToString(dr["MediumPicture"]);
                        }
                        if (!Convert.IsDBNull(dr["DataStatus"]))
                        {
                            model.DataStatus = Convert.ToInt32(dr["DataStatus"]);
                        }
                        return model;
                    }
                }
            }
            return model;
        }

        public int DeleteById(int id)
        {
            var productCategory = this.GetProductCategoryById(id);

            using(DataCommand cmd=DataCommandManager.GetDataCommand("DeleteProductCategoryById"))
            {
                cmd.SetParameterValue("@MyLeft", productCategory.Lft);
                cmd.SetParameterValue("@MyRight", productCategory.Lft);
                cmd.SetParameterValue("@MyWidth", productCategory.Rgt - productCategory.Lft + 1);
                return cmd.ExecuteNonQuery();
            }

        }

        //删除该节点，而不删除该节点的子节点
        public int DeleteCurrentNodeOnlyById(int id)
        {
            var productCategory = this.GetProductCategoryById(id);

            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteCurrentNodeOnlyById"))
            {
                cmd.SetParameterValue("@MyLeft", productCategory.Lft);
                cmd.SetParameterValue("@MyRight", productCategory.Lft);
                cmd.SetParameterValue("@MyWidth", productCategory.Rgt - productCategory.Lft + 1);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(ProductCategoryInfo model)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("UpdateProductCategoryByCategoryId"))
            {
                cmd.SetParameterValue("@CategoryId", model.Id);
                cmd.SetParameterValue("@Title", model.Name);
                cmd.SetParameterValue("@Name", model.Name);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Desc", model.Desc);
                cmd.SetParameterValue("@SmallPicture", model.SmallPicture);
                cmd.SetParameterValue("@MediumPicture", model.MediumPicture);
                cmd.SetParameterValue("@BigPicture", model.BigPicture);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
