/********************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 12/23/2008 
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace DotNet.Common.IO
{
	public class FileSystemChangeEventHandler
	{

		private class FileChangeEventArg
		{
			private object sender;
			private FileSystemEventArgs argument;

			public FileChangeEventArg(object sender, FileSystemEventArgs arg)
			{
				this.sender = sender;
				this.argument = arg;
			}

			public object Sender
			{
				get { return sender; }
			}
			public FileSystemEventArgs Argument
			{
				get { return argument; }
			}
		}

		private object syncObject;

		private Dictionary<string, Timer> timers;
		private int timeout;
		private bool isFolderChange;

		public event FileSystemEventHandler ActualHandler;

		private FileSystemChangeEventHandler()
		{
			syncObject = new object();
			timers = new Dictionary<string, Timer>(StringComparer.InvariantCultureIgnoreCase);
		}

		public FileSystemChangeEventHandler(int timeout)
			: this(timeout, false)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemChangeEventHandler"/> class.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <param name="isFolderChange">if set to <c>true</c> [if the Watcher is to folder change].</param>
		public FileSystemChangeEventHandler(int timeout, bool isFolderChange)
			: this()
		{
			this.timeout = timeout;
			this.isFolderChange = isFolderChange;
		}

		public void ChangeEventHandler(object sender, FileSystemEventArgs e)
		{
			lock (syncObject)
			{
				Timer t;

				string watchPath;

				if (isFolderChange)
				{
					watchPath = Path.GetDirectoryName(e.FullPath);
				}
				else
				{
					watchPath = e.FullPath;
				}

				// disable the existing timer
				if (timers.ContainsKey(watchPath))
				{
					t = timers[watchPath];
					t.Change(Timeout.Infinite, Timeout.Infinite);
					t.Dispose();
				}

				// add a new timer
				if (ActualHandler != null)
				{
					t = new Timer(TimerCallback, new FileChangeEventArg(sender, e), timeout, Timeout.Infinite);
					timers[watchPath] = t;
				}
			}
		}

		private void TimerCallback(object state)
		{
			FileChangeEventArg arg = state as FileChangeEventArg;
			ActualHandler(arg.Sender, arg.Argument);
		}
	}
}