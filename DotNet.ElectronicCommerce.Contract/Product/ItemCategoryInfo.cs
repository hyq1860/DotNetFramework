// -----------------------------------------------------------------------
// <copyright file="ItemCategory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.ElectronicCommerce.Contract.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 商品分类
    /// </summary>
    public class ItemCategoryInfo
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public Int32 ParentId { get; set; }
    }
}
