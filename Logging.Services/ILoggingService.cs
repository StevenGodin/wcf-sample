using System.Collections.Generic;
using System.ServiceModel;
using Logging.Shared;

namespace Logging.Services
{
	[ServiceContract]
	public interface ILoggingService
	{
		[OperationContract]
		void LogMessage(LogMessage message);

		[OperationContract]
		List<LogMessage> DumpLog(LogLevel logLevel);

		[OperationContract]
		void ClearLog();
	}
}