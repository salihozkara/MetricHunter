// <license type='lgpl' version='2.1'>
// 
// Copyright (c) 2009 Geert Audenaert
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2 of the Lesser GNU General 
// Public License as published by the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this program; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.
// 
// </license'>

using System;
using System.Collections.Generic;
using System.Threading;

namespace Twisted.Internet
{
	public abstract class ReactorBase : Interfaces.IReactorCore
	{
		internal class NoDeferredScheduledException : Exception
		{
		}
		
		private class CallLaterItem
		{
			public Deferred deferred;
			public object result;
			
			public CallLaterItem(Deferred deferred, object result)
			{
				this.deferred = deferred;
				this.result = result;
			}
		}
		
		private static Dictionary<Thread, ReactorBase> _reactors = new Dictionary<Thread, ReactorBase>();
		private SortedList<long, CallLaterItem> _callLaterItems = new SortedList<long, CallLaterItem>();
		
		protected ReactorBase()
		{
			if (!_reactors.ContainsKey(Thread.CurrentThread))
				_reactors.Add(Thread.CurrentThread, this);
		}
		
		public abstract void Run();
		
		public abstract void Stop();
		
		/// <summary>
		/// Executes the callback of the deferred in timeout milliseconds.
		/// result is the first parameter of the first callback in the deferred.
		/// </summary>
		/// <param name="deferred">
		/// A <see cref="Deferred"/>
		/// </param>
		/// <param name="result">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="timeout">
		/// A <see cref="System.Int32"/>
		/// </param>
		public void CallLater(Deferred deferred, object result, int timeout)
		{
			this._callLaterItems.Add((timeout * TimeSpan.TicksPerMillisecond) + DateTime.Now.Ticks, new CallLaterItem(deferred, result));
		}
		
		protected int TicksToNextCallLater()
		{
			List<int> indexesToBeRemoved = new List<int>();
			try
			{
				int i = 0;
				foreach (KeyValuePair<long, CallLaterItem> kvp in this._callLaterItems)
				{
					long timeout = kvp.Key - DateTime.Now.Ticks;
					if (timeout <= 0)
					{
						indexesToBeRemoved.Add(i);
						kvp.Value.deferred.Callback(kvp.Value.result);
					}
					else
						return (int)timeout;
					i++;
				}
				return -1;
			}
			finally
			{
				for (int i = indexesToBeRemoved.Count - 1; i >= 0 ; i--)
					this._callLaterItems.RemoveAt(indexesToBeRemoved[i]);
			}
		}
		
		/// <value>
		/// Returns the running reactor from the current thread or null if there is none.
		/// </value>
		public static Interfaces.IReactorCore Reactor
		{
			get
			{
				if (_reactors.ContainsKey(Thread.CurrentThread))
				    return _reactors[Thread.CurrentThread];
				return null;
			}
		}
	}
}
