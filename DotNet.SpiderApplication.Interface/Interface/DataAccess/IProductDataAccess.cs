// -----------------------------------------------------------------------
// <copyright file="IProductDataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DotNet.SpiderApplication.Contract.Entity;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProductDataAccess
    {
        List<ProductInfo> GetProducts(string sqlWhere);
    }
}
