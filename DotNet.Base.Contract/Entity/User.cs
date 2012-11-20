using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DotNet.Base.Contract
{
    /// <summary>
    /// User:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataContract]
    public class User
    {
        public User()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [DataMember]
        public string RoleName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DataMember]
        public string Organization { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Cellphone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Activity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string QQ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Fax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Postcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? InUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? InDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? EditUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? FirstVisit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? PreviousVisit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime? LastVisit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Question { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public int SortCode { get; set; }
    }

    [Serializable]
    [DataContract]
    public class UserExtention
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int OrganizationId { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }
    }
}
