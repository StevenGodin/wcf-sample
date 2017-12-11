using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using Logging.Shared;

namespace Logging.Services
{
	/// <summary>The backend LoggingService that receives and stores all of the log messages.</summary>
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class LoggingService : ILoggingService
	{
		private readonly CircularBuffer<LogMessage> _messages;

		/// <summary>Creates a new LoggingService using the config file LogBufferSize.</summary>
		public LoggingService()
		{
			// Get LogBufferSize from config file. Set to 100 as the default if not set in config.
			if (!int.TryParse(ConfigurationManager.AppSettings["LogBufferSize"], out var bufferSize))
				bufferSize = 100;

			_messages = new CircularBuffer<LogMessage>(bufferSize);
		}

		/// <summary>Creates a new LoggingService with the specified buffer size.</summary>
		/// <param name="bufferSize">The maximum number of log messages to hold in the service.</param>
		public LoggingService(int bufferSize)
		{
			_messages = new CircularBuffer<LogMessage>(bufferSize);
		}

		/// <summary>Addes a new log message to the buffer.</summary>
		/// <param name="message">The LogMessage to Add.</param>
		public void LogMessage(LogMessage message)
		{
			_messages.Add(message);
		}

		/// <summary>Gets the log messages with the specified minimum LogLevel or higher.</summary>
		/// <param name="minLogLevel">The minimum LogLevel</param>
		public List<LogMessage> DumpLog(LogLevel minLogLevel)
		{
			return _messages.Where(m => m.LogLevel >= minLogLevel).ToList();
		}

		/// <summary>Clears the Log buffer.</summary>
		public void ClearLog()
		{
			_messages.Clear();
		}
	}
}