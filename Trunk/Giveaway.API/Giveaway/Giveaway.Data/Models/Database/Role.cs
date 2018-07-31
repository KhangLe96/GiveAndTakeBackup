using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Giveaway.Data.Models.Database
{
    public class Role : BaseEntity
    {
        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}