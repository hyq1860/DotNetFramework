using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Base.Contract
{
    public interface IOrganizationService
    {
        int Insert(OrganizationInfo model);
        int Update(OrganizationInfo model);
        int DeleteById(int id);
        List<OrganizationInfo> GetAllOrganizations();
        List<OrganizationInfo> GetOrganizationByCondition(string sqlWhere);
        OrganizationInfo GetOrganizationById(int id);
    }
}
