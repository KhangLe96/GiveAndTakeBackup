using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Giveaway.Data.Enums;

namespace Giveaway.Data.Models.Database
{
    [Table("Request")]
    public class Request : BaseEntity
    {
        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }

        [Required]
        public string RequestMessage { get; set; }

        [Required]
        public string AcceptMessage { get; set; }

        [Required]
        public RequestStatus RequestStatus { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }

}
