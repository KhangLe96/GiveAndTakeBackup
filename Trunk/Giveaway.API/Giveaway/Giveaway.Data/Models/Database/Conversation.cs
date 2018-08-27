using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Giveaway.Data.Models.Database
{
    [Table("Conversation")]
    public class Conversation : BaseEntity
    {
        public virtual List<UserConversation> Conversations { get; set; }
    }
}