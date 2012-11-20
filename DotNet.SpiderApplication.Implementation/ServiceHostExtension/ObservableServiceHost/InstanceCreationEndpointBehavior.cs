using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DotNet.SpiderApplication.Service
{
	public class InstanceCreationEndpointBehavior : IEndpointBehavior
	{
		private InstanceCreationInitializer m_InstanceCreationInitializer;

		#region IEndpointBehavior Members

		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			// Add the InstanceCreationInitializer
			endpointDispatcher.DispatchRuntime.InstanceContextInitializers.Add(
				m_InstanceCreationInitializer);
		}

		public void Validate(ServiceEndpoint endpoint)
		{
		}

		#endregion IEndpointBehavior Members

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceCreationEndpointBehavior"/> class.
		/// </summary>
		/// <param name="observableServiceHost">The observable service host.</param>
		public InstanceCreationEndpointBehavior(
			ObservableServiceHost observableServiceHost)
		{
			m_InstanceCreationInitializer = new InstanceCreationInitializer(observableServiceHost);
		}

	}
}
