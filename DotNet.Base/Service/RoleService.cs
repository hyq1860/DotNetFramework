using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DotNet.Base.Contract;
using DotNet.Base.DataAccess;

namespace DotNet.Base.Service
{
    public class RoleService:IRoleService
    {
        private static IRoleDataAccess _dataAccess=new RoleDataAccess();
        public int Insert(Role model)
        {
            return _dataAccess.Insert(model);
        }

        public List<Role> GetAllRoles(string sqlWhere)
        {
            return _dataAccess.GetAllRoles(sqlWhere);
        }

        public int DeleteByRoleId(int roleid)
        {
            int success = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                success=_dataAccess.DeleteByRoleId(roleid);
                if(success>0)
                {
                    ITableRelationDataAccess tableRelationDataAccess=new TableRelationDataAccess();
                    success = tableRelationDataAccess.DeleteUser_RoleByRoleId(roleid);
                }
                if(success>0)
                {
                    ts.Complete();
                }
            }
            return success;
        }

        public Role GetRoleByRoleId(int roleid)
        {
            return _dataAccess.GetRoleByRoleId(roleid);
        }

        public int Update(Role model)
        {
            return _dataAccess.Update(model);
        }

        public List<RoleExtension> GetRoleExtensions()
        {
            return _dataAccess.GetRoleExtensions();
        }

        public RoleExtension GetRoleExtensionByRoleId(int roleId)
        {
            return _dataAccess.GetRoleExtensionByRoleId(roleId);
        }

        public Dictionary<int, List<string>> GetRolePermissions()
        {
            return _dataAccess.GetRolePermissions();
        }
    }
}
