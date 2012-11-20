using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Data;

namespace DotNet.Base
{
    public class RoleDataAccess:IRoleDataAccess
    {
        #region##新增Base_Role

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Role model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertRole"))
            {
                cmd.SetParameterValue("@RoleId", KeyGenerator.Instance.GetNextValue("Role"));
                cmd.SetParameterValue("@RoleName", model.RoleName);
                cmd.SetParameterValue("@SortCode", model.SortCode);
                cmd.SetParameterValue("@Enabled", model.Enabled);
                cmd.SetParameterValue("@Description", model.Description);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Role> GetAllRoles(string sqlWhere)
        {
            List<Role> roles = new List<Role>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetAllRoles"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText + " " + sqlWhere;
                }
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        Role role = new Role();
                        
                        if (!Convert.IsDBNull(dr["RoleId"]))
                        {
                            role.RoleId = Convert.ToInt32(dr["RoleId"]);
                        }
                        if (!Convert.IsDBNull(dr["RoleName"]))
                        {
                            role.RoleName = dr["RoleName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            role.Description = dr["Description"].ToString();
                        }
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        #endregion


        #region##通过主键ID得到Base_Role实体

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public Role GetRoleByRoleId(int roleid)
        {
            Role model = new Role();
            //string sql = "select * from Base_Role where RoleId = @PK";
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetRoleIdByRoleId"))
            {
                cmd.SetParameterValue("@RoleId", roleid);
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["RoleId"]))
                        {
                            model.RoleId = (int)dr["RoleId"];
                        }
                        if (!Convert.IsDBNull(dr["RoleName"]))
                        {
                            model.RoleName = (string)dr["RoleName"];
                        }
                        if (!Convert.IsDBNull(dr["SortCode"]))
                        {
                            model.SortCode = (int)dr["SortCode"];
                        }
                        if (!Convert.IsDBNull(dr["Enabled"]))
                        {
                            model.Enabled = (bool)dr["Enabled"];
                        }
                        if (!Convert.IsDBNull(dr["Description"]))
                        {
                            model.Description = (string)dr["Description"];
                        }
                    }
                }
            }
            return model;
        }
        #endregion

        #region##通过主键ID删除Base_Role
        /// <summary>
        /// 功能：通过主键ID删除Base_Role
        /// 创建人：  Wilson 
        /// 创建时间：2011/11/6   
        /// </summary>
        /// <param name="">主键ID</param>
        /// <returns></returns>
        public int DeleteByRoleId(int roleid)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteRoleByRoleId"))
            {
                cmd.SetParameterValue("@RoleId", roleid);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Role model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateRole"))
            {
                cmd.SetParameterValue("@RoleName", model.RoleName);
                cmd.SetParameterValue("@SortCode", model.SortCode);
                cmd.SetParameterValue("@Enabled", model.Enabled);
                cmd.SetParameterValue("@Description", model.Description);
                cmd.SetParameterValue("@RoleId", model.RoleId);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<RoleExtension> GetRoleExtensions()
        {
            List<RoleExtension> data=new List<RoleExtension>();
            using(DataCommand cmd=DataCommandManager.GetDataCommand("GetRolePermission"))
            {
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        RoleExtension model=new RoleExtension();
                        if (!Convert.IsDBNull(dr["RoleId"]))
                        {
                            model.RoleId = Convert.ToInt32(dr["RoleId"]);
                        }
                        if (!Convert.IsDBNull(dr["RoleName"])) 
                        {
                            model.RoleName = dr["RoleName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["PermissionId"]))
                        {
                            model.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        }
                        if (!Convert.IsDBNull(dr["Fullname"])) 
                        {
                            model.PermissionName = dr["Fullname"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Code"])) 
                        {
                            model.PermissionCode = dr["Code"].ToString();
                        }
                        data.Add(model);
                    }
                }
            }
            return data;
        }

        public RoleExtension GetRoleExtensionByRoleId(int roleId)
        {
            RoleExtension model = new RoleExtension();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetRolePermissionByRoleId"))
            {
                cmd.SetParameterValue("@RoleId", roleId);
                using (IDataReader dr = cmd.ExecuteDataReader()) 
                {
                    while (dr.Read()) 
                    {
                        if (Convert.IsDBNull(dr["RoleId"])) 
                        {
                            model.RoleId = Convert.ToInt32(dr["RoleId"]);
                        }
                        if (Convert.IsDBNull(dr["RoleName"])) 
                        {
                            model.RoleName = dr["RoleName"].ToString();
                        }
                        if (Convert.IsDBNull(dr["PermissionId"])) {
                            model.PermissionId = Convert.ToInt32(dr["PermissionId"]);
                        }
                        if (Convert.IsDBNull(dr["Fullname"])) {
                            model.PermissionName = dr["Fullname"].ToString();
                        }
                        if (Convert.IsDBNull(dr["Code"])) {
                            model.PermissionCode = dr["Code"].ToString();
                        }
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获取所有觉得的权限
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<string>> GetRolePermissions()
        {
            List<RoleExtension> roleExtensions = GetRoleExtensions();
            Dictionary<int,List<string>> data=new Dictionary<int, List<string>>();
            foreach (RoleExtension item in roleExtensions)
            {
                if(data.ContainsKey(item.RoleId))
                {
                    if(data[item.RoleId]!=null)
                    {
                        data[item.RoleId].Add(item.PermissionCode);
                    }
                    else
                    {
                        data[item.RoleId] = new List<string>() { item.PermissionCode };
                    }
                }
                else
                {
                    data.Add(item.RoleId,new List<string>(){item.PermissionCode});
                }
            }
            return data;
        }

        #endregion	
    }
}
