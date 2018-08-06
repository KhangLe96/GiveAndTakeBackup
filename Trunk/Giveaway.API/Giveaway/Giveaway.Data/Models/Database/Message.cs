using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Message")]
    public class Message : BaseEntity
    {
        public Guid UserConversationId { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public virtual UserConversation UserConversation { get; set; }
    }
}