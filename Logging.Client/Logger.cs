using System;
using Logging.Services;
using Logging.Shared;

namespace Logging.Client
{
	public class Logger
	{
		private readonly ILoggingService _loggingService;

		public Logger(ILoggingService loggingService, string clientId)
		{
			_loggingService = loggingService;
			ClientId = clientId;
		}

		public string ClientId { get; set; }

		public void LogInfo(string message)
		{
			LogMessage(LogLevel.Info, message);
		}

		public void LogWarning(string message)
		{
			LogMessage(LogLevel.Warning, message);
		}

		public void LogError(string message)
		{
			LogMessage(LogLevel.Error, message);
		}

		private void LogMessage(LogLevel level, string message)
		{
			try
			{
				_loggingService.LogMessage(new LogMessage
				{
					ClientId = ClientId,
					LogLevel = level,
					Message = message
				});
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to Log Message. ERROR: " + e.Message);
				throw;
			}
		}
	}
}