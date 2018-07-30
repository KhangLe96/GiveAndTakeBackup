using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
    public class ProvinceCity : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string ProvinceCityName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

}
