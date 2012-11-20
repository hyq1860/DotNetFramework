using System;
using System.ServiceModel;

namespace DotNet.SpiderApplication.Service
{
	public class InstanceEventArgs : EventArgs
	{
		/// <summary>
		/// Gets or sets the instance context.
		/// </summary>
		/// <value>The instance context.</value>
		public InstanceContext InstanceContext
		{
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceEventArgs"/> class.
		/// </summary>
		public InstanceEventArgs()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InstanceEventArgs"/> class.
		/// </summary>
		/// <param name="instanceContext">The instance context.</param>
		public InstanceEventArgs(
			InstanceContext instanceContext)
		{
			InstanceContext = instanceContext;
		}
	}
}
