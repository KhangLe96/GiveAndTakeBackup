using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class Notification
	{
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "message")]
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
	}
}
