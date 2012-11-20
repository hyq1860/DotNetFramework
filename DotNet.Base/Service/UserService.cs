using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DotNet.Base.Contract;
using DotNet.Base.DataAccess;
using DotNet.Base.Service;

namespace DotNet.Base
{
    public class UserService:IUserService
    {
        private static IUserDataAccess _dataAccess=new UserDataAccess();
        public int Insert(User user)
        {
            return _dataAccess.Insert(user);
        }

        public int Insert(User user, int roleId)
        {
            int success = 0;
            ITableRelationService tableRelationService=new TableRelationService();
            using(TransactionScope ts=new TransactionScope())
            {
                success = Insert(user);
                user.UserId = success;
                if(success>0)
                {
                    success = tableRelationService.InsertUser_Role(user.UserId, roleId);
                }
                if(success>0)
                {
                    ts.Complete();
                }
            }
            return success;
        }

        public List<User> GetUsers(string sqlWhere)
        {
            return _dataAccess.GetUsers(sqlWhere);
        }

        public int DeleteUserByUserId(int userId)
        {
            int success = 0;
            using(TransactionScope ts=new TransactionScope())
            {
                success = _dataAccess.DeleteUserByUserId(userId);
                if(success>0)
                {
                    ITableRelationDataAccess tableRelationDataAccess=new TableRelationDataAccess();
                    success = tableRelationDataAccess.DeleteUser_RoleByUserId(userId);
                    if(success>0)
                    {
                        ts.Complete();
                    }
                }
            }
            return success;
        }

        public int Exists(string nickName, string password)
        {
            return _dataAccess.Exists(nickName, password);
        }

        public User GetUser(int userId)
        {
            return _dataAccess.GetUser(userId);
        }

        public DataTable GetUserInfo(int userId)
        {
            return _dataAccess.GetUserInfo(userId);
        }

        public UserExtention GetUserExtention(int userId)
        {
            return _dataAccess.GetUserExtention(userId);
        }

        public int UpdatePassword(User model)
        {
            return _dataAccess.UpdatePassword(model);
        }
    }
}
