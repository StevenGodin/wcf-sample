using System;
using Logging.Services;
using Logging.Shared;

namespace Logging.Client
{
	/// <summary>
	/// A simple logging class to simply send log messages using a logging service.
	/// </summary>
	/// <remarks>
	/// Allows you to call LogInfo, LogWarning, LogError to 
	/// quickly log messages to the logging service, similar to log4net.
	/// This logger will also catch any exceptions that the Logging service
	/// might throw when a message is logged.
	/// </remarks>
	public class Logger
	{
		private readonly ILoggingService _loggingService;

		/// <summary>Creates a new Logger.</summary>
		/// <param name="loggingService">The Logging service to use.</param>
		/// <param name="clientId">The initial ClientId to use.</param>
		public Logger(ILoggingService loggingService, string clientId)
		{
			_loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
			ClientId = clientId;
		}

		/// <summary>The ClientId to use in all Log Messages.</summary>
		public string ClientId { get; set; }

		/// <summary>Sends an Info message to the logging service.</summary>
		/// <param name="message">The message text to log.</param>
		public void LogInfo(string message)
		{
			LogMessage(LogLevel.Info, message);
		}

		/// <summary>Sends a Warning message to the logging service.</summary>
		/// <param name="message">The message text to log.</param>
		public void LogWarning(string message)
		{
			LogMessage(LogLevel.Warning, message);
		}

		/// <summary>Sends an Error message to the logging service.</summary>
		/// <param name="message">The message text to log.</param>
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
			}
		}
	}
}