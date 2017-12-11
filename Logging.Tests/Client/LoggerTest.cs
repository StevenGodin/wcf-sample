using System;
using Logging.Client;
using Logging.Services;
using Logging.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Logging.Tests.Client
{
	[TestClass]
	public class LoggerTest
	{
		private const string MESSAGE = "A Test Message";

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorThrowsExceptionWithNullLoggingService()
		{
			var logger = new Logger(null, "Test");
		}

		[TestMethod]
		public void ConstructorSetsFieldsAndProperties()
		{
			const string clientId = "Test";
			var loggingService = new Mock<ILoggingService>().Object;
			var logger = new Logger(loggingService, clientId);

			// Create PrivateObject of logger to check backing field.
			var privateLogger = new PrivateObject(logger);

			Assert.AreEqual(loggingService, privateLogger.GetField("_loggingService"));
			Assert.AreEqual(clientId, logger.ClientId);
		}

		[TestMethod]
		public void LogInfoCallsLogMessageWithInfoLevelAndMessage()
		{
			VerifyLoggerActionCallsLogMessageWithExpectedLevel
				(l => l.LogInfo(MESSAGE), LogLevel.Info);
		}

		[TestMethod]
		public void LogWarningCallsLogMessageWithWarningLevel()
		{
			VerifyLoggerActionCallsLogMessageWithExpectedLevel
				(l => l.LogWarning(MESSAGE), LogLevel.Warning);
		}

		[TestMethod]
		public void LogErrorCallsLogMessageWithErrorLevel()
		{
			VerifyLoggerActionCallsLogMessageWithExpectedLevel
				(l => l.LogError(MESSAGE), LogLevel.Error);
		}

		private void VerifyLoggerActionCallsLogMessageWithExpectedLevel
			(Action<Logger> loggerAction, LogLevel expectedLogLevel)
		{
			var mockLoggingService = new Mock<ILoggingService>();
			var logger = new Logger(mockLoggingService.Object, "Test");

			loggerAction.Invoke(logger);

			// Verify the message object passed to LogMessage has the expected values
			mockLoggingService.Verify(ls => ls.LogMessage(It.Is<LogMessage>(msg =>
				msg.ClientId == logger.ClientId &&
				msg.LogLevel == expectedLogLevel &&
				msg.Message == MESSAGE)));
		}

		[TestMethod]
		public void VerifyLoggerCatchesLoggingServiceException()
		{
			var mockLoggingService = new Mock<ILoggingService>();
			var logger = new Logger(mockLoggingService.Object, "Test");
			mockLoggingService.Setup(ls => ls.LogMessage(It.IsAny<LogMessage>()))
				.Throws<Exception>();

			logger.LogInfo(MESSAGE);

			// Verify No Exception thrown
			// Note that this test doesn't verify that the message was
			// written to the console.
		}
	}
}