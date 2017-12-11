using System.Runtime.Serialization;

namespace Logging.Shared
{
	/// <summary>Represent a single log message.</summary>
	[DataContract]
	public class LogMessage
	{
		/// <summary>A string that represents the Client Identity.</summary>
		[DataMember]
		public string ClientId { get; set; }

		/// <summary>The Log Message severity.</summary>
		[DataMember]
		public LogLevel LogLevel { get; set; }

		/// <summary>The log message.</summary>
		[DataMember]
		public string Message { get; set; }

		public override string ToString()
		{
			return $"{ClientId} [{LogLevel}]: {Message}";
		}
	}
}