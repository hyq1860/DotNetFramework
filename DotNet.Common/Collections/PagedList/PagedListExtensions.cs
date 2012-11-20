// -----------------------------------------------------------------------
// <copyright file="PagedListExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common.Collections.PagedList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// 扩展迭代器，转换为可分页List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="allItems">被扩展迭代器</param>
        /// <param name="pageIndex">页码:从1开始,而不是从0开始</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int pageIndex, int pageSize, int totalCount)
        {
            if (allItems == null) throw new ArgumentNullException("allItems");

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var temp = totalCount % pageSize;
            int pageCount = temp == 0 ? temp : temp + 1;
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);

            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalCount);
        }
    }
}
