using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Base.Contract;
using DotNet.Base.DataAccess;

namespace DotNet.Base.Service
{
    public class OrganizationService : IOrganizationService
    {
        private static IOrganizationDataAccess _dataAccess=new OrganizationDataAccess();

        public int Insert(OrganizationInfo model)
        {
            return _dataAccess.Insert(model);
        }

        public int Update(OrganizationInfo model)
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrganizationInfo> GetAllOrganizations()
        {
            throw new NotImplementedException();
        }

        public List<OrganizationInfo> GetOrganizationByCondition(string sqlWhere)
        {
            return _dataAccess.GetOrganizationByCondition(sqlWhere);
        }

        public OrganizationInfo GetOrganizationById(int id)
        {
            return _dataAccess.GetOrganizationById(id);
        }
    }
}
