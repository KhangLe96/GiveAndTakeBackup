using Giveaway.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.API.Shared.Models.DTO
{
	[Table("Avatar")]
	public class Avatar : BaseEntity
	{
		public string SmallImagePath { get; set; }
		public string MediumImagePath { get; set; }
		public string BigImagePath { get; set; }

		[Required]
		public Guid UserId { get; set; }

		public User User { get; set; }
	}
}
