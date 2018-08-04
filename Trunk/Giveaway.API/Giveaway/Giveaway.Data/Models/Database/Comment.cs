using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        public Guid PostId { get; set; }
        public Guid? UserId { get; set; }

        [Required]
        public string CommentMessage { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
