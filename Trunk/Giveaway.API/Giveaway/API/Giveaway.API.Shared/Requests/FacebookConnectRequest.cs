using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
	[DataContract]
	public class FacebookConnectRequest
	{
		[DataMember(Name = "accessToken")]
		public string AccessToken { get; set; }

		[DataMember(Name = "userId")]
		public string FacebookId { get; set; }
	}
}
