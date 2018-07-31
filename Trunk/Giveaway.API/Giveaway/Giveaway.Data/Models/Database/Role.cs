using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Role")] //Review: remember add this table name
    public class Role : BaseEntity
    {
        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}