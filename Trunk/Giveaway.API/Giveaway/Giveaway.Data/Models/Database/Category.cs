using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Giveaway.Data.Enums;

namespace Giveaway.Data.Models.Database
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [DefaultValue(CategoryStatus.Enabled)]
        public CategoryStatus CategoryStatus { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
