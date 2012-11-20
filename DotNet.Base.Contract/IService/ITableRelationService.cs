using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// 表之间主外键关系
    /// </summary>
    public interface ITableRelationService
    {
        /// <summary>
        /// 用户表-角色表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        int InsertUser_Role(int userId, int roleId);

        int DeleteUser_RoleByUserId(int userId);

        int DeleteUser_RoleByRoleId(int roleId);

        int InsertRole_Permission(int roleId, int permission);

        int InsertUser_Organization(int userId, int oid);

        int SetRole_Permission(int roleId, int permission);

        int DeleteRole_PermissionByRoleId(int roleId);
    }
}
