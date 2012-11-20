using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConfigChangeWatcher : IConfigChangeWatcher
    {
        private static readonly object _configChangedKey = new object();
		private static int _defaultPollDelayInMilliseconds = 15000;

		private int _pollDelayInMilliseconds = _defaultPollDelayInMilliseconds;
		private Thread _pollingThread;
		private EventHandlerList _eventHandlers = new EventHandlerList();
		private DateTime _lastWriteTime;
		private PollingStatus _pollingStatus;
		private object _lockObj = new object();

        private IConfigParameter configParameter;
        private bool restartAppDomainOnChange = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configParameter"></param>
        public ConfigChangeWatcher(IConfigParameter configParameter)
            : this(configParameter, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configParameter"></param>
        /// <param name="restartAppDomainOnChange"></param>
        public ConfigChangeWatcher(IConfigParameter configParameter, bool restartAppDomainOnChange)
        {
            this.configParameter = configParameter;
            this.restartAppDomainOnChange = restartAppDomainOnChange;
        }

        /// <summary>
        /// Sets the default poll delay.
        /// </summary>
        /// <param name="newDefaultPollDelayInMilliseconds">The new default poll.</param>
		public static void SetDefaultPollDelayInMilliseconds(int newDefaultPollDelayInMilliseconds)
		{
			_defaultPollDelayInMilliseconds = newDefaultPollDelayInMilliseconds;
		}

        /// <summary>
        /// Reset the default to 15000 millisecond.
        /// </summary>
        public static void ResetDefaultPollDelay()
		{
			_defaultPollDelayInMilliseconds = 15000;
		}

        /// <summary>
        /// Sets the poll delay in milliseconds.
        /// </summary>
        /// <param name="newDelayInMilliseconds">
        /// The poll delay in milliseconds.
        /// </param>
		public void SetPollDelayInMilliseconds(int newDelayInMilliseconds)
		{
			_pollDelayInMilliseconds = newDelayInMilliseconds;
		}

		/// <summary>
		/// <para>Allows an <see cref="Common.Configuration.Storage.ConfigurationChangeFileWatcher"/> to attempt to free resources and perform other cleanup operations before the <see cref="Common.Configuration.Storage.ConfigurationChangeFileWatcher"/> is reclaimed by garbage collection.</para>
		/// </summary>
        ~ConfigChangeWatcher()
		{
			Disposing(false);
		}

        /// <summary>
        /// Event raised when the underlying persistence mechanism for configuration notices that
        /// the persistent representation of configuration information has changed.
        /// </summary>
        public event ConfigChangedEventHandler Changed
        {
            add { _eventHandlers.AddHandler(_configChangedKey, value); }
            remove { _eventHandlers.RemoveHandler(_configChangedKey, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IConfigParameter GetParameter 
        { 
            get { return this.configParameter; } 
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RestartAppDomainOnChange
        {
            get { return this.restartAppDomainOnChange; }
            set { this.restartAppDomainOnChange = value; }
        }

		/// <summary>
		/// <para>Starts watching the configuration file.</para>
		/// </summary>
		public void StartWatching()
		{
			lock (_lockObj)
			{
				if (_pollingThread == null)
				{
					_pollingStatus = new PollingStatus(true);
					_pollingThread = new Thread(new ParameterizedThreadStart(Poller));
					_pollingThread.IsBackground = true;
					_pollingThread.Name = this.GetParameter.GroupName;
					_pollingThread.Start(_pollingStatus);
				}
			}
		}

		/// <summary>
		/// <para>Stops watching the configuration file.</para>
		/// </summary>
		public void StopWatching()
		{
			lock (_lockObj)
			{
				if (_pollingThread != null)
				{
					_pollingStatus.Polling = false;
					_pollingStatus = null;
					_pollingThread = null;
				}
			}
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		public void Dispose()
		{
			Disposing(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="Common.Configuration.Storage.ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		/// <param name="isDisposing">
		/// <para><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</para>
		/// </param>
		protected virtual void Disposing(bool isDisposing)
		{
			if (isDisposing)
			{
				_eventHandlers.Dispose();
				StopWatching();
			}
		}

		/// <summary>
		/// <para>Raises the <see cref="Changed"/> event.</para>
		/// </summary>
		protected virtual void OnChanged()
		{
            ConfigChangedEventHandler callbacks = (ConfigChangedEventHandler)_eventHandlers[_configChangedKey];
            ConfigChangedEventArgs eventData = this.BuildEventData();

            try
            {
                if (callbacks != null)
                {
                    foreach (ConfigChangedEventHandler callback in callbacks.GetInvocationList())
                    {
                        if (callback != null)
                        {
                            callback(this, eventData);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogException(e);
            }
		}

		private void LogException(Exception e)
		{
			try
			{
				EventLog.WriteEntry(GetEventSourceName(), R.ExceptionEventRaisingFailed + GetType().FullName + " :" + e.Message, EventLogEntryType.Error);

			}
			catch
			{
				// Just drop this on the floor. If sending it to the EventLog failed, there is nowhere
				// else for us to send it. Sorry!
			}
		}

		/// <summary>
		/// <para>Returns the <see cref="DateTime"/> of the last change of the information watched</para>
		/// </summary>
		/// <returns>The <see cref="DateTime"/> of the last modificaiton, or <code>DateTime.MinValue</code> if the information can't be retrieved</returns>
		protected abstract DateTime GetCurrentLastWriteTime();     

        /// <summary>
        /// Builds the change event data, in a suitable way for the specific watcher implementation
        /// </summary>
        /// <returns>The change event information</returns>
        protected abstract ConfigChangedEventArgs BuildEventData();

		/// <summary>
		/// Returns the source name to use when logging events
		/// </summary>
		/// <returns>The event source name</returns>
		protected abstract string GetEventSourceName();

		private void Poller(object parameter)
		{
			_lastWriteTime = DateTime.MinValue;
			DateTime currentLastWriteTime = DateTime.MinValue;
			PollingStatus pollingStatus = (PollingStatus)parameter;

			while (pollingStatus.Polling)
			{
				currentLastWriteTime = GetCurrentLastWriteTime();
				if (currentLastWriteTime != DateTime.MinValue)
				{
					// might miss a change if a change occurs before it's ran for the first time.
					if (_lastWriteTime.Equals(DateTime.MinValue))
					{
						_lastWriteTime = currentLastWriteTime;
					}
					else
					{
						if (_lastWriteTime.Equals(currentLastWriteTime) == false)
						{
							_lastWriteTime = currentLastWriteTime;
							OnChanged();
						}
					}
				}
				Thread.Sleep(_pollDelayInMilliseconds);		
			}
		}

		private class PollingStatus
		{
			private bool polling;

			public PollingStatus(bool polling)
			{
				this.polling = polling;
			}

			public bool Polling
			{
				get { return polling; }
				set { polling = value; }
			}
		}
    }
}