using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNet.SpiderApplication.Client
{
    using System.Threading;

    using DotNet.SpiderApplication.Service;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 代码

        ManualResetEvent mReset = new ManualResetEvent(true);

        List<Thread> threads = new List<Thread>();

        public static bool flagExit = false;

        static object locker = new object();

        static int th_cnt = 0;

        void DoWork(object threadParam)
        {
            List<ALink> items = threadParam as List<ALink>;
            foreach (ALink link in items.ToArray())
            {
                mReset.WaitOne();
                if (flagExit) break;
                lock (locker)
                {
                    th_cnt++;
                    UpdateProcess(th_cnt);
                }
                Spider.SuNingProductList(link.Url);
                OutputText(Thread.CurrentThread.Name + ":" + link.Url);
                ShowText(link.Url);

                Thread.Sleep(50);
            }
            if (flagExit)
            {
                OutputText(Thread.CurrentThread.Name + ":exit");
            }
            else
            {
                OutputText(Thread.CurrentThread.Name + ":finlished");
            }
            OutputText(th_cnt.ToString());
        }

        delegate void ControlHandler(string text);
        delegate void ProcessBarHandler(int i);

        void ShowText(string text)
        {
            if (!label1.InvokeRequired)
            {
                label1.Text = text;
            }
            else
            {
                ControlHandler show = new ControlHandler(ShowText);
                BeginInvoke(show, new object[] { text });
            }
        }

        void UpdateProcess(int i)
        {
            if (!progressBar1.InvokeRequired)
            {
                progressBar1.Value = i > progressBar1.Maximum ? progressBar1.Maximum : i;
            }
            else
            {
                ProcessBarHandler show = new ProcessBarHandler(UpdateProcess);
                BeginInvoke(show, new object[] { i });
            }
        }

        static List<T> Clone<T>(IEnumerable<T> oldList)
        {
            return new List<T>(oldList);
        }

        void OutputText(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }

        void ReleaseThread()
        {
            if (threads.Count > 0)
            {
                foreach (Thread th in threads)
                {
                    th.Abort();
                }
                threads.Clear();
            }
        }



        WorkStatus workStatus = WorkStatus.Waitting;



        void SetButtonStatus()
        {
            switch (workStatus)
            {
                case WorkStatus.Working:
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;
                    break;
                case WorkStatus.Pause:
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = true;
                    break;
                case WorkStatus.Stop:
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    break;
                case WorkStatus.Waitting:
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    break;
            }
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
            if (workStatus == WorkStatus.Pause)
            {
                if (threads.Count > 0)
                {
                    mReset.Set();
                }
                workStatus = WorkStatus.Working;
                SetButtonStatus();
                return;
            }

            workStatus = WorkStatus.Working;
            SetButtonStatus();

            List<ALink> links = new List<ALink>();
            var dt = DataAccess.GetProductCategory(" ECPlatformId=4 limit 48,100");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Url"))
                    {
                        var url = Convert.ToString(dr["Url"]);
                        links.Add(new ALink(url));
                        //Spider.SuNingProductList(url);
                    }
                }
            }

            progressBar1.Maximum = links.Count;
            progressBar1.Value = 0;
            th_cnt = 0;

            int dataCount = links.Count;
            int perCount = 25;

            int threadCount = dataCount % perCount == 0 ? dataCount / perCount : dataCount / perCount + 1;

            int limitCount = 3;
            //限制最大线程数量
            if (threadCount > limitCount)
            {
                threadCount = limitCount;
                perCount = dataCount % threadCount == 0 ? dataCount / threadCount : dataCount / threadCount + 1;
            }

            int cnt = 0; int i_cnt = 0;

            List<ALink> perLinks = new List<ALink>();
            //分配资源
            foreach (ALink link in links)
            {
                if (cnt % perCount == 0 && cnt > 0)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(DoWork));
                    thread.Name = "thread" + i_cnt.ToString();
                    thread.Start(Clone(perLinks));
                    threads.Add(thread);
                    i_cnt++;
                    perLinks.Clear();
                }
                perLinks.Add(link);
                cnt++;
            }

            if (perLinks.Count > 0 && i_cnt < threadCount)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(DoWork));
                thread.Name = "thread" + i_cnt.ToString();
                thread.Start(Clone(perLinks));
                threads.Add(thread);
                i_cnt++;
                perLinks.Clear();
            }

            //Thread.Sleep(1000);
            //flagExit = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (workStatus == WorkStatus.Working || workStatus == WorkStatus.Pause)
            {
                ReleaseThread();

                progressBar1.Value = 0;
                progressBar1.Refresh();

                th_cnt = 0;

                workStatus = WorkStatus.Stop;
                SetButtonStatus();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (workStatus == WorkStatus.Working)
            {
                if (threads.Count > 0)
                {
                    mReset.Reset();
                    workStatus = WorkStatus.Pause;
                    SetButtonStatus();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetButtonStatus();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseThread();
        }
    }

    public class ALink
    {
        public string Url = "";
        public ALink(string url)
        {
            this.Url = url;
        }
    }

    enum WorkStatus
    {
        Pause, Stop, Working, Waitting
    }
}
