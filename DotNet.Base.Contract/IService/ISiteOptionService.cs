// -----------------------------------------------------------------------
// <copyright file="ISiteOptionService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Base.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISiteOptionService
    {
        int Add(SiteOptionInfo model);

        List<SiteOptionInfo> GetSiteOption();

        int Update(SiteOptionInfo model);
    }
}
