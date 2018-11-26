using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Notification
{
	[DataContract]
	public class NotificationRequest
	{
		[DataMember(Name = "message")]
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }

		//[DataMember(Name = "id")]
		//[JsonProperty(PropertyName = "id")]
		//public Guid SourceUserId { get; set; }

		//[DataMember(Name = "relevantId")]
		//[JsonProperty(PropertyName = "relevantId")]
		//public Guid RelevantId { get; set; }

		//[DataMember(Name = "type")]
		//[JsonProperty(PropertyName = "type")]
		//public NotificationType Type { get; set; }
	}
}
