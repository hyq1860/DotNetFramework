using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.SpiderApplication.WebSite
{
    using System.IO;
    using System.Text;

    using DotNet.Common.Utility;

    public partial class _Default : System.Web.UI.Page
    {
        public string Data { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var datas = JsonHelper.FromJson<List<Gold>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\data.json"));
            datas = datas.Where(s => s.Name.Contains("Au9999")).OrderBy(s => s.DateString).ToList();
            foreach (var gold in datas)
            {
                gold.DateString=gold.DateString.Replace("年", ", ").Replace("月", ", ").Replace("日", String.Empty);
                sb.AppendLine(string.Format("[Date.UTC({0}), {1}],", gold.DateString, gold.OpeningPrice));
            }
            Data = sb.ToString().TrimEnd(',');
            First = datas.FirstOrDefault().DateString;
            Latest = datas.LastOrDefault().DateString;
        }

        public string First { get; set; }
        public string Latest { get; set; }
    }

    public class Gold
    {
        public string DateString { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public decimal OpeningPrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal ClosingPrice { get; set; }
    }
}