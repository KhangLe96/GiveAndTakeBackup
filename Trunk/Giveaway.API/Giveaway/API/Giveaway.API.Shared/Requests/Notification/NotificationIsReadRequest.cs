using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Notification
{
	public class NotificationIsReadRequest
	{
		[DataMember(Name = "isRead")]
		[JsonProperty(PropertyName = "isRead")]
		public bool IsRead { get; set; }
	}
}
