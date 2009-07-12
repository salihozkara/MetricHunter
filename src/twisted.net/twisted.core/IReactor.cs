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


using System.Net;

namespace Twisted.Core
{
	public interface IReactor
	{
		void ConnectTcp(IPAddress ip, int port, IFactory factory);
		void ListenTcp(IPAddress ip, int port, IFactory factory);
		void Run();
		void Stop();
	}
}
