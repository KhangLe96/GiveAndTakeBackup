using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class Request
	{
		[DataMember(Name = "requestMessage")]
		public string RequestMessage { get; set; }

		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		[DataMember(Name = "postId")]
		public string PostId { get; set; }
	}
}