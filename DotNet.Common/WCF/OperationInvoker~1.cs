using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DotNet.Common
{
    public class OperationInvoker<TChannel> : OperationInvoker
    {
        public string EndpointName { get; private set; }

        public OperationInvoker(string endpointName)
        {
            this.EndpointName = endpointName;
        }
        public void Invoke(Action<TChannel> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory = ChannelFactories.GetFactory<TChannel>(this.EndpointName);
            TChannel channel = channelFactory.CreateChannel();
            Invoke(serviceInvocation, channel);
        }
        public TResult Invoke<TResult>(Func<TChannel, TResult> serviceInvocation)
        {
            ChannelFactory<TChannel> channelFactory = ChannelFactories.GetFactory<TChannel>(this.EndpointName);
            TChannel channel = channelFactory.CreateChannel();
            return Invoke(serviceInvocation, channel);
        }
    }
}
