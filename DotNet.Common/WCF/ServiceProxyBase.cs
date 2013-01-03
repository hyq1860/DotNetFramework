namespace DotNet.Common
{
    public abstract class ServiceProxyBase<TChannel>
    {
        /// <summary>
        /// Gets Invoker.
        /// </summary>
        public OperationInvoker<TChannel> Invoker { get; private set; }

        public ServiceProxyBase(string endpointname)
        {
            this.Invoker = new OperationInvoker<TChannel>(endpointname);
        }
    }
}
