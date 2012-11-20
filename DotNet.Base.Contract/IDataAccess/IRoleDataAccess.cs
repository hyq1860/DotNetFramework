using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    public interface IRoleDataAccess
    {
        int Insert(Role model);
        List<Role> GetAllRoles(string sqlWhere);
        int DeleteByRoleId(int roleid);
        Role GetRoleByRoleId(int roleid);
        int Update(Role model);
        List<RoleExtension> GetRoleExtensions();
        RoleExtension GetRoleExtensionByRoleId(int roleId);

        Dictionary<int, List<string>> GetRolePermissions();
    }
}
