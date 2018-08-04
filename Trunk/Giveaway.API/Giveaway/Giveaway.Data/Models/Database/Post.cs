using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Giveaway.Data.Enums;

namespace Giveaway.Data.Models.Database
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProvinceCityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public PostStatus PostStatus { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ProvinceCity ProvinceCity { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }

}
