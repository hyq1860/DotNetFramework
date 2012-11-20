using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 表之间主外键关系
    /// </summary>
    public  interface ITableRelationDataAccess
    {
        int InsertUser_Role(int userId,int roleId);

        int DeleteUser_RoleByUserId(int userId);

        int DeleteUser_RoleByRoleId(int roleId);

        int InsertRole_Permission(int roleId, int permission);

        int DeleteRole_PermissionByRoleId(int roleId);

        /// <summary>
        /// 添加用户部门关系
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        int InsertUser_Organization(int userId, int oid);

        int ExistsRole_Permission(int roleId, int permission);

        int SetRole_Permission(int roleId, int permission);
    }
}
