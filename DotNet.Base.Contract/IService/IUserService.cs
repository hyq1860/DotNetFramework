using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    public interface IUserService
    {
        int Insert(User user);
        int Insert(User user,int roleId);
        List<User> GetUsers(string sqlWhere);
        int DeleteUserByUserId(int userId);

        int Exists(string nickName, string password);

        User GetUser(int userId);

        DataTable GetUserInfo(int userId);

        UserExtention GetUserExtention(int userId);

        int UpdatePassword(User model);
    }
}
