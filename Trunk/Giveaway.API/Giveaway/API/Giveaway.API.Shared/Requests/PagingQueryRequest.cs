using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryRequest
    {
        [DataMember(Name = "page")] public int Page { get; set; } = 1;

        [DataMember(Name = "limit")] public int Limit { get; set; } = 10;
    }
}
