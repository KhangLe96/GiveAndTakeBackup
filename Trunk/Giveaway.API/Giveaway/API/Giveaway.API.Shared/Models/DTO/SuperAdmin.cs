using Giveaway.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.API.Shared.Models.DTO
{
	/// <summary>
	/// Model used for declaration of SuperAdmin User Type
	/// </summary>
	[Table("SuperAdmin")]
	public class SuperAdmin : BaseEntity
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
