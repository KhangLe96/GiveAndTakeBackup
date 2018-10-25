using Giveaway.Data.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DefaultValue(RequestStatus.Pending)]
        public RequestStatus RequestStatus { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
