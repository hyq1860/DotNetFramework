using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace DotNet.SpiderApplication.Service
{
	public class InstanceCreationInitializer : IInstanceContextInitializer
	{
		private ObservableServiceHost m_ObservableServiceHost;

		#region IInstanceContextInitializer Members
		/// <summary>
		/// Provides the ability to modify the newly created <see cref="T:System.ServiceModel.InstanceContext"/> object.
		/// </summary>
		/// <param name="instanceContext">The system-supplied instance context.</param>
		/// <param name="message">The message that triggered the creation of the instance context.</param>
		public void Initialize(
			InstanceContext instanceContext,
			Message message)
		{
			if (m_ObservableServiceHost != null)
			{
				// Fire event that instance was created
				m_ObservableServiceHost.OnInstanceCreated(instanceContext);
			}
		}
		#endregion IInstanceContextInitializer Member

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceCreationInitializer"/> class.
		/// </summary>
		/// <param name="observableServiceHost">The observable service host.</param>
		public InstanceCreationInitializer(
			ObservableServiceHost observableServiceHost)
		{
			m_ObservableServiceHost = observableServiceHost;
		}
	}
}
