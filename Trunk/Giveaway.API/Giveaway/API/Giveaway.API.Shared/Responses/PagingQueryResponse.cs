using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses
{
    [DataContract]
    public class PagingQueryResponse
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
