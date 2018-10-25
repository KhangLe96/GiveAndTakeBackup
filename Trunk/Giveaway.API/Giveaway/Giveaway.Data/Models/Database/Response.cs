using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Response")]
    public class Response : BaseEntity
    {
        [Required]
        public string ResponseMessage { get; set; }

		[ForeignKey("Request")]
	    public Guid RequestId { get; set; }
		public virtual Request Request { get; set; }
    }
}