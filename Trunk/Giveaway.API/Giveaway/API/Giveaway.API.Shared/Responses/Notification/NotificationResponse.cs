using Giveaway.Data.Enums;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses.Notification
{
	[DataContract]
	public class NotificationResponse
	{
		[DataMember(Name = "id")]
		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "message")]
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }

		[DataMember(Name = "sourceUserId")]
		[JsonProperty(PropertyName = "sourceUserId")]
		public Guid SourceUserId { get; set; }

		[DataMember(Name = "relevantId")]
		[JsonProperty(PropertyName = "relevantId")]
		public Guid RelevantId { get; set; }

		[DataMember(Name = "type")]
		[JsonProperty(PropertyName = "type")]
		public NotificationType Type { get; set; }

		[DataMember(Name = "status")]
		[JsonProperty(PropertyName = "status")]
		public NotificationStatus Status { get; set; }

		[DataMember(Name = "backgroundColor")]
		[JsonProperty(PropertyName = "backgroundColor")]
		public string BackgroundColor { get; set; }
	}
}
