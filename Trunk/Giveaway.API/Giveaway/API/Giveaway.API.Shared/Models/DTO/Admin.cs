using Giveaway.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.API.Shared.Models.DTO
{
	/// <summary>
	/// Models used for declaration of Admin User Type
	/// </summary>
	[Table("Admin")]
	public class Admin : BaseEntity
	{
		#region Required Properties

		[Required]
		public Guid UserId { get; set; }

		#endregion

		#region Unrequired Properties

		public User User { get; set; }

		#endregion
	}
}
