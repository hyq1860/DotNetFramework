using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Base.DataAccess;

namespace DotNet.Base.Service
{
    public class TableRelationService:ITableRelationService
    {
        private static ITableRelationDataAccess _dataAccess=new TableRelationDataAccess();
        public int InsertUser_Role(int userId, int roleId)
        {
            return _dataAccess.InsertUser_Role(userId, roleId);
        }

        public int DeleteUser_RoleByUserId(int userId)
        {
            return _dataAccess.DeleteUser_RoleByUserId(userId);
        }

        public int DeleteUser_RoleByRoleId(int roleId)
        {
            return _dataAccess.DeleteUser_RoleByRoleId(roleId);
        }

        public int InsertRole_Permission(int roleId, int permission)
        {
            return _dataAccess.InsertRole_Permission(roleId, permission);
        }

        public int InsertUser_Organization(int userId, int oid)
        {
            return _dataAccess.InsertUser_Organization(userId, oid);
        }

        public int SetRole_Permission(int roleId, int permission)
        {
            return _dataAccess.SetRole_Permission(roleId, permission);
        }

        public int DeleteRole_PermissionByRoleId(int roleId)
        {
            return _dataAccess.DeleteRole_PermissionByRoleId(roleId);
        }
    }
}
