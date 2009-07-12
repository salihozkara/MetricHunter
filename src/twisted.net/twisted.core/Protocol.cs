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
using System.Net.Sockets;
using System.Collections.Generic;

namespace Twisted.Core
{
	public class Protocol
	{	
		internal delegate void WritesPendingDelegate(Protocol protocol);
		private Queue<byte[]> _pendingWrites = new Queue<byte[]>();
		private WritesPendingDelegate _writesPending = null;
		internal Socket Socket = null;
		
		/// <summary>
		/// Called when a new connection is instantiated
		/// </summary>
		public virtual void ConnectionMade()
		{
		}
		
		/// <summary>
		/// Called when the connection is closed by the other side
		/// </summary>
		public virtual void ConnectionLost()
		{
		}
		
		/// <summary>
		/// Writes buffer buf onto the transport.
		/// </summary>
		/// <param name="buf">
		/// A <see cref="System.Byte"/>
		/// </param>
		public void Write(byte[] buf)
		{
			this.QueueWriteAction(buf);
		}
		
		/// <summary>
		/// Writes count bytes from buffer buf onto the transport starting at position start
		/// </summary>
		/// <param name="buf">
		/// A <see cref="System.Byte"/>
		/// </param>
		/// <param name="start">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="count">
		/// A <see cref="System.Int32"/>
		/// </param>
		public void Write(byte[] buf, int start, int length)
		{
			byte[] newbuf = new byte[length];
			Array.Copy(buf, start, newbuf, 0, length);
			this.QueueWriteAction(buf);
		}
		
		/// <summary>
		/// Ends the current connection.
		/// </summary>
		public void LooseConnection()
		{
		}
		
		/// <summary>
		/// Called when data is received on the transport.
		/// </summary>
		/// <param name="buf">
		/// Data received on the transport <see cref="System.Byte"/>
		/// </param>
		public virtual void DataReceived(byte[] buf)
		{
		}
		
		private void QueueWriteAction(byte[] buf)
		{
			this._pendingWrites.Enqueue(buf);
			if ( this._writesPending != null )
				this._writesPending(this);
		}
		
		internal Queue<byte[]> PendingWrites
		{
			get
			{
				return this._pendingWrites;
			}
		}
		
		internal void SetWritesPendingCallback(WritesPendingDelegate callback)
		{
			this._writesPending = callback;
		}	
	}
}
