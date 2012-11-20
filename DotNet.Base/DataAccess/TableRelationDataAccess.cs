using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Data;

namespace DotNet.Base.DataAccess
{
    public class TableRelationDataAccess:ITableRelationDataAccess
    {
        public int InsertUser_Role(int userId, int roleId)
        {
            if (ExistUserRole(userId)>0)
            {
                int result = DeleteUser_RoleByUserId(userId);
                if(result>0)
                {
                    using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertBase_User_Role"))
                    {
                        cmd.SetParameterValue("@UserId", userId);
                        cmd.SetParameterValue("@RoleId", roleId);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertBase_User_Role"))
                {
                    cmd.SetParameterValue("@UserId", userId);
                    cmd.SetParameterValue("@RoleId", roleId);
                    return cmd.ExecuteNonQuery();
                }
            }
            return 0;
        }

        private int ExistUserRole(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("ExistsUser_Role"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteScalar<int>();
            }
        }

        public int DeleteUser_RoleByUserId(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteUser_RoleByUserId"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteUser_RoleByRoleId(int roleId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteUser_RoleByRoleId"))
            {
                cmd.SetParameterValue("@RoleId", roleId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertRole_Permission(int roleId, int permission)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertRole_Permission"))
            {
                cmd.SetParameterValue("@RoleId", roleId);
                cmd.SetParameterValue("@PermissionId", permission);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteRole_PermissionByRoleId(int roleId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteRole_PermissionByRoleId"))
            {
                cmd.SetParameterValue("@RoleId", roleId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int ExistsRole_Permission(int roleId, int permission)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("ExistsRole_Permission")) 
            {
                cmd.SetParameterValue("@RoleId", roleId);
                cmd.SetParameterValue("@PermissionId", permission);
                return cmd.ExecuteScalar<int>();
            }
        }

        public int SetRole_Permission(int roleId, int permission)
        {
            if(ExistsRole_Permission(roleId,permission)>0)
            {
                return 0;
            }
            return InsertRole_Permission(roleId, permission);
        }

        public int InsertUser_Organization(int userId, int oid)
        {
            if (ExistUser_Organization(userId)>0)
            {
                int result=DeleteUser_OrganizationByUserId(userId);
                if(result>0)
                {
                    using(DataCommand cmd=DataCommandManager.GetDataCommand("InsertUser_Organization"))
                    {
                        cmd.SetParameterValue("@UserId", userId);
                        cmd.SetParameterValue("@OrganizationId", oid);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertUser_Organization"))
            {
                cmd.SetParameterValue("@UserId", userId);
                cmd.SetParameterValue("@OrganizationId", oid);
                return cmd.ExecuteNonQuery();
            }
        }

        private int DeleteUser_OrganizationByUserId(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteUser_OrganizationByUserId"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteNonQuery();
            }
        }

        private int ExistUser_Organization(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("ExistsUser_Organization"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteScalar<int>();
            }
        }

        public int SetRelation(string tableName, KeyValuePair<string, object> keyValueA, KeyValuePair<string, object> keyValueB)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("SetRealation"))
            {
                cmd.SetParameterValue("@TableName", tableName);
                cmd.SetParameterValue("@" + keyValueA.Key, keyValueA.Value);
                cmd.SetParameterValue("@" + keyValueB.Key, keyValueB.Value);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
