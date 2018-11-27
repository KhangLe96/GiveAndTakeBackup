using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Notification
{
	public class NotificationIsSeenRequest
	{
		[DataMember(Name = "isSeen")]
		[JsonProperty(PropertyName = "isSeen")]
		public bool IsSeen { get; set; }
	}
}
