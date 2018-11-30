using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class Notification
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

		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "isSeen")]
		public bool IsSeen { get; set; }

		[DataMember(Name = "isRead")]
		public bool IsRead { get; set; }

		[DataMember(Name = "createdTime")]
		public DateTime CreatedTime { get; set; }

		[DataMember(Name = "postUrl")]
		public string PostUrl { get; set; }

		[DataMember(Name = "avatarUrl")]
		public string AvatarUrl { get; set; }
	}
}
