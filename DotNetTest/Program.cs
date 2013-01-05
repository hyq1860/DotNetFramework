using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using DotNet.Common.Utility;
using DotNet.IoC;
using DotNet.Web;
using DotNet.Web.Http;
using MongoDB.Bson;

namespace DotNetTest
{
    using System.Diagnostics;
    using System.IO;

    using DotNet.BasicSpider;
    using DotNet.TaskScheduler;

    using csExWB;
    
    class Program
    {
        public static void HtmlParse(string html)
        {
            if(string.IsNullOrEmpty(html))
                return;
            MessageBox.Show(html);
        }

        public string  GetInfoPropertys(object objInfo)
        {
            StringBuilder strB = new StringBuilder();

            if (objInfo == null) return string.Empty;
            Type tInfo = objInfo.GetType();
            PropertyInfo[] pInfos = tInfo.GetProperties();
            if (tInfo.IsGenericType)
            {
                System.Collections.ICollection Ilist = objInfo as System.Collections.ICollection;
                if (Ilist != null)
                {
                    strB.AppendFormat("集合子属性{0}<br/>", Ilist.Count);
                    foreach (object obj in Ilist)
                    {
                        GetInfoPropertys(obj);
                    }
                }
                else
                {
                    strB.Append("泛型集合为空<br/>");
                }
                return string.Empty;
            }
            foreach (PropertyInfo pTemp in pInfos)
            {
                string Pname = pTemp.Name;
                string pTypeName = pTemp.PropertyType.Name;
                object Pvalue = pTemp.GetValue(objInfo, null);
                if (pTemp.PropertyType.IsValueType || pTemp.PropertyType.Name.StartsWith("String"))
                {
                    string value = (Pvalue == null ? "为空" : Pvalue.ToString());
                    strB.AppendFormat("属性名：{0}，属性类型：{1}，属性值：{2}<br/>", Pname, pTypeName, value);
                }
                else
                {
                    string value = Pvalue == null ? "为空" : Pvalue.ToString();
                    strB.AppendFormat("<br/><b>子类</b>，属性名：{0}，属性类型：{1}，属性值：{2}<br/>", Pname, pTypeName, value);
                    strB.Append("----------------------------------------------<br/>");
                    GetInfoPropertys(Pvalue);
                }

            }
            return strB.ToString();
        }

        [STAThread]
        static void Main(string[] args)
        {
            #region

            FileRead.Read();
            FileRead.Read("GetPromotionRulesByIdsWithoutRuleExpands");
            return;
            #endregion

            #region mongodb
            //var objectid= MongoDBOfficialTest.Insert(new ShoppingCartEntity(){CartId = "123",Ha = "ssss",Promotion = new PromotionEntity(){Date1="满赠新促销",Date2 = new List<string>(){"测试"}}});
            var objectid = new ObjectId("50e78f8c9e3eca2d6c538b9d");
            MongoDBOfficialTest.GetById(objectid);
            //MongoDBOfficialTest.GetById(objectid);

            //MongoDBTest.Insert(new ShoppingCartEntity(){CartId = "123456"});
            //MongoDBTest.GetById("123456");
            //MongoDBTest.Update(new ShoppingCartEntity(){CartId = "123456"});
            return;
            #endregion

            //var indexUrls= GoldSpider.GetUrls();
            //var urls= GoldSpider.Spider(indexUrls);
            //string json = JsonHelper.ToJson(urls);
            //File.WriteAllText(Environment.CurrentDirectory+"\\urls.json",json);

            var data= JsonHelper.FromJson<List<string>>(File.ReadAllText(Environment.CurrentDirectory + "\\urls.json"));
            var finalDatas= GoldSpider.SpiderPrice(data);
            File.WriteAllText(Environment.CurrentDirectory + "\\data.json", JsonHelper.ToJson(finalDatas));
            return;

            //SqliteTest.Test();
            //string connectionstring1 = "Data Source=e:\\sqlite.db3";
            //string connectionstring2 = "Data Source=e:\\sqlite.db3;PRAGMA cache_size=10000";
            //SqliteTest.Query("select id from test1 limit 0,10000", connectionstring2);
            //SqliteTest.Query("select id from test1 limit 0,10000", connectionstring1);
            //SqliteTest.Query("select id from test1 limit 0,10000", connectionstring1);

            SqliteTest.Memory(100000);
            SqliteTest.MemoryQuery("select id from test1 limit 0,10000");
            return;

            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.TimeOut = 15;
            WebBrowerManager.Instance.FilterRequest = true;
            WebBrowerManager.Instance.FilterAction.Add(".css", (string key, string source) =>
                {
                    if(source.EndsWith(key))
                    {
                        return true;
                    }
                    return false;
                });
            string html1 = WebBrowerManager.Instance.Run("http://www.sge.sh/publish/sge/xqzx/jyxq/index.htm");

            Console.WriteLine(html1);
            Console.Read();
            return;

            TaskManager taskManager=new TaskManager();
            taskManager.Test02();
            Console.ReadKey();
            return;

            Process.Start("IExplore.exe", "www.northwindtraders.comTest");

            
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
