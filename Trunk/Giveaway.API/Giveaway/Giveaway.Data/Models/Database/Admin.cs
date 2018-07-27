using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.Data.Models.Database
{
	/// <summary>
	/// Models used for declaration of Admin User Type
	/// </summary>
	[Table("Admin")]
	[DataContract]
    public class Admin : BaseEntity
	{
		#region Required Properties

		[Required]
	    [DataMember(Name = "userId")]
		public Guid UserId { get; set; }

        #endregion

        #region Unrequired Properties

	    [DataMember(Name = "user")]
        public virtual User User { get; set; }

        #endregion
    }
}
