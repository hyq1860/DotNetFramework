using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

using DotNet.BasicSpider;
using DotNet.IoC;
using DotNet.SpiderApplication.Contract.Entity;
using DotNet.SpiderApplication.Contract.WCF;
using csExWB;

namespace DotNet.SpiderApplication.WebBrowerInstance
{
    using System.IO;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());
            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.FilterRequest = true;
            WebBrowerManager.Instance.FilterAction.Add(".css", (string key, string source) =>
            {
                if (source.EndsWith(key))
                {
                    return true;
                }
                return false;
            });
        //http://counter.360buy.com/aclk.aspx?key=p523868

            WebBrowerManager.Instance.FilterAction.Add(".aspx", (string key, string source) =>
            {
                if (source.Contains(key))
                {
                    //File.WriteAllText(@"z:\1\"+source+".txt",source);
                    return true;
                }
                return false;
            });
            WebBrowerManager.Instance.FilterAction.Add(".ashx", (string key, string source) =>
            {
                if (source.Contains(key))
                {
                    return true;
                }
                return false;
            });
            WebBrowerManager.Instance.FilterAction.Add("http://wiki.360buy.com", (string key, string source) =>
            {
                if (source.StartsWith(key))
                {
                    return true;
                }
                return false;
            });
            WebBrowerManager.Instance.FilterAction.Add("http://chat.360buy.com", (string key, string source) =>
            {
                if (source.StartsWith(key))
                {
                    return true;
                }
                return false;
            });
            try
            {
                var data = GetSpiderTask(20);
                int i = 0;
                foreach (var spiderProductInfo in data)
                {
                    i++;
                    var document = WebBrowerManager.Instance.Brower(spiderProductInfo.Url);
                    //var ver = SpiderManager.SpiderProductDetail(new SpiderProductInfo() { ECPlatformId = spiderProductInfo.ECPlatformId, Url = spiderProductInfo.Url, ProductId = spiderProductInfo.ProductId });
                    //CommonBootStrapper.ServiceLocator.GetInstance<IProductService>().Update(ver);
                    ReportState(
                        new SpiderResult()
                            {
                                Url = document.Url,
                                TaskCount = data.Count,
                                Current = i,
                                HtmlSource = document.HtmlSource,
                                Title = document.Title,
                                Elapse = document.Elapse,
                                Charset = document.Encoding
                            });
                    if (i == 1)
                    {
                        ReportIEVersion(WebBrowerManager.Instance.IEVersion);
                    }
                }
            }
            catch (Exception exception)
            {
                //File.WriteAllText("z:\\1.txt", epnfex.Message);
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
                catch (TimeoutException)
                {
                    (server as ICommunicationObject).Abort();
                    throw;
                }
                catch (CommunicationException)
                {
                    (server as ICommunicationObject).Abort();
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)server);
                }
            }
        }

        public static  void ReportState(SpiderResult result)
        {
            using (var factory = new ChannelFactory<IServerToClient>(new NetNamedPipeBinding() { MaxReceivedMessageSize = int.MaxValue }, new EndpointAddress("net.pipe://127.0.0.1/Server")))
            {
                IServerToClient clientToServerChannel = factory.CreateChannel();
                try
                {
                    clientToServerChannel.TransferData(result);
                }
                catch (TimeoutException)
                {
                    (clientToServerChannel as ICommunicationObject).Abort();
                    throw;
                }
                catch (CommunicationException)
                {
                    (clientToServerChannel as ICommunicationObject).Abort();
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)clientToServerChannel);
                }
            }
        }

        /// <summary>
        /// 报告ie版本设置
        /// </summary>
        /// <param name="ieVersion"></param>
        public static void ReportIEVersion(string ieVersion)
        {
            using (var factory = new ChannelFactory<IServerToClient>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://127.0.0.1/Server")))
            {
                IServerToClient clientToServerChannel = factory.CreateChannel();
                try
                {
                    clientToServerChannel.ReportIEVersion(ieVersion);
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

        /// <summary>
        /// </summary>
        /// <param name="channel">
        /// The channel.
        /// </param>
        private static void CloseChannel(ICommunicationObject channel)
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
