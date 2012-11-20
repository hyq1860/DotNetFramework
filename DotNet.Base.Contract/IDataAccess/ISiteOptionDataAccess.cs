// -----------------------------------------------------------------------
// <copyright file="ISiteOptionDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.Base.Contract;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISiteOptionDataAccess
    {
        int Add(SiteOptionInfo model);

        List<SiteOptionInfo> GetSiteOption();

        int Update(SiteOptionInfo model);
    }
}
