using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Giveaway.Data.Enums;

namespace Giveaway.Data.Models.Database
{
	[Table("Notification")]
	public class Notification : BaseEntity
	{
		[Required]
		public string Message { get; set; }
		[Required]
		public Guid DestinationUserid { get; set; }
		[Required]
		public Guid SourceUserId { get; set; }
		public Guid RelevantId { get; set; }
		[Required]
		public NotificationType Type { get; set; }
	}
}
