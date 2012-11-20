using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Base;
using DotNet.Base.Contract;
using DotNet.Data;
namespace DotNet.Base
{
    public class UserDataAccess : IUserDataAccess
    {
        public int Insert(User user)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertUser"))
            {
                cmd.SetParameterValue("@UserId", user.UserId);
                cmd.SetParameterValue("@NickName", user.NickName);
				cmd.SetParameterValue("@Name", user.Name);
				cmd.SetParameterValue("@Password", user.Password);
                cmd.SetParameterValue("@Sex", user.Sex);
                cmd.SetParameterValue("@Cellphone", user.Cellphone);
                cmd.SetParameterValue("@Telephone", user.Telephone);
                cmd.SetParameterValue("@Email", user.Email);
                cmd.SetParameterValue("@Activity", user.Activity);
                cmd.SetParameterValue("@Address", user.Address);
                cmd.SetParameterValue("@QQ", user.QQ);
                cmd.SetParameterValue("@Fax", user.Fax);
                cmd.SetParameterValue("@Postcode", user.Postcode);
                cmd.SetParameterValue("@InUserId", user.InUserId);
                cmd.SetParameterValue("@InDate", user.InDate);
                cmd.SetParameterValue("@EditUserId", user.EditUserId);
                cmd.SetParameterValue("@EditDate", user.EditDate);
                cmd.SetParameterValue("@Enabled", user.Enabled);
                cmd.SetParameterValue("@FirstVisit", user.FirstVisit);
                cmd.SetParameterValue("@PreviousVisit", user.PreviousVisit);
                cmd.SetParameterValue("@LastVisit", user.LastVisit);
                cmd.SetParameterValue("@Question", user.Question);
                cmd.SetParameterValue("@Answer", user.Answer);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return user.UserId;
                return result;
                // if (result != null)
                //     return Convert.ToInt32(result);
                //return -1;
            }
        }

        public List<User> GetUsers(string sqlWhere)
        {
            List<User> users=new List<User>();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUsers"))
            {
                //cmd.CommandText = cmd.CommandText + " " + sqlWhere;
                if(!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText+" " + sqlWhere;
                }
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        User user=new User();
                        if (!Convert.IsDBNull(dr["UserId"]))
                        {
                            user.UserId = Convert.ToInt32(dr["UserId"]);
                        }
                        if (!Convert.IsDBNull(dr["NickName"]))
                        {
                            user.NickName = dr["NickName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Name"]))
                        {
                            user.Name = dr["Name"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Password"]))
                        {
                            user.Password =  dr["Password"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Sex"]))
                        {
                            user.Sex = Convert.ToBoolean(dr["Sex"]);
                        }
                        if (!Convert.IsDBNull(dr["Cellphone"]))
                        {
                            user.Cellphone = dr["Cellphone"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Telephone"]))
                        {
                            user.Telephone = dr["Telephone"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Email"]))
                        {
                            user.Email = dr["Email"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Activity"]))
                        {
                            user.Activity = Convert.ToBoolean(dr["Activity"]);
                        }
                        if (!Convert.IsDBNull(dr["Address"]))
                        {
                            user.Address = dr["Address"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["QQ"]))
                        {
                            user.QQ = dr["QQ"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Fax"]))
                        {
                            user.Fax = dr["Fax"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Postcode"]))
                        {
                            user.Postcode = dr["Postcode"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["InUserId"]))
                        {
                            user.InUserId = Convert.ToInt32(dr["InUserId"]);
                        }
                        if (!Convert.IsDBNull(dr["InDate"]))
                        {
                            user.InDate = Convert.ToDateTime(dr["InDate"]);
                        }
                        if (!Convert.IsDBNull(dr["EditUserId"]))
                        {
                            user.EditUserId = Convert.ToInt32(dr["EditUserId"]);
                        }
                        if (!Convert.IsDBNull(dr["EditDate"]))
                        {
                            user.EditDate = Convert.ToDateTime(dr["EditDate"]);
                        }
                        if (!Convert.IsDBNull(dr["Enabled"]))
                        {
                            //user.Enabled = dr["Enabled"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["FirstVisit"]))
                        {
                            user.FirstVisit = Convert.ToDateTime(dr["FirstVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["FirstVisit"]))
                        {
                            user.FirstVisit = Convert.ToDateTime(dr["FirstVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["PreviousVisit"]))
                        {
                            user.PreviousVisit = Convert.ToDateTime(dr["PreviousVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["LastVisit"]))
                        {
                            user.LastVisit = Convert.ToDateTime(dr["LastVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["Question"]))
                        {
                            user.Question = dr["Question"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Answer"]))
                        {
                            user.Answer = dr["Answer"].ToString();
                        }
                        user.RoleName = !Convert.IsDBNull(dr["RoleName"]) ? dr["RoleName"].ToString() : string.Empty;
                        user.Organization = !Convert.IsDBNull(dr["FullName"]) ? dr["FullName"].ToString() : string.Empty;
                        users.Add(user);
                    }
                }
                
            }
            return users;
        }

        public int DeleteUserByUserId(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("DeleteUserByUserId"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Exists(string nickName, string password)
        {
            using(DataCommand cmd=DataCommandManager.GetDataCommand("UserExist"))
            {
                cmd.SetParameterValue("@NickName", nickName);
                cmd.SetParameterValue("@Password", password);
                int result = cmd.ExecuteScalar<int>();
                return result;
            }
        }

        public User GetUser(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUser"))
            {
                cmd.SetParameterValue("@UserId", userId);
                User user = new User();
                using (IDataReader dr = cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["UserId"]))
                        {
                            user.UserId = Convert.ToInt32(dr["UserId"]);
                        }
                        if (!Convert.IsDBNull(dr["NickName"]))
                        {
                            user.NickName = dr["NickName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Name"]))
                        {
                            user.Name = dr["Name"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Password"]))
                        {
                            user.Password = dr["Password"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Sex"]))
                        {
                            //user.Sex = Convert.ToInt32(dr["Sex"]);
                        }
                        if (!Convert.IsDBNull(dr["Cellphone"]))
                        {
                            user.Cellphone = dr["Cellphone"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Telephone"]))
                        {
                            user.Telephone = dr["Telephone"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Email"]))
                        {
                            user.Email = dr["Email"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Activity"]))
                        {
                            user.Activity = Convert.ToBoolean(dr["Activity"]);
                        }
                        if (!Convert.IsDBNull(dr["Address"]))
                        {
                            user.Address = dr["Address"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["QQ"]))
                        {
                            user.QQ = dr["QQ"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Fax"]))
                        {
                            user.Fax = dr["Fax"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Postcode"]))
                        {
                            user.Postcode = dr["Postcode"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["InUserId"]))
                        {
                            user.InUserId = Convert.ToInt32(dr["InUserId"]);
                        }
                        if (!Convert.IsDBNull(dr["InDate"]))
                        {
                            user.InDate = Convert.ToDateTime(dr["InDate"]);
                        }
                        if (!Convert.IsDBNull(dr["EditUserId"]))
                        {
                            user.EditUserId = Convert.ToInt32(dr["EditUserId"]);
                        }
                        if (!Convert.IsDBNull(dr["EditDate"]))
                        {
                            user.EditDate = Convert.ToDateTime(dr["EditDate"]);
                        }
                        if (!Convert.IsDBNull(dr["Enabled"]))
                        {
                            //user.Enabled = dr["Enabled"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["FirstVisit"]))
                        {
                            user.FirstVisit = Convert.ToDateTime(dr["FirstVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["FirstVisit"]))
                        {
                            user.FirstVisit = Convert.ToDateTime(dr["FirstVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["PreviousVisit"]))
                        {
                            user.PreviousVisit = Convert.ToDateTime(dr["PreviousVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["LastVisit"]))
                        {
                            user.LastVisit = Convert.ToDateTime(dr["LastVisit"]);
                        }
                        if (!Convert.IsDBNull(dr["Question"]))
                        {
                            user.Question = dr["Question"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Answer"]))
                        {
                            user.Answer = dr["Answer"].ToString();
                        }
                    }
                }
                return user;
            }
        }

        public DataTable GetUserInfo(int userId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUserInfo"))
            {
                cmd.SetParameterValue("@UserId", userId);
                return cmd.ExecuteDataTable();
            }
        }

        public UserExtention GetUserExtention(int userId)
        {
            UserExtention model=new UserExtention();
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUserInfo")) 
            {
                cmd.SetParameterValue("@UserId", userId);
                using(IDataReader dr=cmd.ExecuteDataReader())
                {
                    while (dr.Read())
                    {
                        if (!Convert.IsDBNull(dr["UserId"]))
                        {
                            model.UserId = Convert.ToInt32(dr["UserId"]);
                        }
                        if (!Convert.IsDBNull(dr["NickName"])) 
                        {
                            model.NickName = dr["NickName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["Name"])) 
                        {
                            model.Name = dr["UserId"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["OrganizationId"]))
                        {
                            model.OrganizationId = Convert.ToInt32(dr["OrganizationId"]);
                        }
                        if (!Convert.IsDBNull(dr["FullName"])) 
                        {
                            model.OrganizationName = dr["FullName"].ToString();
                        }
                        if (!Convert.IsDBNull(dr["RoleId"])) 
                        {
                            model.RoleId = Convert.ToInt32(dr["RoleId"]) ;
                        }
                        if (!Convert.IsDBNull(dr["RoleName"])) 
                        {
                            model.RoleName = dr["RoleName"].ToString();
                        }
                    }
                }
            }
            return model;
        }

        public int UpdatePassword(User model)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdatePassword"))
            {
                cmd.SetParameterValue("@Password", model.Password);
                cmd.SetParameterValue("@UserId", model.UserId);
                return cmd.ExecuteNonQuery();
            }
        }


        public void AppendWhereIfNeed(StringBuilder sql,bool hasWhere)
        {
            sql.AppendLine(hasWhere ? "Where" : "AND");
        }
    }
}
