using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Data;

namespace DotNet.Base.DataAccess
{
    public class PermissionDataAccess:IPermissionDataAccess
    {
        public List<Permission> GetPermissions(string sqlWhere)
        {
            List<Permission> permissions=new List<Permission>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetPermission"))
            {
                if(!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText + " " + sqlWhere;
                }
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        Permission model=new Permission();
                        if (!Convert.IsDBNull(dr["PermissionId"]))
                        {
                            model.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        }
                        if (!Convert.IsDBNull(dr["Code"]))
                        {
                            model.Code = dr["Code"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Fullname"]))
                        {
                            model.Fullname =dr["Fullname"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["ParentId"]))
                        {
                            model.ParentId = dr["ParentId"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["SortCode"]))
                        {
                            model.SortCode = Convert.ToInt32(dr["SortCode"]) ;
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            model.Description = dr["Description"].ToString();
                        }
                        permissions.Add(model);
                    }
                }

            }
            return permissions;
        }

        public int Insert(Permission model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertPermission")) 
            {
                cmd.SetParameterValue("@PermissionId", model.PermissionId);
                cmd.SetParameterValue("@Code", model.Code);
                cmd.SetParameterValue("@Fullname", model.Fullname);
                cmd.SetParameterValue("@ParentId", model.ParentId);
                cmd.SetParameterValue("@SortCode", model.SortCode);
                cmd.SetParameterValue("@Description", model.Description);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
