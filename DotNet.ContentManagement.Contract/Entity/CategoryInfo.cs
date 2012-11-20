// -----------------------------------------------------------------------
// <copyright file="CategoryInfo.cs" company="">
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
    public class CategoryInfo
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int ParentId { get; set; }
    }
}
