// -----------------------------------------------------------------------
// <copyright file="MetaDataInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ContentManagement.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MetaDataInfo
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int MetaId { get; set; }

        public string Attr_Value { get; set; }
    }
}
