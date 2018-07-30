using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses
{
	[DataContract]
	public class FacebookConnectResponse
	{
		[DataMember(Name = "token")]
		public string Token { get; set; }

		[DataMember(Name = "profile")]
		public UserProfileResponse Profile { get; set; }
	}
}
