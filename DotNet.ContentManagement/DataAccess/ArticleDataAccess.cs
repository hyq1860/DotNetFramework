// -----------------------------------------------------------------------
// <copyright file="ArticleDataAccess.cs" company="">
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
    using DotNet.Common.Collections.PagedList;
    using DotNet.ContentManagement.Contract;
    using DotNet.ContentManagement.Contract.IDataAccess;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ArticleDataAccess:IArticleDataAccess
    {
        public int Insert(ArticleInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertArticle"))
            {
                model.Id = KeyGenerator.Instance.GetNextValue("Article");
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@CategoryId", model.CategoryId);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@SubTitle", model.SubTitle);
                cmd.SetParameterValue("@Summary", model.Summary);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                cmd.SetParameterValue("@Source", model.Source);
                cmd.SetParameterValue("@AllowComments", model.AllowComments);
                cmd.SetParameterValue("@Clicks", model.Clicks);
                cmd.SetParameterValue("@ReadPassword", model.ReadPassword);
                cmd.SetParameterValue("@UserId", model.UserId);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                cmd.SetParameterValue("@PublishDate", model.PublishDate);
                cmd.SetParameterValue("@InDate", model.InDate);
                cmd.SetParameterValue("@EditDate", model.EditDate);
                cmd.SetParameterValue("@FocusPicture", model.FocusPicture);
                cmd.SetParameterValue("@DataStatus", model.DataStatus);
                //var obj = cmd.ExecuteScalar();
                //if (obj != null)
                //{
                //    return Convert.ToInt32(obj);
                //}

                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(ArticleInfo model)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("UpdateArticle"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@CategoryId", model.CategoryId);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@SubTitle", model.SubTitle);
                cmd.SetParameterValue("@Summary", model.Summary);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Keywords", model.Keywords);
                cmd.SetParameterValue("@MetaDesc", model.MetaDesc);
                cmd.SetParameterValue("@Source", model.Source);
                cmd.SetParameterValue("@AllowComments", model.AllowComments);
                cmd.SetParameterValue("@Clicks", model.Clicks);
                cmd.SetParameterValue("@ReadPassword", model.ReadPassword);
                cmd.SetParameterValue("@UserId", model.UserId);
                cmd.SetParameterValue("@DisplayOrder", model.DisplayOrder);
                cmd.SetParameterValue("@PublishDate", model.PublishDate);
                cmd.SetParameterValue("@InDate", model.InDate);
                cmd.SetParameterValue("@EditDate", model.EditDate);
                cmd.SetParameterValue("@FocusPicture", model.FocusPicture);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("DeleteArticleById"))
            {
                cmd.SetParameterValue("@Id", id);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteByCategoryId(int categoryId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteByCategoryId"))
            {
                cmd.SetParameterValue("@CategoryId", categoryId);
                return cmd.ExecuteNonQuery();
            }
        }

        public PagedList<ArticleInfo> GetArticles(int pageSize, int pageIndex, string strWhere)
        {
            var result = new List<ArticleInfo>();
            var totalCount = 0;
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetArticlesByCondition"))
            {
                var sql = cmd.CommandText;
                if (!string.IsNullOrEmpty(strWhere))
                {
                    cmd.CommandText += "  and " + strWhere;
                    var temp = cmd.ExecuteScalar();
                    if(temp!=null)
                    {
                        totalCount = Convert.ToInt32(temp);
                    }
                    cmd.CommandText = sql;
                }

                if (string.IsNullOrEmpty(strWhere))
                {
                    cmd.CommandText += "  order by DisplayOrder desc limit " + (pageIndex - 1) * pageSize + "," + pageSize;
                }
                else
                {
                    cmd.CommandText += "  and " + strWhere + " order by DisplayOrder desc limit " + (pageIndex - 1) * pageSize + "," + pageSize;
                }

                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = GetModelFromDataReader(dr);
                        result.Add(model);
                    }
                }
            }
            
            return result.ToPagedList(pageIndex, pageSize, totalCount);
        }

        public ArticleInfo GetArticleById(int id)
        {
           
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetArticleById"))
            {
                cmd.SetParameterValue("@Id", id);
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    ArticleInfo model=null;
                    while (dr.Read())
                    {
                        model=new ArticleInfo();
                        model = this.GetModelFromDataReader(dr);
                        break;
                    }
                    return model;
                }
            }
            
        }

        private ArticleInfo GetModelFromDataReader(IDataReader dr)
        {
            var model = new ArticleInfo();
            model.Category=new ArticleCategoryInfo();
            if (!Convert.IsDBNull(dr["Id"]))
            {
                model.Id = Convert.ToInt32(dr["Id"]);
            }
            if (!Convert.IsDBNull(dr["CategoryId"]))
            {
                model.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                model.Category.Id = model.CategoryId;
            }
            if (!Convert.IsDBNull(dr["CategoryName"]))
            {
                model.Category.Title = dr["CategoryName"].ToString();
                model.Category.Name = dr["CategoryName"].ToString();
                model.CategoryName = model.Category.Name;
                
            }

            if (!Convert.IsDBNull(dr["Title"]))
            {
                model.Title = dr["Title"].ToString();
            }
            if (!Convert.IsDBNull(dr["SubTitle"]))
            {
                model.SubTitle = dr["SubTitle"].ToString();
            }
            if (!Convert.IsDBNull(dr["Summary"]))
            {
                model.Summary = dr["Summary"].ToString();
            }
            if (!Convert.IsDBNull(dr["Content"]))
            {
                model.Content = dr["Content"].ToString();
            }
            if (!Convert.IsDBNull(dr["Keywords"]))
            {
                model.Keywords = dr["Keywords"].ToString();
            }
            if (!Convert.IsDBNull(dr["MetaDesc"]))
            {
                model.MetaDesc = dr["MetaDesc"].ToString();
            }
            if (!Convert.IsDBNull(dr["Source"]))
            {
                model.Source = dr["Source"].ToString();
            }
            if (!Convert.IsDBNull(dr["AllowComments"]))
            {
                model.AllowComments = Convert.ToBoolean(dr["AllowComments"]);
            }
            if (!Convert.IsDBNull(dr["Clicks"]))
            {
                model.Clicks = Convert.ToInt32(dr["Clicks"]);
            }
            if (!Convert.IsDBNull(dr["ReadPassword"]))
            {
                model.ReadPassword = dr["ReadPassword"].ToString();
            }
            if (!Convert.IsDBNull(dr["UserId"]))
            {
                model.UserId = Convert.ToInt32(dr["UserId"]);
            }
            if (!Convert.IsDBNull(dr["DisplayOrder"]))
            {
                model.DisplayOrder = Convert.ToInt64(dr["DisplayOrder"]);
            }
            if (!Convert.IsDBNull(dr["PublishDate"]))
            {
                model.PublishDate = Convert.ToDateTime(dr["PublishDate"]);
            }
            if (!Convert.IsDBNull(dr["InDate"]))
            {
                model.InDate = Convert.ToDateTime(dr["InDate"]);
            }
            if (!Convert.IsDBNull(dr["EditDate"]))
            {
                model.EditDate = Convert.ToDateTime(dr["EditDate"]);
            }
            if (!Convert.IsDBNull(dr["FocusPicture"]))
            {
                model.FocusPicture = dr["FocusPicture"].ToString();
            }
            if (!Convert.IsDBNull(dr["DataStatus"]))
            {
                model.DataStatus = Convert.ToInt32(dr["DataStatus"]);
            }
            return model;
        }
    }
}
