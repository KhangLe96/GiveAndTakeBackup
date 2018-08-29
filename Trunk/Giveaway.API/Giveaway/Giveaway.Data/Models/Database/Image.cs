using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Image")]
    public class Image : BaseEntity
    {
        [Required]
        public string OriginalImage { get; set; }
        [Required]
        public string ResizedImage { get; set; }
        [Required]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
