using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("WarningMessage")]
    public class WarningMessage : BaseEntity
    {
        public Guid UserId { get; set; }

        [Required]
        public string Message { get; set; }

        public virtual User User { get; set; }
    }
}