// -----------------------------------------------------------------------
// <copyright file="HttpClientTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using DotNet.Web;

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;

    using Amib.Threading;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class HttpClientTest
    {
        public static void Test()
        {
            //var urls = new List<string>();
            ////urls.Add("http://blogz.sohu.com/index/c13981.shtml");
            ////urls.Add("http://www.cnblogs.com/kwklover/archive/2007/01/22/627173.html");
            ////urls.Add("http://www.soxuan.com");
            //urls.Add("http://www.google.com.hk/");
            //urls.Add("http://www.cnblogs.com");
            //urls.Add("http://www.miniclip.com/games/en/");
            //urls.Add("http://www.baidu.com");
            //urls.Add("http://www.360buy.com");
            //foreach (string url in urls)
            //{
            //    HttpClient client=new HttpClient(url);
            //    client.Timeout = 30000;
            //    string html=client.Request();
            //    Console.Write(html);
            //}

            var test1 = new WaitForIdleExample();
            var states = new List<string>();
            states.Add("http://www.360buy.com/product/222701.html");
            states.Add("http://www.360buy.com/product/682747.html");
            states.Add("http://www.360buy.com/product/222704.html");
            states.Add("http://www.360buy.com/product/222701.html");
            states.Add("http://www.360buy.com/product/354444.html");
            states.Add("http://www.360buy.com/product/352655.html");
            states.Add("http://www.360buy.com/product/481284.html");
            states.Add("http://www.360buy.com/product/563181.html");
            states.Add("http://www.360buy.com/product/481245.html");
            states.Add("http://www.360buy.com/product/673975.html");
            System.Net.ServicePointManager.DefaultConnectionLimit = 500; 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //test1.DoWork(states.ToArray());
            //foreach (string state in states)
            //{
            //    test1.DoSomeWork(state);
            //}
            test1.DoWork(states.ToArray());
            sw.Stop();
            Console.Write(sw.ElapsedMilliseconds);
            Console.Read();
        }
    }

    public class WaitForIdleExample
    {
        public static List<string> Data = new List<string>();
        public void DoWork(object[] states)
        {
            STPStartInfo stp = new STPStartInfo();
            stp.DisposeOfStateObjects = true;
            stp.CallToPostExecute = CallToPostExecute.Always;
            stp.ThreadPriority = ThreadPriority.Highest;
            stp.UseCallerCallContext = true;
            stp.MaxWorkerThreads = 20;
            SmartThreadPool smartThreadPool = new SmartThreadPool();
            smartThreadPool.MaxThreads = states.Length;
            smartThreadPool.MinThreads = 1;
            foreach (object state in states)
            {
                IWorkItemResult i = smartThreadPool.QueueWorkItem(new
                    WorkItemCallback(this.DoSomeWork), state);
                Data.Add(i.Result.ToString());
            }

            // Wait for the completion of all work items
            smartThreadPool.WaitForIdle();

            smartThreadPool.Shutdown();
        }

        public object DoSomeWork(object state)
        {
            // Do the work
            Console.WriteLine("Thread:{0}; State:{1}", Thread.CurrentThread.ManagedThreadId, state);
            Thread.Sleep(1000);
            //var hc = new HttpClient(state.ToString());
            //return hc.Request();
            //var wc = new WebClient();
            //return wc.DownloadString(state.ToString());
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}
