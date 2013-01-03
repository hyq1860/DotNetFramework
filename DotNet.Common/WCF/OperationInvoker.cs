using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;

namespace DotNet.Common
{
    public class OperationInvoker
    {
        public static void Invoke<TChanne>(Action<TChanne> serviceInvocation, TChanne channel)
        {
            ICommunicationObject communicationObject = (ICommunicationObject)channel;
            try
            {
                serviceInvocation(channel);
                communicationObject.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communicationObject);
            }
        }
        public static TResult Invoke<TChanne, TResult>(Func<TChanne, TResult> serviceInvocation, TChanne channel)
        {
            ICommunicationObject communicationObject = (ICommunicationObject)channel;
            TResult result = default(TResult);
            try
            {
                result = serviceInvocation(channel);
                communicationObject.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex, communicationObject);
            }
            return result;
        }
        public static void HandleException(Exception ex, ICommunicationObject channel)
        {
            if (ex is TimeoutException || ex is CommunicationException)
            {
                channel.Abort();
            }
            throw ex;
        }
    }
}
