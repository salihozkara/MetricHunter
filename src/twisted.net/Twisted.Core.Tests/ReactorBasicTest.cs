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
using System.Text;
using System.Net;
using NUnit.Framework;
using Twisted.Internet;
using Twisted.Internet.Interfaces;

namespace Twisted.Core.Tests
{
	public class EchoServerProtocol : Protocol
	{
		public override void ConnectionMade ()
		{
			base.ConnectionMade ();
			Console.WriteLine("{0} Connection Made", this.GetHashCode());
			Write(ASCIIEncoding.ASCII.GetBytes("Hello there!\n"));
		}
		
		public override void DataReceived (byte[] buf)
		{
			base.DataReceived (buf);
			Write(buf);
		}
		
		public override void ConnectionLost ()
		{
			base.ConnectionLost ();
			Console.WriteLine("{0} Connection Lost", this.GetHashCode());
		}
	}
	
	public class EchoClientProtocol : Protocol
	{
		public override void ConnectionMade ()
		{
			base.ConnectionMade ();
			Console.WriteLine("{0} Connection Made", this.GetHashCode());
		}
		
		public override void DataReceived (byte[] buf)
		{
			base.DataReceived (buf);
			string str = ASCIIEncoding.ASCII.GetString(buf);
			Console.WriteLine(str);
			if (str == "Hello there!\n")
				Write(ASCIIEncoding.ASCII.GetBytes("I'm fine how are you?\n"));
			else if (str == "I'm fine how are you?\n")
				Write(ASCIIEncoding.ASCII.GetBytes("Ok, bye!\n"));
			else if (str == "Ok, bye!\n")
			{
				LooseConnection();
				ReactorBase.Reactor.Stop();
			}
			else
				throw new NUnit.Framework.AssertionException("Echo protocol failed!\nMessage="+str);
		}
		
		public override void ConnectionLost ()
		{
			base.ConnectionLost ();
			Console.WriteLine("{0} Connection Lost", this.GetHashCode());
		}
	}	
	
	public class ServerFactory : IFactory
	{
		public Protocol BuildProtocol()
		{
			return new EchoServerProtocol();
		}
	}
	
	public class ClientFactory : IFactory
	{
		public Protocol BuildProtocol()
		{
			return new EchoClientProtocol();
		}
	}

	[TestFixture()]
	public class ReactorBasicTest
	{
		
		[Test()]
		public void TestCase()
		{
			SelectReactor reactor = new SelectReactor();
			reactor.ListenTcp(new IPAddress(new byte[]{127,0,0,1}), 1234, new ServerFactory());
			reactor.ConnectTcp(new IPAddress(new byte[]{127,0,0,1}), 1234, new ClientFactory());
			reactor.Run();
		}
	}
}
