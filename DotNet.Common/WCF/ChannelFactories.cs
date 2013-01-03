using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DotNet.Common
{
public static class ChannelFactories
{
    private static Dictionary<string, ChannelFactory> channelFactories = new Dictionary<string, ChannelFactory>();
    public static ChannelFactory<TChannel> GetFactory<TChannel>(string endpointConfigName)
    {
        if (channelFactories.ContainsKey(endpointConfigName))
        {
            return channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
        }

        lock (channelFactories)
        {
            if (channelFactories.ContainsKey(endpointConfigName))
            {
                return channelFactories[endpointConfigName] as ChannelFactory<TChannel>;
            }
            ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(endpointConfigName);
            channelFactory.Open();
            channelFactories[endpointConfigName] = channelFactory;
            return channelFactory;
        }
    }
}
}
 