using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using Logging.Services;
using Logging.Shared;

namespace Logging.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				// Create and start service host reading config from app.config for the LoggingService
				var service = new LoggingService();
				var host = new ServiceHost(service);
				host.Open();
				
				Console.WriteLine("Host Server is running. Press any key to exit");
				Console.ReadKey();
				host.Close();
				WriteLogToFile(service.DumpLog(LogLevel.Info), "LogFile.txt");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Debug.WriteLine(e.Message);
				Console.ReadKey();
			}
		}

		static void WriteLogToFile(IEnumerable<LogMessage> messages, string path)
		{
			using (var sw = File.CreateText(path))
			{
				foreach (var logMessage in messages)
				{
					sw.WriteLine(logMessage);
				}
			}
		}
	}
}