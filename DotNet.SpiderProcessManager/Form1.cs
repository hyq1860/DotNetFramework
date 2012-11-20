using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNet.SpiderProcessManager
{
    using System.Diagnostics;

    using DotNet.Data;

    public partial class Form1 : Form
    {
        private Process myProcess;

        private Process myProcess2;
        public Form1()
        {
            InitializeComponent();
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
    }
}


