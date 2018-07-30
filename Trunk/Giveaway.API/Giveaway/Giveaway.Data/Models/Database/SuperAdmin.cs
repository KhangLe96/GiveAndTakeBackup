using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.Data.Models.Database
{
	/// <summary>
	/// Model used for declaration of SuperAdmin User Type
	/// </summary>
	[Table("SuperAdmin")]
	[DataContract]
    public class SuperAdmin : BaseEntity
	{
		#region Required Properties

		[Required]
		public Guid UserId { get; set; }

		#endregion

		#region Unrequired Properties

		public virtual User User { get; set; }

		#endregion

	}
}
