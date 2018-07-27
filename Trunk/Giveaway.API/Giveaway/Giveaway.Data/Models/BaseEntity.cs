using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Giveaway.Data.Models
{
	public abstract class BaseEntity : IEntity
	{
		[Key]
        [DataMember(Name = "id")]
		public Guid Id { get; set; }

        [DefaultValue(false)]
        [DataMember(Name = "isDeleted")]
		public bool IsDeleted { get; set; }

        [Required]
        [DataMember(Name = "createdTime")]
		public DateTimeOffset CreatedTime { get; set; }

        [Required]
        [DataMember(Name = "updatedTime")]
		public DateTimeOffset UpdatedTime { get; set; }
    }
}