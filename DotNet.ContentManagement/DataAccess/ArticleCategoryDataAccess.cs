// -----------------------------------------------------------------------
// <copyright file="ArticleCategoryDataAccess.cs" company="">
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

    using DotNet.Base;
    using DotNet.Common.Collections;
    using DotNet.ContentManagement.Contract;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleCategoryDataAccess:IArticleCategoryDataAccess
    {
        public int Insert(ArticleCategoryInfo model)
        {
            var parent = this.GetArticleCategoryById(model.ParentId);
            model.Id = KeyGenerator.Instance.GetNextValue("ArticleCategory");
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertArticleCategory"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@Name", model.Name);
                cmd.SetParameterValue("@UrlPath", model.UrlPath);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                cmd.SetParameterValue("@ParentId", model.ParentId);

                cmd.SetParameterValue("@MyLeft", parent.Lft);

                cmd.SetParameterValue("@Description", model.Description);
                cmd.SetParameterValue("@InUserId", model.InUserId);
                cmd.SetParameterValue("@InDate", model.InDate);
                cmd.SetParameterValue("@EditDate", model.EditDate);
                cmd.SetParameterValue("@EditUserId", model.EditUserId);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                cmd.SetParameterValue("@DataStatus", model.DataStatus);

                cmd.SetParameterValue("@Type", model.CategoryType);

                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(ArticleCategoryInfo model)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("UpdateArticleCategoryById"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@Name", model.Name);
                cmd.SetParameterValue("@UrlPath", model.UrlPath);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                
                cmd.SetParameterValue("@Description", model.Description);
               
                cmd.SetParameterValue("@EditDate", model.EditDate);
                cmd.SetParameterValue("@EditUserId", model.EditUserId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            var articleCategory = this.GetArticleCategoryById(id);

            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteArticleCategoryById"))
            {
                cmd.SetParameterValue("@MyLeft", articleCategory.Lft);
                cmd.SetParameterValue("@MyRight", articleCategory.Lft);
                cmd.SetParameterValue("@MyWidth", articleCategory.Rgt - articleCategory.Lft + 1);
                return cmd.ExecuteNonQuery();
            }
        }

        public PagedList<ArticleCategoryInfo> GetArticleCategory(int pageSize, int pageIndex, string strWhere)
        {
            throw new NotImplementedException();
        }

        public ArticleCategoryInfo GetArticleCategoryById(int id)
        {
            var model = new ArticleCategoryInfo();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetArticleCategoryById"))
            {
                cmd.SetParameterValue("@Id", id);
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        model = this.GetModelFromDataReader(dr);
                    }
                }
            }
            return model;
        }

        private ArticleCategoryInfo GetModelFromDataReader(IDataReader dr)
        {
            var model = new ArticleCategoryInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                model.Id = Convert.ToInt32(dr["Id"]);
            }
            if (!Convert.IsDBNull(dr["Title"]))
            {
                model.Title = dr["Title"].ToString();
            }
            if (!Convert.IsDBNull(dr["Name"]))
            {
                model.Name = dr["Name"].ToString();
            }
            if (!Convert.IsDBNull(dr["UrlPath"]))
            {
                model.UrlPath = dr["UrlPath"].ToString();
            }
            if (!Convert.IsDBNull(dr["Keywords"]))
            {
                model.Keywords = dr["Keywords"].ToString();
            }
            if (!Convert.IsDBNull(dr["MetaDesc"]))
            {
                model.MetaDesc = dr["MetaDesc"].ToString();
            }
            if (!Convert.IsDBNull(dr["ParentId"]))
            {
                model.ParentId = Convert.ToInt32(dr["ParentId"]);
            }
            if (!Convert.IsDBNull(dr["Lft"]))
            {
                model.Lft = Convert.ToInt32(dr["Lft"]);
            }
            if (!Convert.IsDBNull(dr["Rgt"]))
            {
                model.Rgt = Convert.ToInt32(dr["Rgt"]);
            }
            if (!Convert.IsDBNull(dr["Depth"]))
            {
                model.Depth = Convert.ToInt32(dr["Depth"]);
            }
            if (!Convert.IsDBNull(dr["Description"]))
            {
                model.Description = dr["Description"].ToString();
            }
            if (!Convert.IsDBNull(dr["InUserId"]))
            {
                model.InUserId = Convert.ToInt32(dr["InUserId"]);
            }
            if (!Convert.IsDBNull(dr["InDate"]))
            {
                model.InDate = Convert.ToDateTime(dr["InDate"]);
            }
            if (!Convert.IsDBNull(dr["EditDate"]))
            {
                model.EditDate = Convert.ToDateTime(dr["EditDate"]);
            }
            if (!Convert.IsDBNull(dr["EditUserId"]))
            {
                model.EditUserId = Convert.ToInt32(dr["EditUserId"]);
            }
            if (!Convert.IsDBNull(dr["DisplayOrder"]))
            {
                model.DisplayOrder = Convert.ToInt64(dr["DisplayOrder"]);
            }
            if (!Convert.IsDBNull(dr["DataStatus"]))
            {
                model.DataStatus = Convert.ToInt32(dr["DataStatus"]);
            }
            if (!Convert.IsDBNull(dr["Type"]))
            {
                model.CategoryType = Convert.ToInt32(dr["Type"]);
            }
            return model;
        }

        public List<ArticleCategoryInfo> GetAllArticleCategory()
        {
            var result = new List<ArticleCategoryInfo>();
            using(DataCommand cmd=DataCommandManager.GetDataCommand("GetAllArticleCategory"))
            {
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = this.GetModelFromDataReader(dr);
                        result.Add(model);
                    }
                }
            }
            return result;
        }
    }
}
