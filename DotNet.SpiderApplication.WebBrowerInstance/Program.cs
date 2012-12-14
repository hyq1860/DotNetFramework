using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DotNet.BasicSpider;
using DotNet.IoC;
using DotNet.SpiderApplication.Contract;
using DotNet.SpiderApplication.Contract.Entity;
using DotNet.SpiderApplication.Contract.WCF;
using csExWB;

namespace DotNet.SpiderApplication.WebBrowerInstance
{
    
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());
            WebBrowerManager.Instance.Setup(new cEXWB());
            string l_serviceAddress = "net.pipe://127.0.0.1/GetSpiderTask";
            ChannelFactory<ISpiderServer> l_factory = new ChannelFactory<ISpiderServer>(new NetNamedPipeBinding(), new EndpointAddress(l_serviceAddress));
            ISpiderServer server = l_factory.CreateChannel();
            try
            {
                var data= server.GetSpiderTask(5);

                foreach (var spiderProductInfo in data)
                {
                 //   var html=WebBrowerManager.Instance.Run(data.FirstOrDefault().Url);
                 //server.SpiderProductDetail(new SpiderProductInfo() { Url = data.FirstOrDefault().Url });
                    var ver = SpiderManager.SpiderProductDetail(new SpiderProductInfo() { ECPlatformId = spiderProductInfo.ECPlatformId, Url = spiderProductInfo.Url, ProductId = spiderProductInfo.ProductId });
                    CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().Update(ver);
                    ReportState(new SpiderState(){Url=ver.Url});
                }
                 
            }
            catch (Exception epnfex)
            {
                File.WriteAllText("z:\\1.txt", epnfex.Message);
            }

            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
            Console.ReadKey();
        }

        public static  void ReportState(SpiderState state)
        {
            using (ChannelFactory<IServerToClient> factory = new ChannelFactory<IServerToClient>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://127.0.0.1/Server")))
            {
                IServerToClient clientToServerChannel = factory.CreateChannel();
                try
                {
                    clientToServerChannel.ReportStatus(state);
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    CloseChannel((ICommunicationObject)clientToServerChannel);
                }
            }
        }

        private static  void CloseChannel(ICommunicationObject channel)
        {
            try
            {
                channel.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                channel.Abort();
            }
        }
    }
}
