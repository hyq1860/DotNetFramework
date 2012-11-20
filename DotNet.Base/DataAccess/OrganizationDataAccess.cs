using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Data;

namespace DotNet.Base.DataAccess
{
    public class OrganizationDataAccess : IOrganizationDataAccess
    {
        public int Insert(OrganizationInfo model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertOrganization"))
            {
                cmd.SetParameterValue("@OrganizationId", model.OrganizationId);
                cmd.SetParameterValue("@FullName", model.FullName);
                cmd.SetParameterValue("@ShortName", model.ShortName);
                cmd.SetParameterValue("@Code", model.Code);
                cmd.SetParameterValue("@ParentId", model.ParentId);
                cmd.SetParameterValue("@CorporateRepresentative", model.CorporateRepresentative);
                cmd.SetParameterValue("@Enabled", model.Enabled);
                cmd.SetParameterValue("@SortCode", model.SortCode);
                cmd.SetParameterValue("@Description", model.Description);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(OrganizationInfo model)
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrganizationInfo> GetAllOrganizations()
        {
            throw new NotImplementedException();
        }

        public List<OrganizationInfo> GetOrganizationByCondition(string sqlWhere)
        {
            List<OrganizationInfo> organizationInfos = new List<OrganizationInfo>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetOrganizations"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText + " " + sqlWhere;
                }
                
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        OrganizationInfo model = new OrganizationInfo();
                        if (!Convert.IsDBNull(dr["OrganizationId"]))
                        {
                            model.OrganizationId = Convert.ToInt32(dr["OrganizationId"]);
                        }
                        if (!Convert.IsDBNull(dr["FullName"]))
                        {
                            model.FullName = dr["FullName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["ShortName"]))
                        {
                            model.ShortName = dr["ShortName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Code"]))
                        {
                            model.Code = dr["Code"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["ParentId"]))
                        {
                            model.ParentId = Convert.ToInt32(dr["ParentId"]);
                        }
                        if (!Convert.IsDBNull(dr["CorporateRepresentative"]))
                        {
                            model.CorporateRepresentative = dr["CorporateRepresentative"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Enabled"]))
                        {
                            model.Enabled = (bool)dr["Enabled"];
                        }
                        if (!Convert.IsDBNull(dr["SortCode"]))
                        {
                            model.SortCode = Convert.ToInt32(dr["SortCode"]);
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            model.Description = dr["Description"].ToString();
                        }
                        organizationInfos.Add(model);
                    }
                }
            }
            return organizationInfos;
        }

        public OrganizationInfo GetOrganizationById(int id)
        {
            OrganizationInfo model = new OrganizationInfo();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetOrganizationById"))
            {
                cmd.SetParameterValue("@OrganizationId", id);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    if (!Convert.IsDBNull(dr["OrganizationId"]))
                    {
                        model.OrganizationId = Convert.ToInt32(dr["OrganizationId"]) ;
                    }
                    if (!Convert.IsDBNull(dr["FullName"]))
                    {
                        model.FullName = dr["FullName"].ToString();
                    }
                    if (!Convert.IsDBNull(dr["ShortName"]))
                    {
                        model.ShortName = dr["ShortName"].ToString();
                    }
                    if (!Convert.IsDBNull(dr["Code"]))
                    {
                        model.Code = dr["Code"].ToString();
                    }
                    if (!Convert.IsDBNull(dr["ParentId"]))
                    {
                        model.ParentId = Convert.ToInt32(dr["ParentId"]) ;
                    }
                    if (!Convert.IsDBNull(dr["CorporateRepresentative"]))
                    {
                        model.CorporateRepresentative = dr["CorporateRepresentative"].ToString();
                    }
                    if (!Convert.IsDBNull(dr["Enabled"]))
                    {
                        model.Enabled = (bool)dr["Enabled"];
                    }
                    if (!Convert.IsDBNull(dr["SortCode"]))
                    {
                        model.SortCode = Convert.ToInt32(dr["SortCode"]) ;
                    }
                    if (!Convert.IsDBNull(dr["Description"]))
                    {
                        model.Description = dr["Description"].ToString();
                    }
                }
            }
            return model;
        }
    }
}
