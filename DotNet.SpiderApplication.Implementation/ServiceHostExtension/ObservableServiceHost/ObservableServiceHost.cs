using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DotNet.SpiderApplication.Service
{
	/// <summary>
	/// Provides a host for services 
	/// and adds an event for when a new <see cref="InstanceContext"/> of the service is created.
	/// </summary>
	public class ObservableServiceHost : ServiceHost
	{
		/// <summary>
		/// Occurs when a new <see cref="InstanceContext"/> is created.
		/// </summary>
		public event EventHandler<InstanceEventArgs> InstanceCreated;

		protected internal virtual void OnInstanceCreated(
			InstanceContext instanceContext)
		{
			EventHandler<InstanceEventArgs> handler = InstanceCreated;
			if (handler != null)
			{
				handler(this, new InstanceEventArgs(instanceContext));
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableServiceHost"/> class.
		/// </summary>
		/// <param name="singletonInstance">The instance of the hosted service.</param>
		/// <param name="baseAddresses">An <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the hosted service.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="singletonInstance"/> is null.</exception>
		public ObservableServiceHost(
			object singletonInstance, 
			params Uri[] baseAddresses) : base(singletonInstance, baseAddresses)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableServiceHost"/> class.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="baseAddresses">The base addresses.</param>
		public ObservableServiceHost(
			Type serviceType,
			params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
		}

		protected override void OnOpening()
		{
			AddInstanceContextInitializers();

			base.OnOpening();
		}

		private void AddInstanceContextInitializers()
		{
			// For each non-metadata endpoint we want to add an InstanceCreationEndpointBehavior
			foreach (ServiceEndpoint endpoint in this.Description.Endpoints)
			{
				if (endpoint.Contract.ContractType != typeof(IMetadataExchange))
				{
					endpoint.Behaviors.Add(new InstanceCreationEndpointBehavior(this));
				}
			}
		}
	}
}
