using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Common.Pager;

namespace DotNet.EnterpriseWebSite.Pager
{
    public partial class Pager_01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public HtmlPager Paging
        {
            get
            {
                var paging = new HtmlPager();
                paging.PageIndex =
                    int.Parse(string.IsNullOrEmpty(Request.QueryString["page"]) ? "1" : Request.QueryString["page"]);
                paging.TotalRecordCount = 1001;
                paging.PageSize = 11;
                paging.Href = "Pager_01.aspx?delegate=wenxue&";
                return paging;
            }
        }
    }

    /// <summary>
    /// 生成分页标签
    /// </summary>
    public class Pager
    {
        private int pageIndex;
        /// <summary>
        /// 当前请求的页码
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            private set { pageIndex = value; }
        }

        private int pageSize;
        /// <summary>
        /// 请求的当前页的大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            private set { pageSize = value; }
        }

        private int rowCount;
        /// <summary>
        /// 数据总量，可以根据这个判断出能够显示多少页
        /// </summary>
        public int RowCount
        {
            get { return rowCount; }
            private set { rowCount = value; }
        }

        private string hrefFormat;
        /// <summary>
        /// 页码连接所要指向的url格式化字符串例如：aticleList.aspx?{0}&delegate=wenxue
        /// </summary>
        public string HrefFormat
        {
            get { return hrefFormat; }
            set { hrefFormat = value; }
        }
        private int pageCount;

        private Pager() { }

        public Pager(int pageIndex, int pageSize, int rowCount)
        {
            //计算出总共有多少页
            this.pageCount = (int)Math.Ceiling(rowCount / (double)pageSize);
            //如果pageIndex<1 就让当前页码等于1，如果pageIndex大于最大页码.
            this.PageIndex = GetPageIndex(pageIndex, pageSize, rowCount);

            this.PageSize = pageSize;
        }

        public string CreatePager()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class='pager'>");
            if (this.PageIndex > 1)
            {
                sb.AppendLine("\t<a id='pre' href='" + string.Format(this.HrefFormat, "page=" + GetPageIndex(this.pageIndex - 1, this.PageSize, this.RowCount)) + "'>" + "上一页" + "</a>");
            }
            else
            {
                sb.AppendLine("\t<a id='pre' class='false'>" + "上一页" + "</a>");
            }
            CreatePageIndex(sb);
            if (this.pageIndex < this.pageCount)
            {
                sb.AppendLine("\t<a id='next' href='" + string.Format(this.HrefFormat, "page=" + GetPageIndex(this.pageIndex + 1, this.PageSize, this.RowCount)) + "'>" + "下一页" + "</a>");
            }

            sb.AppendLine("</div>");
            return sb.ToString();
        }

        private void CreatePageIndex(StringBuilder sb)
        {
            if (this.pageIndex <= 7)
            {
                int i = 1;
                for (; i <= (this.pageIndex > this.pageCount ? 10 : this.pageCount); i++)
                {
                    if (i == this.pageIndex)
                    {
                        sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "' class='Active'>" + i + "</a>");
                        continue;
                    }
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "'>" + i + "</a>");
                }
                if (i < this.pageCount)
                {
                    sb.AppendLine("...");
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + this.pageCount + "") + "'>" + this.pageCount + "</a>");

                }
            }
            else if (this.PageIndex > 12 && this.pageCount > this.pageIndex + 5)
            {
                sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=1") + "'>" + 1 + "</a>");
                sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=2") + "'>" + 2 + "</a>");
                sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=3") + "'>" + 3 + "</a>");
                sb.AppendLine("...");
                for (int i = this.pageIndex - 4; i <= (this.pageIndex + 4 > this.pageCount ? this.pageCount : this.pageIndex + 4); i++)
                {
                    if (i == this.pageIndex)
                    {
                        sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "' class='Active'>" + i + "</a>");
                        continue;
                    }
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "'>" + i + "</a>");
                }
                sb.AppendLine("...");
                for (int j = this.pageCount - 2; j <= this.pageCount; j++)
                {
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + j) + "'>" + j + "</a>");
                }
            }
            else if (this.pageIndex > 7)
            {

                int i = this.pageIndex - 4;
                if (i > 2)
                {
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=1") + "'>" + 1 + "</a>");
                    sb.AppendLine("...");
                }
                for (; i <= (this.pageIndex + 4 > this.pageCount ? this.pageCount : this.pageIndex + 4); i++)
                {
                    if (i == this.pageIndex)
                    {
                        sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "' class='Active'>" + i + "</a>");
                        continue;
                    }
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + i) + "'>" + i + "</a>");

                }
                if (i < this.pageCount)
                {
                    sb.AppendLine("...");
                    sb.AppendLine("\t<a href='" + string.Format(HrefFormat, "page=" + this.pageCount + "") + "'>" + this.pageCount + "</a>");
                }
            }
        }

        private int GetPageIndex(int pageIndex, int pageSize, int rowCount)
        {
            int pageCount = (int)Math.Ceiling(rowCount / (double)pageSize);
            if (pageIndex < 1)
            {
                return 1;
            }
            else if (pageIndex > this.pageCount)//就让他等于最大页码
            {
                return pageCount;
            }
            else
            {
                return pageIndex;
            }
        }


        /// <summary>
        /// 类似GOOGLE的分页函数
        /// </summary>
        /// <param name="totalRecordCount">总记录数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="query_string">Url参数</param>
        public string Pagination(int totalRecordCount, int pageSize, int pageIndex, string query_string)
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

            pagestr += pageIndex > 1 ? "<a href=\"" + query_string + "?page=1\">首页</a>&nbsp;&nbsp;<a href=\"" + query_string + "?page=" + pre + "\">上一页</a>" : "首页 上一页";
            
            // 中间页处理，这个增加时间复杂度，减小空间复杂度
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += pageIndex == i ? "&nbsp;&nbsp;<font color=\"#ff0000\">" + i + "</font>" : "&nbsp;&nbsp;<a href=\"" + query_string + "?page=" + i + "\">" + i + "</a>";
            }

            pagestr += pageIndex != totalPageCount ? "&nbsp;&nbsp;<a href=\"" + query_string + "?page=" + next + "\">下一页</a>&nbsp;&nbsp;<a href=\"" + query_string + "?page=" + totalPageCount + "\">末页</a>" : " 下一页 末页";

            return pagestr;
        }
    }
}