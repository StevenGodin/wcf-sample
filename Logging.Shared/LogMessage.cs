using System.Runtime.Serialization;

namespace Logging.Shared
{
	[DataContract]
	public class LogMessage
	{
		[DataMember]
		public string ClientId { get; set; }

		[DataMember]
		public LogLevel LogLevel { get; set; }

		[DataMember]
		public string Message { get; set; }
	}
}