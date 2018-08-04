using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Feedback")]
    public class Feedback : BaseEntity
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public virtual User User { get; set; }
    }

}
