using Demo.Business;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;

namespace Demo.Client
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			while (true)
			{
				var bytes = Encoding.ASCII.GetBytes(Console.ReadLine());

				var client = new TcpClient();
				await client.ConnectAsync(IPAddress.Loopback, Settings.Port);
				var stream = client.GetStream();
				await stream.WriteAsync(bytes, 0, bytes.Length);
				await stream.FlushAsync();
				await stream.DisposeAsync();
				client.Dispose();
			}
		}
	}
}
