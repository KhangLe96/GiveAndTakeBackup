using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "totals")]
        public int Totals { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
