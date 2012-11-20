// -----------------------------------------------------------------------
// <copyright file="SiteOptionDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract;
    using DotNet.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteOptionDataAccess : ISiteOptionDataAccess
    {
        public int Add(SiteOptionInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertSiteOption"))
            {
                cmd.SetParameterValue("@OptionId", model.OptionId);
                cmd.SetParameterValue("@SiteId", model.SiteId);
                cmd.SetParameterValue("@OptionName", model.OptionName);
                cmd.SetParameterValue("@OptionKey", model.OptionKey);
                cmd.SetParameterValue("@OptionValue", model.OptionValue);
                cmd.SetParameterValue("@HtmlType", model.HtmlType);
                cmd.SetParameterValue("@AutoLoad", model.AutoLoad);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<SiteOptionInfo> GetSiteOption()
        {
            var list = new List<SiteOptionInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetSiteOptions"))
            {
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        SiteOptionInfo model=new SiteOptionInfo();
                        if (!Convert.IsDBNull(dr["OptionId"]))
                        {
                            model.OptionId = (int)dr["OptionId"];
                        }
                        if (!Convert.IsDBNull(dr["SiteId"]))
                        {
                            model.SiteId = (int)dr["SiteId"];
                        }
                        if (!Convert.IsDBNull(dr["OptionName"]))
                        {
                            model.OptionName = (string)dr["OptionName"];
                        }
                        if (!Convert.IsDBNull(dr["OptionKey"]))
                        {
                            model.OptionKey = (string)dr["OptionKey"];
                        }
                        if (!Convert.IsDBNull(dr["OptionValue"]))
                        {
                            model.OptionValue = (string)dr["OptionValue"];
                        }
                        if (!Convert.IsDBNull(dr["OptionGroup"]))
                        {
                            model.OptionGroup = (string)dr["OptionGroup"];
                        }
                        if (!Convert.IsDBNull(dr["HtmlType"]))
                        {
                            model.HtmlType = (int)dr["HtmlType"];
                        }
                        if (!Convert.IsDBNull(dr["AutoLoad"]))
                        {
                            model.AutoLoad = (string)dr["AutoLoad"];
                        }
                        if (!Convert.IsDBNull(dr["Editor"]))
                        {
                            model.Editor = (string)dr["Editor"];
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public int Update(SiteOptionInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateSiteOption"))
            {
                cmd.SetParameterValue("@OptionId", model.OptionId);
                cmd.SetParameterValue("@OptionValue", model.OptionValue);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
