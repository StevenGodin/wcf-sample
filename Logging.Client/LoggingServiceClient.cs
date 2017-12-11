using System.Collections.Generic;
using System.ServiceModel;
using Logging.Services;
using Logging.Shared;

namespace Logging.Client
{
	public class LoggingServiceClient : ClientBase<ILoggingService>, ILoggingService
	{
		public void LogMessage(LogMessage message)
		{
			Channel.LogMessage(message);
		}

		public List<LogMessage> DumpLog(LogLevel logLevel)
		{
			return Channel.DumpLog(logLevel);
		}

		public void ClearLog()
		{
			Channel.ClearLog();
		}
	}
}