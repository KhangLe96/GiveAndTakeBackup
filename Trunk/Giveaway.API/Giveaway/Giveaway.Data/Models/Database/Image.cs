using System;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
    public class Image : BaseEntity
    {
        [Required]
        public string ImageUrl { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
