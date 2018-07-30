using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
