using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract.IService
{
    public interface IUserRoleService
    {
        int Insert(int userId, int roleId);
    }
}
