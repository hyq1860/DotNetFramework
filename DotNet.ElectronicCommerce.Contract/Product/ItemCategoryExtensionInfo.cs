// -----------------------------------------------------------------------
// <copyright file="ItemCategoryExtensionInfo.cs" company="">
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
    /// 商品分类扩展类
    /// </summary>
    public class ItemCategoryExtensionInfo
    {
        public Int32 Id { get; set; }

        public Int32 CategoryId { get; set; }

        /// <summary>
        /// (扩展属性)显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// (扩展属性)字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型(比如：字符串，整数，日期之类)
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 长度(比如:50)
        /// </summary>
        public string ValueLength { get; set; }

        /// <summary>
        /// 输入类型(比如：文本框，文本域，下拉框，复选框之类)
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }

    }
}
