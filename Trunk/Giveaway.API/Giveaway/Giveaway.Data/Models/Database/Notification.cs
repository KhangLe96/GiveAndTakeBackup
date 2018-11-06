using Giveaway.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
	[Table("Notification")]
	public class Notification : BaseEntity
	{
		[Required]
		public string Message { get; set; }
		[Required]
		public Guid DestinationUserId { get; set; }
		[Required]
		public Guid SourceUserId { get; set; }
		public Guid RelevantId { get; set; }
		[Required]
		public NotificationType Type { get; set; }
		[Required]
		public NotificationStatus Status { get; set; }
		[Required]
		public string BackgroundColor { get; set; }
	}
}
