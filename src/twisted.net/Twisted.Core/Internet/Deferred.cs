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
using System.Collections;
using System.Collections.Generic;

namespace Twisted.Internet
{

	public class Deferred
	{
		private class Callback_
		{
			public Delegate method = null;
			public object[] parameters = null;
			
			public Callback_(Delegate method, object[] parameters)
			{
				this.method = method;
				this.parameters = parameters;
			}
		}
		
		private class CallbackPair
		{
			public Callback_ Callback = null;
			public Callback_ Errback = null;
			
			public CallbackPair(Callback_ callback, Callback_ errback)
			{
				this.Callback = callback;
				this.Errback = errback;
			}
		}
		
		/// <summary>
		/// Default exception type when Errback is called on to a deferred without a specified exception.
		/// </summary>
		public class FailureException : Exception
		{
		}
		
		public class AlreadyCalledException : Exception
		{
		}

		public bool called = false;
		public object result = null;
		private delegate object PassThroughDelegate(object parameter);		
		private Queue<CallbackPair> _callbacks = new Queue<CallbackPair>();
		private bool _runningCallbacks = false;
		
		private object PassThrough(object parameter)
		{
			return parameter;
		}
		
		/// <summary>
		/// Convenience method for adding just a callback.
		/// </summary>
		/// <param name="callback">
		/// A <see cref="Delegate"/>
		/// </param>
		/// <param name="parameters">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <returns>
		/// A <see cref="Deferred"/>
		/// </returns>
		public Deferred AddCallback(Delegate callback, params object[] parameters)
		{
			if (callback == null)
				throw new ArgumentNullException("callbak");
			return this.AddCallbacks(callback, null, parameters, null);
		}
		
		/// <summary>
		/// Convenience method for adding just an errback. 
		/// </summary>
		/// <param name="errback">
		/// A <see cref="Delegate"/>
		/// </param>
		/// <param name="parameters">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <returns>
		/// A <see cref="Deferred"/>
		/// </returns>
		public Deferred AddErrback(Delegate errback, params object[] parameters)
		{
			if (errback == null)
				throw new ArgumentNullException("errback");
			return this.AddCallbacks(new PassThroughDelegate(this.PassThrough), errback, new object[]{}, parameters);
		}
		
		
		/// <summary>
		/// Add a pair of callbacks (success and error) to this Deferred.
		/// 
		/// These will be executed when the 'master' callback is run.
		/// </summary>
		/// <param name="callback">
		/// A <see cref="Delegate"/>
		/// </param>
		/// <param name="errback">
		/// A <see cref="Delegate"/>
		/// </param>
		/// <param name="callbackParameters">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="errbackParameters">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <returns>
		/// A <see cref="Deferred"/>
		/// </returns>
		public Deferred AddCallbacks(Delegate callback, Delegate errback, object[] callbackParameters, object[] errbackParameters)
		{
			if (callback == null)
				throw new ArgumentNullException("callbak");
			if (callbackParameters == null)
				throw new ArgumentNullException("callbackParameters");
			if (errback != null && errbackParameters == null)
				throw new ArgumentNullException("errbackParameters");
			if (errback == null)
			{
				errback = new PassThroughDelegate(this.PassThrough);
				errbackParameters = new object[]{};
			}
			this._callbacks.Enqueue(
			     new CallbackPair(
			          new Callback_(callback, callbackParameters),
			          new Callback_(errback, errbackParameters)));
			if (this.called)
				this.RunCallbacks();
			return this;
		}
		
		/// <summary>
		/// Run all success callbacks that have been added to this Deferred.
		/// Each callback will have its result passed as the first
		/// argument to the next; this way, the callbacks act as a
		/// 'processing chain'. Also, if the success-callback returns a Failure
		/// or raises an Exception, processing will continue on the *error*-
		/// callback chain.
		/// </summary>
		/// <param name="result">
		/// A <see cref="System.Object"/>
		/// </param>
		public void Callback(object result)
		{
			StartRunCallbacks(result);
		}
		
		/// <summary>
		/// Run all error callbacks that have been added to this Deferred.
		/// Each callback will have its result passed as the first
		/// argument to the next; this way, the callbacks act as a
		/// 'processing chain'. Also, if the error-callback returns a non-Failure
		/// or doesn't raise an Exception, processing will continue on the
		/// *success*-callback chain.
		/// </summary>
		public void Errback()
		{
			try
			{
				throw new FailureException();
			}
			catch (FailureException e)
			{
				this.Errback(e);
			}
		}		
		
		/// <summary>
		/// Run all error callbacks that have been added to this Deferred.
		/// Each callback will have its result passed as the first
		/// argument to the next; this way, the callbacks act as a
		/// 'processing chain'. Also, if the error-callback returns a non-Failure
		/// or doesn't raise an Exception, processing will continue on the
		/// *success*-callback chain.
		/// If the argument that's passed to me is not a failure.Failure instance,
		/// it will be embedded in one. If no argument is passed, a failure.Failure
		/// instance will be created based on the current traceback stack.
		/// Passing a string as `fail' is deprecated, and will be punished with
		/// a warning message.
		/// </summary>
		/// <param name="failure">
		/// A <see cref="Exception"/>
		/// </param>
		public void Errback(Exception failure)
		{
			if (failure == null)
				throw new ArgumentNullException("failure");
			this.StartRunCallbacks(failure);
		}
		
		private void StartRunCallbacks(object result)
		{
			if (this.called)
				throw new AlreadyCalledException();
			this.called = true;
			this.result = result;
			this.RunCallbacks();
		}
		
		private void RunCallbacks()
		{
			if (this._runningCallbacks)
				return;
			while (this._callbacks.Count > 0)
			{
				CallbackPair cp = this._callbacks.Dequeue();
				Callback_ c = this.result is Exception ? cp.Errback : cp.Callback;
				try
				{
					this._runningCallbacks = true;
					try
					{
						object[] parameters = new object[c.parameters.Length + 1];
						parameters[0] = this.result;
						Array.Copy(c.parameters, 0, parameters, 1, c.parameters.Length);
						this.result = c.method.DynamicInvoke(parameters);
					}
					finally
					{
						this._runningCallbacks = false;
					}
				}
				catch (Exception e)
				{
					this.result = e;
				}
			}
		}
	}
}
