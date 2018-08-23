using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("ProvinceCity")]
    public class ProvinceCity : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string ProvinceCityName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

}
