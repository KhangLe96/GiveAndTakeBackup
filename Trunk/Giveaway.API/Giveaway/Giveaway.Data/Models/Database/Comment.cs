using System;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
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
