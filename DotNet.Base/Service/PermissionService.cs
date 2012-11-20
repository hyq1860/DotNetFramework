using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Base.DataAccess;

namespace DotNet.Base.Service
{
    public class PermissionService : IPermissionService
    {
        private static IPermissionDataAccess _dataAccess=new PermissionDataAccess();
        public List<Permission> GetPermissions(string sqlWhere)
        {
            return _dataAccess.GetPermissions(sqlWhere);
        }

        public int Insert(Permission model)
        {
            throw new NotImplementedException();
        }
    }
}
