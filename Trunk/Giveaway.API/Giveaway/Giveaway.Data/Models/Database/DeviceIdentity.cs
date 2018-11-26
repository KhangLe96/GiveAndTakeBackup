using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Giveaway.Data.Enums;

//using MySql.Data.EntityFrameworkCore.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
	[Table("DeviceIdentity")]
	public class DeviceIdentity : BaseEntity
	{
		[Required]
		public MobilePlatform MobilePlatform { get; set; }
		[Required]
		public string DeviceToken { get; set; }

		[ForeignKey("User")]
		public Guid UserId { get; set; }
		public virtual User User { get; set; }
	}
}
