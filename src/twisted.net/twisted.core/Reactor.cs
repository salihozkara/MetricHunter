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
using System.Net;
using System.Net.Sockets;

namespace Twisted.Core
{
	public class SelectReactor : IReactor
	{
		private delegate void SocketActionDelegate(Socket socket);
		
		private Dictionary<Socket, IFactory> _listeners = new Dictionary<Socket, IFactory>();
		private Dictionary<Socket, Protocol> _connections = new Dictionary<Socket, Protocol>();
		private List<Socket> _pendingWrites = new List<Socket>();
		private bool _running = false;
		private byte[] _buffer = new byte[1048576];
		
		/// <summary>
		/// Makes the reactor listen on ip:port. For each incoming connection factory.buildProtocol() will be called.
		/// </summary>
		/// <param name="ip">
		/// A <see cref="IPAddress"/>
		/// </param>
		/// <param name="port">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="factory">
		/// A <see cref="IFactory"/>
		/// </param>
		public void ListenTcp(IPAddress ip, int port, IFactory factory)
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind(new IPEndPoint(ip, port));
			socket.Listen(100);
			this._listeners.Add(socket, factory);
		}
		
		/// <summary>
		/// Makes the reactor connect to ip:port.
		/// </summary>
		/// <param name="ip">
		/// A <see cref="IPAddress"/>
		/// </param>
		/// <param name="port">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="factory">
		/// A <see cref="IFactory"/>
		/// </param>
		public void ConnectTcp(IPAddress ip, int port, IFactory factory)
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(new IPEndPoint(ip, port));
			this.AddConnection(socket, factory);
		}
		
		/// <summary>
		/// Makes the reactor start processing networking events
		/// </summary>
		public void Run()
		{
			this._running = true;
			while (this._running)
			{
				List<Socket> readSockets = new List<Socket>();
				readSockets.AddRange(this._listeners.Keys);
				readSockets.AddRange(this._connections.Keys);
				List<Socket> writeSockets = new List<Socket>();
				writeSockets.AddRange(this._pendingWrites);
				List<Socket> errorSockets = new List<Socket>();
				errorSockets.AddRange(this._connections.Keys);
				Socket.Select(readSockets, writeSockets, errorSockets, 100000);
				ProcessSockets(readSockets, CheckAcceptOrRead);
				ProcessSockets(writeSockets, ProcessWrite);
				ProcessSockets(errorSockets, ProcessError);
			}
		}
		
		/// <summary>
		/// Makes the reactor stop processing networking events
		/// </summary>
		public void Stop()
		{
			this._running = false;
		}
		
		private void ProcessSockets(IEnumerable<Socket> sockets, SocketActionDelegate action)
		{
			foreach(Socket socket in sockets)
				action(socket);
		}
		
		private void CheckAcceptOrRead(Socket socket)
		{
			if (this._listeners.ContainsKey(socket))
				AcceptConnection(socket);
			else
				ProcessRead(socket);
		}
		
		private void AcceptConnection(Socket socket)
		{
			Socket connection = socket.Accept();
			IFactory factory = this._listeners[socket];
			this.AddConnection(connection, factory);
		}
		
		private void AddConnection(Socket connection, IFactory factory)
		{
			Protocol protocol = factory.BuildProtocol();
			protocol.SetWritesPendingCallback(this.WritesPending);
			protocol.Socket = connection;
			protocol.ConnectionMade();
			this._connections.Add(connection, protocol);
		}
				
		private void ProcessRead(Socket socket)
		{
			Protocol protocol = this._connections[socket];
			int bytesReceived = socket.Receive(this._buffer);
			if (bytesReceived == 0)
			{
				protocol.ConnectionLost();
				this._connections.Remove(socket);
				socket.Close();
			}
			else
			{
				byte[] buf = new byte[bytesReceived];
				Array.Copy(this._buffer, buf, bytesReceived);
				protocol.DataReceived(buf);
			}
		}

		private void ProcessWrite(Socket socket)
		{
			Protocol protocol;
		 	try
			{
				protocol = this._connections[socket];
			}
			catch (KeyNotFoundException)
			{
				this._pendingWrites.Remove(socket);
				return;
			}
			byte[] buf = protocol.PendingWrites.Dequeue();
			socket.Send(buf);
			if ( protocol.PendingWrites.Count == 0 )
				this._pendingWrites.Remove(socket);
		}
		
		private void ProcessError(Socket socket)
		{
			throw new NotImplementedException();
		}
		
		private void WritesPending(Protocol protocol)
		{
			if (! this._pendingWrites.Contains(protocol.Socket))
				this._pendingWrites.Add(protocol.Socket);
		}	
	}
}
