using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("UserConversation")]
    public class UserConversation : BaseEntity
    {
        public Guid? UserId { get; set; }
        public Guid? ConversationId { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual User User { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}