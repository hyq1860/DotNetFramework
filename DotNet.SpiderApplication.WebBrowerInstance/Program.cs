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
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // IOC的注入
            BootStrapperManager.Initialize(new NinjectBootstrapper());

            WebBrowerManager.Instance.Setup(new cEXWB());
            WebBrowerManager.Instance.FilterRequest = true;
            WebBrowerManager.Instance.FilterAction.Add(
                ".css",
                (string key, string source) =>
                    {
                        if (source.EndsWith(key))
                {
                    return true;
                }

                return false;
            });
            WebBrowerManager.Instance.FilterAction.Add(
                ".aspx",
                (string key, string source) =>
                    {
                        if (source.Contains(key))
                {
                    return true;
                }

                return false;
                    });
            WebBrowerManager.Instance.FilterAction.Add(
                ".ashx",
                (string key, string source) =>
                    {
                        if (source.Contains(key))
                {
                    return true;
                }

                return false;
                    });
            WebBrowerManager.Instance.FilterAction.Add(
                "http://wiki.360buy.com",
                (string key, string source) =>
                    {
                        if (source.StartsWith(key))
                {
                    return true;
                }

                return false;
                    });
            WebBrowerManager.Instance.FilterAction.Add(
                "http://chat.360buy.com",
                (string key, string source) =>
                    {
                        if (source.StartsWith(key))
                {
                    return true;
                }

                return false;
            });

            while (Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024 <100)
            {
                Console.WriteLine(Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024);
                try
                {
                    var common = CommonBootStrapper.GetInstance<ICommonSpider>();
                    var spiderTasks = common.GetSpiderTask(20);
                    if (spiderTasks == null || spiderTasks.Count == 0)
                    {
                        Process.GetCurrentProcess().Kill();
                        return;
                    }

                    int i = 0;
                    var serverToClient = CommonBootStrapper.GetInstance<ISpiderClientToManageClient>();
                    foreach (var spiderProductInfo in spiderTasks)
                    {
                        i++;
                        var document = WebBrowerManager.Instance.Brower(spiderProductInfo.Url);
                        serverToClient.TransferData(new SpiderResult()
                        {
                            Url = document.Url,
                            TaskCount = spiderTasks.Count,
                            Current = i,
                            HtmlSource = document.HtmlSource,
                            Title = document.Title,
                            Elapse = document.Elapse,
                            Charset = document.Encoding
                        });
                        if (i == 1)
                        {
                            serverToClient.ReportIEVersion(WebBrowerManager.Instance.IEVersion);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }

            Process.GetCurrentProcess().Kill();
        }

        #region 废弃
        public static List<SpiderTask> GetSpiderTask(int count)
        {
            string serviceAddress = "net.pipe://127.0.0.1/GetSpiderTask";
            using (var channelFactory = new ChannelFactory<ICommonSpider>(new NetNamedPipeBinding(), new EndpointAddress(serviceAddress)))
            {
                ICommonSpider spider = channelFactory.CreateChannel();
                try
                {
                    return spider.GetSpiderTask(count);
                }
                catch (TimeoutException)
                {
                    (spider as ICommunicationObject).Abort();
                    throw;
                }
                catch (CommunicationException)
                {
                    (spider as ICommunicationObject).Abort();
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)spider);
                }
            }
        }

        public static  void ReportState(SpiderResult result)
        {
            using (var factory = new ChannelFactory<ISpiderClientToManageClient>(new NetNamedPipeBinding() { MaxReceivedMessageSize = int.MaxValue }, new EndpointAddress("net.pipe://127.0.0.1/Server")))
            {
                ISpiderClientToManageClient manageClientToManageSpiderClientChannel = factory.CreateChannel();
                try
                {
                    manageClientToManageSpiderClientChannel.TransferData(result);
                }
                catch (TimeoutException)
                {
                    (manageClientToManageSpiderClientChannel as ICommunicationObject).Abort();
                    throw;
                }
                catch (CommunicationException)
                {
                    (manageClientToManageSpiderClientChannel as ICommunicationObject).Abort();
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)manageClientToManageSpiderClientChannel);
                }
            }
        }

        /// <summary>
        /// 报告ie版本设置
        /// </summary>
        /// <param name="ieVersion"></param>
        public static void ReportIEVersion(string ieVersion)
        {
            using (var factory = new ChannelFactory<ISpiderClientToManageClient>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://127.0.0.1/Server")))
            {
                ISpiderClientToManageClient manageClientToManageSpiderClientChannel = factory.CreateChannel();
                try
                {
                    manageClientToManageSpiderClientChannel.ReportIEVersion(ieVersion);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    CloseChannel((ICommunicationObject)manageClientToManageSpiderClientChannel);
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
        #endregion
    }
}
