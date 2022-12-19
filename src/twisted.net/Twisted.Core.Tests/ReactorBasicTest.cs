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
		private SelectReactor _reactor = new SelectReactor();
		private bool _testDeferredSuceeded = false;
		
		[Test()]
		public void TestReactorTCP()
		{
			this._reactor.ListenTcp(new IPAddress(new byte[]{127,0,0,1}), 1234, new ServerFactory());
			this._reactor.ConnectTcp(new IPAddress(new byte[]{127,0,0,1}), 1234, new ClientFactory());
			this._reactor.Run();
		}
		
		private Deferred GetDummyData(int x)
		{
			Deferred d = new Deferred();
			ReactorBase.Reactor.CallLater(d, x*3, 2000);
			return d;
		}
		
		private delegate object PrintDataDelegate(object data);
		private object PrintData(object data)
		{
			Console.WriteLine(data.ToString());
			throw new InvalidOperationException();
		}
		
		private object PrintDataError(object data)
		{
			Assert.IsInstanceOfType(typeof(InvalidOperationException), data);
			return "some data";
		}
		
		private object PrintDataSuccess(object data)
		{
			Assert.AreEqual(data, "some data");
			this._testDeferredSuceeded = true;
			return null;
		}
		
		private delegate void StopReactorDelegate(object dummy);
		private void StopReactor(object dummy)
		{
			ReactorBase.Reactor.Stop();
		}
		
		[Test()]
		public void TestDefferedSystem()
		{
			Deferred d = GetDummyData(5);
			d.AddCallback(new PrintDataDelegate(PrintData));
			d.AddErrback(new PrintDataDelegate(PrintDataError));
			Deferred dToChain = new Deferred();
			d.ChainDeferred(dToChain);
			dToChain.AddCallback(new PrintDataDelegate(PrintDataSuccess));
			
			d = new Deferred();
			d.AddCallback(new StopReactorDelegate(StopReactor));
			_reactor.CallLater(d, null, 4000);
			_reactor.Run();
			Assert.IsTrue(this._testDeferredSuceeded);
		}
	}
}
