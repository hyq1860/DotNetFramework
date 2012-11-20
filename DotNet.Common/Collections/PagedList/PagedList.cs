// -----------------------------------------------------------------------
// <copyright file="PagedList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PagedList<T> :List<T>,IPagedList
    {
        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;

            this.TotalCount = items.Count;

            this.PageIndex = pageIndex;

            int startRecordIndex = (this.PageIndex - 1) * this.PageSize + 1;

            int endRecordIndex = this.TotalCount > this.PageIndex * this.PageSize ? this.PageIndex * this.PageSize : this.TotalCount;
            for (int i = startRecordIndex - 1; i < endRecordIndex; i++)
            {
                Add(items[i]);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="intPageIndex">编号</param>
        /// <param name="intPageSize">大小</param>
        public PagedList(IEnumerable<T> items, int intPageIndex, int intPageSize, int totalCount)
        {
            this.PageIndex = intPageIndex;

            this.PageSize = intPageSize;

            this.TotalCount = totalCount;

            this.AddRange(items);
        }
    }
}
