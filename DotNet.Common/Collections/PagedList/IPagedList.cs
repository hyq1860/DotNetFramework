// -----------------------------------------------------------------------
// <copyright file="IPagedList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; set; }
    }
}
