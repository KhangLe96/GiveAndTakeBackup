using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Response")]
    public class Response : BaseEntity
    {
        public Guid RequestId { get; set; }

        [Required]
        public string ResponseMessage { get; set; }

        public virtual Request Request { get; set; }
    }
}