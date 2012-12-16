namespace DotNet.SpiderApplication.WebBrowerInstance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.ServiceModel;

    using DotNet.BasicSpider;
    using DotNet.IoC;
    using DotNet.SpiderApplication.Contract;
    using DotNet.SpiderApplication.Contract.Entity;
    using DotNet.SpiderApplication.Contract.WCF;

    using csExWB;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());
            WebBrowerManager.Instance.Setup(new cEXWB());
            try
            {
                var data = GetSpiderTask(20);
                int i = 0;
                foreach (var spiderProductInfo in data)
                {
                    i++;
                    var ver = SpiderManager.SpiderProductDetail(new SpiderProductInfo() { ECPlatformId = spiderProductInfo.ECPlatformId, Url = spiderProductInfo.Url, ProductId = spiderProductInfo.ProductId });
                    CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().Update(ver);
                    ReportState(new SpiderState() { Url = ver.Url,TaskCount=data.Count,Current =i });
                }
            }
            catch (Exception epnfex)
            {
                File.WriteAllText("z:\\1.txt", epnfex.Message);
            }

            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }

        public static List<SpiderProductInfo> GetSpiderTask(int count)
        {
            string serviceAddress = "net.pipe://127.0.0.1/GetSpiderTask";
            using (var channelFactory = new ChannelFactory<ISpiderServer>(new NetNamedPipeBinding(), new EndpointAddress(serviceAddress)))
            {
                ISpiderServer server = channelFactory.CreateChannel();
                try
                {
                    return server.GetSpiderTask(count);
                }
                catch (Exception exception)
                {
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)server);
                }
            }
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
                    throw;
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
                throw;
            }
            finally
            {
                channel.Abort();
            }
        }
    }
}
