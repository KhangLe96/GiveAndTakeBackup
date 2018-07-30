using System;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
    public class Image : BaseEntity
    {
        public Guid PostId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual Post Post { get; set; }
    }
}
