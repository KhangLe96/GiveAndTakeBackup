using Giveaway.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.API.Shared.Models.DTO
{
    [Table("VerificationCode")]
    public class VerificationCode : BaseEntity
    {
	    public string VerifiedCode  { get; set; }

	    public Guid UserId { get; set; }

		public User User { get; set; }
    }
}
