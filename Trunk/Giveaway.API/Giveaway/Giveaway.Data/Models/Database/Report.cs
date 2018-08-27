using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Giveaway.Data.Models.Database
{
    public class Report : BaseEntity
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public Guid PostId { get; set; }

        [ForeignKey("User")]
        public Guid UserId;
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
