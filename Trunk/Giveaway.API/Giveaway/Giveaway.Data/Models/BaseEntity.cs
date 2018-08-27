using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;

namespace Giveaway.Data.Models
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DefaultValue(EntityStatus.Activated)]
        [DataMember(Name = "EntityStatus")]
        public EntityStatus EntityStatus { get; set; }

        [Required]
        [DataMember(Name = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [Required]
        [DataMember(Name = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }
    }
}