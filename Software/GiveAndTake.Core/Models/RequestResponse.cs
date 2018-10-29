using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class RequestResponse
	{
		[DataMember(Name = "requestId")]
		public string RequestId { get; set; }

		[DataMember(Name = "responseMessage")]
		public string ResponseMessage { get; set; }
	}
}
