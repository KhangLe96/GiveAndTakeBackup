using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryRequest
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
