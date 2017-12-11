using Logging.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests.Shared
{
	[TestClass]
	public class LogMessageTest
	{
		[TestMethod]
		public void VerifyToString()
		{
			var msg = new LogMessage
			{
				ClientId = "Client",
				LogLevel = LogLevel.Warning,
				Message = "A Test Message"
			};

			Assert.AreEqual("Client [Warning]: A Test Message", msg.ToString());
		}
	}
}