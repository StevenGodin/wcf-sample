using System.Linq;
using Logging.Services;
using Logging.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests.Services
{
	[TestClass]
	public class LoggingServiceTest
	{
		[TestMethod]
		public void NewLoggingServiceIsEmpty()
		{
			var loggingService = new LoggingService(20);

			Assert.AreEqual(0, loggingService.DumpLog(LogLevel.Info).Count);
		}

		[TestMethod]
		public void VerifyBufferSizeFromConfig()
		{
			var loggingService = new LoggingService();
			var privateLoggingService = new PrivateObject(loggingService);
			var buffer = (CircularBuffer<LogMessage>)privateLoggingService.GetField("_messages");

			Assert.AreEqual(25, buffer.Size);
		}

		[TestMethod]
		public void DumpInfoReturnsCorrectItems()
		{
			var loggingService = CreateFilledLoggingService();
			var results = loggingService.DumpLog(LogLevel.Info);

			// Assert 8 items returned
			Assert.AreEqual(8, results.Count);
		}

		[TestMethod]
		public void DumpWarningReturnsCorrectItems()
		{
			var loggingService = CreateFilledLoggingService();
			var results = loggingService.DumpLog(LogLevel.Warning);

			// Assert 5 items returned and no items are LogLevel.Info
			Assert.AreEqual(5, results.Count);
			Assert.IsFalse(results.Any(msg => msg.LogLevel == LogLevel.Info));
		}

		[TestMethod]
		public void DumpErrorReturnsCorrectItems()
		{
			var loggingService = CreateFilledLoggingService();
			var results = loggingService.DumpLog(LogLevel.Error);

			// Assert 2 items returned and all items are LogLevel.Error
			Assert.AreEqual(2, results.Count);
			Assert.IsTrue(results.All(msg => msg.LogLevel == LogLevel.Error));
		}

		[TestMethod]
		public void ClearLogClearsTheLog()
		{
			var loggingService = CreateFilledLoggingService();

			// Verify Log is not empty before clearing
			Assert.IsTrue(loggingService.DumpLog(LogLevel.Info).Any());

			loggingService.ClearLog();

			// Verify Log is empty
			Assert.IsFalse(loggingService.DumpLog(LogLevel.Info).Any());
		}

		private LoggingService CreateFilledLoggingService()
		{
			var loggingService = new LoggingService(20);
			loggingService.LogMessage(new LogMessage {ClientId = "Client 1", LogLevel = LogLevel.Info, Message = "Message 1"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 1", LogLevel = LogLevel.Info, Message = "Message 2"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 1", LogLevel = LogLevel.Error, Message = "Message 3"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 2", LogLevel = LogLevel.Error, Message = "Message 4"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 2", LogLevel = LogLevel.Warning, Message = "Message 5"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 2", LogLevel = LogLevel.Warning, Message = "Message 6"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 3", LogLevel = LogLevel.Warning, Message = "Message 7"});
			loggingService.LogMessage(new LogMessage {ClientId = "Client 3", LogLevel = LogLevel.Info, Message = "Message 8"});

			return loggingService;
		}
	}
}