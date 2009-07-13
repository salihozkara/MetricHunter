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

namespace Twisted.Internet
{
	public class SelectReactor : ReactorBase, Interfaces.IReactorTCP
	{
		private class ListeningPort : Interfaces.IListeningPort
		{
			private SelectReactor _reactor = null;
			private Socket _socket;
			
			public ListeningPort(SelectReactor reactor, Socket socket)
			{
				this._reactor = reactor;
				this._socket = socket;
			}
				
			public void stopListening()
			{
				this._socket.Close();
				this._reactor._listeners.Remove(this._socket);
			}
		}
		
		private class Connector : Interfaces.IConnector
		{
			private SelectReactor _reactor = null;
			private Socket _socket;

			public Connector(SelectReactor reactor, Socket socket)
			{
				this._reactor = reactor;
				this._socket = socket;
			}
			
			public void disconnect()
			{
				this._socket.Close();
				this._reactor._connections.Remove(this._socket);				
			}
		}
		
		private delegate void SocketActionDelegate(Socket socket);
		
		private Dictionary<Socket, Interfaces.IFactory> _listeners = new Dictionary<Socket, Interfaces.IFactory>();
		private Dictionary<Socket, Protocol> _connections = new Dictionary<Socket, Protocol>();
		private List<Socket> _pendingWrites = new List<Socket>();
		private bool _running = false;
		private byte[] _buffer = new byte[1048576];
		
		public Interfaces.IListeningPort ListenTcp(IPAddress ip, int port, Interfaces.IFactory factory)
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Blocking = false;
			socket.Bind(new IPEndPoint(ip, port));
			socket.Listen(100);
			this._listeners.Add(socket, factory);
			return new ListeningPort(this, socket);
		}
		
		public Interfaces.IConnector ConnectTcp(IPAddress ip, int port, Interfaces.IFactory factory)
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Blocking = true;
			socket.Connect(new IPEndPoint(ip, port));
			this.AddConnection(socket, factory);
			return new Connector(this, socket);
		}
		
		public override void Run()
		{
			base.Run();
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
		
		public override void Stop()
		{
			this._running = false;
			base.Stop();
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
			Interfaces.IFactory factory = this._listeners[socket];
			this.AddConnection(connection, factory);
		}
		
		private void AddConnection(Socket connection, Interfaces.IFactory factory)
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
