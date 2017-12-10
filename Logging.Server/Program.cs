using System;
using System.Diagnostics;
using System.ServiceModel;
using Logging.Services;

namespace Logging.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				// Create and start service host reading config from app.config for the LoggingService
				var host = new ServiceHost(typeof(LoggingService));
				host.Open();

				Console.WriteLine("Host Server is running. Press any key to exit");
				Console.ReadKey();
				host.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Debug.WriteLine(e.Message);
				Console.ReadKey();
			}
		}
	}
}