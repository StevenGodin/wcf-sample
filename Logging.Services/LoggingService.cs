using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Logging.Shared;

namespace Logging.Services
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class LoggingService : ILoggingService
	{
		private readonly List<LogMessage> _messages = new List<LogMessage>();

		public void LogMessage(LogMessage message)
		{
			_messages.Add(message);
		}

		public List<LogMessage> DumpLog(LogLevel logLevel)
		{
			return _messages.Where(m => m.LogLevel >= logLevel).ToList();
		}

		public void ClearLog()
		{
			_messages.Clear();
		}
	}
}