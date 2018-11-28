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
		public Guid Id { get; set; }

		[DataMember(Name = "notiMessage")]
		public string Message { get; set; }

		[DataMember(Name = "sourceUserId")]
		public Guid SourceUserId { get; set; }

		[DataMember(Name = "relevantId")]
		public Guid RelevantId { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "isSeen")]
		public bool IsSeen { get; set; }

		[DataMember(Name = "isRead")]
		public bool IsRead { get; set; }

		[DataMember(Name = "createdTime")]
		public DateTimeOffset CreatedTime { get; set; }

		[DataMember(Name = "postUrl")]
		public string PostUrl { get; set; }

		[DataMember(Name = "avatarUrl")]
		public string AvatarUrl { get; set; }
	}
}
