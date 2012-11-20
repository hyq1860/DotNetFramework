// -----------------------------------------------------------------------
// <copyright file="BasicContentDataAccess.cs" company="">
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
    public class BasicContentDataAccess : IBasicContentDataAccess
    {
        public int Insert(BasicContentInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertBasicContent"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@Category", model.Category);
                cmd.SetParameterValue("@LinkPath", model.LinkPath);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Summary", model.Summary);
                
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(BasicContentInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateBasicContent"))
            {
                cmd.SetParameterValue("@Id", model.Id);
                cmd.SetParameterValue("@Title", model.Title);
                cmd.SetParameterValue("@Category", model.Category);
                cmd.SetParameterValue("@LinkPath", model.LinkPath);
                cmd.SetParameterValue("@Content", model.Content);
                cmd.SetParameterValue("@Summary", model.Summary);
                return cmd.ExecuteNonQuery();
            }
        }

        public BasicContentInfo GetBasicContentById(int id)
        {
            var model = new BasicContentInfo();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetBasicContentById"))
            {
                cmd.SetParameterValue("@Id", id);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while(dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["Id"]))
                        {
                            model.Id = Convert.ToInt32(dr["Id"]);
                        }
                        if (!Convert.IsDBNull(dr["Title"]))
                        {
                            model.Title = dr["Title"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Category"]))
                        {
                            model.Category = dr["Category"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["LinkPath"]))
                        {
                            model.LinkPath = dr["LinkPath"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Content"]))
                        {
                            model.Content = dr["Content"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Summary"]))
                        {
                            model.Summary = dr["Summary"].ToString();
                        }
                    }
                }
            }
            return model;
        }

        public List<BasicContentInfo> GetBasicContents()
        {
            var data = new List<BasicContentInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetBasicContents"))
            {
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        var model = new BasicContentInfo();
                        if (!Convert.IsDBNull(dr["Id"]))
                        {
                            model.Id = Convert.ToInt32(dr["Id"]);
                        }
                        if (!Convert.IsDBNull(dr["Title"]))
                        {
                            model.Title = dr["Title"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Category"]))
                        {
                            model.Category = dr["Category"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["LinkPath"]))
                        {
                            model.LinkPath = dr["LinkPath"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Content"]))
                        {
                            model.Content = dr["Content"].ToString();
                        }
                        if(!Convert.IsDBNull(dr["InDate"]))
                        {
                            model.InDate = Convert.ToDateTime(dr["InDate"]);
                        }
                        if (!Convert.IsDBNull(dr["Summary"]))
                        {
                            model.Content = dr["Summary"].ToString();
                        }
                        data.Add(model);
                    }
                }
            }
            return data;
        }
    }
}
