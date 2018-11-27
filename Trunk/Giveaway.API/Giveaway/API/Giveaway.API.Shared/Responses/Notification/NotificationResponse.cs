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
		[JsonProperty(PropertyName = "notiMessage")]
		public string Message { get; set; }

		[DataMember(Name = "sourceUserId")]
		[JsonProperty(PropertyName = "sourceUserId")]
		public Guid SourceUserId { get; set; }

		[DataMember(Name = "relevantId")]
		[JsonProperty(PropertyName = "relevantId")]
		public Guid RelevantId { get; set; }

		[DataMember(Name = "type")]
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[DataMember(Name = "isSeen")]
		[JsonProperty(PropertyName = "isSeen")]
		public bool IsSeen { get; set; }

		[DataMember(Name = "isRead")]
		[JsonProperty(PropertyName = "isRead")]
		public bool IsRead { get; set; }

		[DataMember(Name = "createdTime")]
		[JsonProperty(PropertyName = "createdTime")]
		public DateTimeOffset CreatedTime { get; set; }

		[DataMember(Name = "postUrl")]
		[JsonProperty(PropertyName = "postUrl")]
		public string PostUrl { get; set; }

		[DataMember(Name = "avatarUrl")]
		[JsonProperty(PropertyName = "avatarUrl")]
		public string AvatarUrl { get; set; }
	}
}
