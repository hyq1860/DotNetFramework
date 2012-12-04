using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using DotNet.Common.Utility;
using DotNet.IoC;
using DotNet.Web;
using DotNet.Web.Http;

namespace DotNetTest
{
    using DotNet.BasicSpider;

    using csExWB;

    class Program
    {
        public static void HtmlParse(string html)
        {
            if(string.IsNullOrEmpty(html))
                return;
            MessageBox.Show(html);
        }

        static void Main(string[] args)
        {
            //
            //EncodingTest.Test();
            //return;
            using(HttpClient hc1 = new HttpClient("http://www.cnblogs.com"))
            {
                string html = hc1.Request();
            }
            
            //WebPage page = new WebPage(html, "http://www.cnblogs.com", Encoding.UTF8);
            //page.SaveHtmlAndResource(@"1.html", false, new DirConfig(@"z:\1"));

            //return;

            HttpClientTest.Test();
            return;

            SqliteTest.Test();

            var uri = new Uri("http://misc.360buyimg.com/lib/js/2012/base-v1.js");


            //WebBrowerManager.Instance.ToVisitUrls = new List<string> { "http://www.360buy.com" };
            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.Run(uri.ToString());


            BootStrapperManager.Initialize(new NinjectBootstrapper());

            var add = CommonBootStrapper.ServiceLocator.GetInstance<Test>();
            //add.Alert("ceshi");
            add.Test1();


            HttpClient hc = new HttpClient("http://misc.360buyimg.com/lib/js/2012/base-v1.js");

            hc.SaveFile("e:\\1.js");

            hc.Request();
            hc.BeginRequest((h) =>
                {
                    Console.Write(h);
                });
            Console.ReadKey();
            var s= hc.Request();

            var list = new List<UnionOrderTransBFD>();
            list.Add(new UnionOrderTransBFD() { ActualPrice = 1, CommissionPrice = 1, Rate = 1, Source = ">123", SONumber = 111111111111, UpdateDate = DateTime.Now });
            list.Add(new UnionOrderTransBFD() { ActualPrice = 1, CommissionPrice = 1, Rate = 1, Source = ">123", SONumber = 111111111111, UpdateDate = DateTime.Now });

            var xml= ObjectXmlSerializer.ToXml(list,"ccc",true,false);

            var a=new A();
            a.name = 1;
            //a.ObjectB = new B() { ItemCode = "1", Qty = 1 };
            var b = new A();
            b.name =1;
            //b.ObjectB = new B() { ItemCode = "1", Qty = 1 };
            var isEqual = DotNet.Common.Utility.GenericEqualityComparer<A>.Equals(a, b);
            Console.WriteLine(isEqual);

            
        }
    }

    public class Test
    {
        private IAdd add;
        public Test(IAdd add)
        {
            this.add = add;
        }

        public void Test1()
        {
            add.Alert("ddddd");
        }
    }

    /// <summary>
    /// UnionOrderTransBFD
    /// </summary>
    [DataContract]
    [Serializable]
    [XmlRoot("MyCity", IsNullable = false)]
    public class UnionOrderTransBFD
    {
        /// <summary>
        /// Source
        /// </summary>
        [DataMember]
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// SONumber
        /// </summary>
        [DataMember]
        public Int64 SONumber
        {
            get;
            set;
        }

        /// <summary>
        /// TransactionNumber
        /// </summary>
        [DataMember]
        public int SOTransactionNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Rate
        /// </summary>
        [DataMember]
        public decimal Rate
        {
            get;
            set;
        }

        /// <summary>
        /// ActualPrice
        /// </summary>
        [DataMember]
        public decimal ActualPrice
        {
            get;
            set;
        }

        /// <summary>
        /// CommissionPrice
        /// </summary>
        [DataMember]
        public decimal CommissionPrice
        {
            get;
            set;
        }

        /// <summary>
        /// UpdateDate
        /// </summary>
        [DataMember]
        public DateTime UpdateDate
        {
            get;
            set;
        }
    }

    public class A
    {
        public int name { get; set; }
        //public B ObjectB { get; set; }
    }

    public class B 
    {
        public string ItemCode { get; set; }

        public int Qty { get; set; }
    }
}
