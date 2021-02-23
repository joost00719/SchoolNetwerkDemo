using Demo.Business;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Demo.Server
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{

			while (true)
			{
				var listener = new TcpListener(IPAddress.Loopback, Settings.Port);
				listener.Start();
				Byte[] bytes;
				TcpClient client = listener.AcceptTcpClient();

				NetworkStream ns = client.GetStream();
				if (client.ReceiveBufferSize > 0)
				{

					bytes = new byte[client.ReceiveBufferSize];
					ns.Read(bytes, 0, client.ReceiveBufferSize);
					string msg = new string(Encoding.UTF8.GetString(bytes, 0, bytes.Length).TakeWhile(x => x != '\0').Reverse().ToArray());

					Console.WriteLine(msg);
				}

				listener.Stop();
				client.Dispose();
			}
		}
	}
}
