// -----------------------------------------------------------------------
// <copyright file="HtmlPager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Common.Pager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// html分页辅助类
    /// http://www.cnblogs.com/bmw3/archive/2012/05/30/2526518.html
    /// </summary>
    public class HtmlPager
    {
        private int pageIndex;

        /// <summary>
        /// 当前请求的页码
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int pageSize;

        /// <summary>
        /// 请求的当前页的大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int totalRecordCount;

        /// <summary>
        /// 数据总量，可以根据这个判断出能够显示多少页
        /// </summary>
        public int TotalRecordCount
        {
            get { return totalRecordCount; }
            set { totalRecordCount = value; }
        }

        private string href;
        /// <summary>
        /// 页码连接所要指向的url格式化字符串例如：aticleList.aspx?{0}&delegate=wenxue
        /// </summary>
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        /// <summary>
        /// 生成最终的分页html
        /// </summary>
        /// <returns></returns>
        public string RenderPager()
        {
            var sb = new StringBuilder();

            // 总页数
            int totalPageCount = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            // 计算总页数
            if (pageSize != 0)
            {
                totalPageCount = totalRecordCount / pageSize;
                totalPageCount = (totalRecordCount % pageSize) != 0 ? totalPageCount + 1 : totalPageCount;
                totalPageCount = totalPageCount == 0 ? 1 : totalPageCount;
            }

            next = pageIndex + 1;
            pre = pageIndex - 1;

            // 中间页起始序号
            startcount = (pageIndex + 5) > totalPageCount ? totalPageCount - 9 : pageIndex - 4;

            // 中间页终止序号
            endcount = pageIndex < 5 ? 10 : pageIndex + 5;

            // 为了避免输出的时候产生负数，设置如果小于1就从序号1开始
            if (startcount < 1)
            {
                startcount = 1;
            }

            // 页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内
            if (totalPageCount < endcount)
            {
                endcount = totalPageCount;
            }

            sb.AppendFormat("共{0}页", totalPageCount);

            if(pageIndex>1)
            {
                sb.AppendFormat("<a href=\"{0}?page=1\">首页</a>&nbsp;&nbsp;<a href=\"{0}?page={1}\">上一页</a>",href,pre);
            }
            else
            {
                sb.Append("首页 上一页");
            }

            // 中间页处理，这个增加时间复杂度，减小空间复杂度
            for (int i = startcount; i <= endcount; i++)
            {
                if (pageIndex == i)
                {
                    sb.AppendFormat("&nbsp;&nbsp;<font color=\"#ff0000\">{0}</font>", i);
                }
                else
                {
                    sb.AppendFormat("&nbsp;&nbsp;<a href=\"{0}?page={1}\">{1}</a>", href, i);
                }
            }

            if (pageIndex != totalPageCount)
            {
                sb.AppendFormat("&nbsp;&nbsp;<a href=\"{0}?page={1}\">下一页</a>&nbsp;&nbsp;<a href=\"{0}?page={2}\">末页</a>", href,next, totalPageCount);
            }
            else
            {
                sb.Append(" 下一页 末页");
            }
            return sb.ToString();
        }

        public string RenderPager2()
        {
            // 总页数
            int totalPageCount = 0;
            int next = 0;
            int pre = 0;
            int startcount = 0;
            int endcount = 0;
            string pagestr = string.Empty;

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            // 计算总页数
            if (pageSize != 0)
            {
                totalPageCount = totalRecordCount / pageSize;
                totalPageCount = (totalRecordCount % pageSize) != 0 ? totalPageCount + 1 : totalPageCount;
                totalPageCount = totalPageCount == 0 ? 1 : totalPageCount;
            }

            next = pageIndex + 1;
            pre = pageIndex - 1;

            // 中间页起始序号
            startcount = (pageIndex + 5) > totalPageCount ? totalPageCount - 9 : pageIndex - 4;

            // 中间页终止序号
            endcount = pageIndex < 5 ? 10 : pageIndex + 5;

            // 为了避免输出的时候产生负数，设置如果小于1就从序号1开始
            if (startcount < 1)
            {
                startcount = 1;
            }

            // 页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内
            if (totalPageCount < endcount)
            {
                endcount = totalPageCount;
            }

            pagestr = "共" + totalPageCount + "页&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            pagestr += pageIndex > 1 ? "<a href=\"" + href + "?page=1\">首页</a>&nbsp;&nbsp;<a href=\"" + href + "page=" + pre + "\">上一页</a>" : "首页 上一页";

            // 中间页处理，这个增加时间复杂度，减小空间复杂度
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += pageIndex == i ? "&nbsp;&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;&nbsp;<a href=\"" + href + "page=" + i + "\">" + i + "</a>";
            }

            pagestr += pageIndex != totalPageCount ? "&nbsp;&nbsp;<a href=\"" + href + "page=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + href + "page=" + totalPageCount + "\">末页</a>" : " 下一页 末页";

            return pagestr;
        }
    }
}
