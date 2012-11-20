using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using DotNet.Common.Core;
using DotNet.SpiderApplication.Service;

namespace DotNet.SpiderProcessManager
{
    using System.Diagnostics;

    using DotNet.Common.Logging;
    using DotNet.Data;
    using DotNet.SpiderApplication.Contract.Entity;

    public partial class Main : Form
    {
        private ServiceHost host;

        public Main()
        {
            InitializeComponent();

            //var p = SingletonProvider<ProcessWatcher>.UniqueInstance;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(CalculatorService));
            host.Open();

            System.Threading.Timer timer = new  System.Threading.Timer(delegate { SessionManager.RenewSessions(); }, null, 0, 5000);

            //CalculatorService cs=new CalculatorService();
            //var data = cs.GetProcessData("");
            //foreach (SpiderParameter spiderParameter in data)
            //{
            //    //http://www.360buy.com/product/354444.html
            //    var beginIndex = spiderParameter.Url.IndexOf("product");
            //    var endIndex=spiderParameter.Url.IndexOf(".html",beginIndex);
            //    var length = "product/".Length;

            //    try
            //    {
            //        throw new Exception("jjj");
            //        var url = spiderParameter.Url.Substring(beginIndex + length, endIndex - beginIndex-length);
            //        var imageUrl = string.Format("http://jprice.360buyimg.com/price/gp{0}-1-1-3.png", url);
            //        //var price = ImageProcess.Recognize(imageUrl);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
                
            //}



            var p = SingletonProvider<ProcessWatcher>.UniqueInstance;
            //p.StartWatch();
            MessageBox.Show("wcf监听成功");
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            host.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(myProcess==null)
            {
                myProcess=new Process();
            }
            //var recordCount = GetProductsCount();

            //var dt1 = GetProduct(" limit 0," + recordCount / 4);
            //var dt2 = GetProduct(" limit " + recordCount / 4 + "," + recordCount / 2);

            //var dt3 = GetProduct(" limit " + recordCount / 2 + "," + recordCount / 2 + recordCount / 4);
            //var dt4 = GetProduct(" limit " + recordCount / 2 + recordCount / 4 + "," + recordCount);

            
            myProcess.StartInfo.FileName = Environment.CurrentDirectory + "\\DotNet.SpiderApplication.exe";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
        }


        private Process myProcess;

        private Process myProcess2;

        public int GetProductsCount()
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductsCount"))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private DataTable GetProduct(string sqlWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText += sqlWhere;
                }
                return cmd.ExecuteDataTable();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(myProcess!=null)
            {
                myProcess.Kill();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (myProcess2 == null)
            {
                myProcess2 = new Process();
            }
            //var recordCount = GetProductsCount();

            //var dt1 = GetProduct(" limit 0," + recordCount / 4);
            //var dt2 = GetProduct(" limit " + recordCount / 4 + "," + recordCount / 2);

            //var dt3 = GetProduct(" limit " + recordCount / 2 + "," + recordCount / 2 + recordCount / 4);
            //var dt4 = GetProduct(" limit " + recordCount / 2 + recordCount / 4 + "," + recordCount);


            myProcess2.StartInfo.FileName = Environment.CurrentDirectory + "\\DotNet.SpiderApplication.exe";
            myProcess2.StartInfo.CreateNoWindow = true;
            myProcess2.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (myProcess2 != null)
            {
                myProcess2.Kill();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Guid> guids=new List<Guid>();
            guids.Add(SessionManager.CurrentSessionList.FirstOrDefault().Value.SessionID);
            SessionManager.KillSessions(guids);
        }
    }
}


