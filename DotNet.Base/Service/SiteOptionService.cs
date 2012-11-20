// -----------------------------------------------------------------------
// <copyright file="SiteOptionService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract;
    using DotNet.Base.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteOptionService : ISiteOptionService
    {
        private ISiteOptionDataAccess _dataAccess=new SiteOptionDataAccess();
        public int Add(SiteOptionInfo model)
        {
            return _dataAccess.Add(model);
        }

        public List<SiteOptionInfo> GetSiteOption()
        {
            return _dataAccess.GetSiteOption();
        }

        public int Update(SiteOptionInfo model)
        {
            return _dataAccess.Update(model);
        }
    }
}
