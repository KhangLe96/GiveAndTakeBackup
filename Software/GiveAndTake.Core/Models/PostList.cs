using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class PostList
    {
        [DataMember(Name = "results")]
        public List<Post> Posts { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}