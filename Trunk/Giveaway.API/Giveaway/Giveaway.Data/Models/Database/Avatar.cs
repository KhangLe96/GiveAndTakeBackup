using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.Data.Models.Database
{
	[Table("Avatar")]
	[DataContract]
    public class Avatar : BaseEntity
	{
		public string SmallImagePath { get; set; }
		public string MediumImagePath { get; set; }
		public string BigImagePath { get; set; }

		[Required]
		public Guid UserId { get; set; }

		public virtual User User { get; set; }
	}
}
