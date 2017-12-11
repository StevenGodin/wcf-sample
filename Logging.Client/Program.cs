using System;
using System.Linq;
using Logging.Shared;

namespace Logging.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new LoggingServiceClient();
			var logger = new Logger(client, "Sample Client");
			logger.LogInfo("Application Started");

			WriteHelp();
			logger.LogInfo("Wrote initial Help message.");

			// Start Command Loop
			var exit = false;
			while (!exit)
			{
				// Get command from the user
				Console.Write("> ");
				var cmdLine = Console.ReadLine();
				if (cmdLine == null)
					continue;

				// Parse the command and message from the input string
				var cmd = cmdLine.Split(' ').First();
				var message = cmdLine.Substring(cmd.Length).Trim();

				// Execute the specified command
				switch (cmd)
				{
					case "help":
						WriteHelp();
						logger.LogInfo("Displayed Help again.");
						break;
					case "client":
						logger.ClientId = message;
						break;
					case "info":
						logger.LogInfo(message);
						break;
					case "warning":
						logger.LogWarning(message);
						break;
					case "error":
						logger.LogError(message);
						break;
					case "dump":
						// if successfull parse of LogLevel from message string
						if (Enum.TryParse(message, true, out LogLevel level))
						{
							var logs = client.DumpLog(level);
							if (!logs.Any())
								Console.WriteLine(string.Empty);
							foreach (var log in logs)
								Console.WriteLine(log);
						}
						else
						{
							Console.WriteLine("ERROR: Invalid Minimum Log Level specified.");
							logger.LogError("An invalid minimum Log Level was specified for dump.");
						}
						break;
					case "clear":
						client.ClearLog();
						break;
					case "exit":
						exit = true;
						logger.LogWarning("Application is Exiting");
						break;
				}
			}

			logger.LogWarning("Application has Exited");
			client.Close();
		}

		static void WriteHelp()
		{
			Console.WriteLine("List of Commands:");
			Console.WriteLine("help\tDisplay this help message.");
			Console.WriteLine("client\tChange the ClientId name.");
			Console.WriteLine("info\tLog an Info message.");
			Console.WriteLine("warning\tLog a Warning message.");
			Console.WriteLine("error\tLog an Error message.");
			Console.WriteLine("dump\tDump the log.");
			Console.WriteLine("clear\tClear the log.");
			Console.WriteLine("exit\tExit the application.");
		}
	}
}
