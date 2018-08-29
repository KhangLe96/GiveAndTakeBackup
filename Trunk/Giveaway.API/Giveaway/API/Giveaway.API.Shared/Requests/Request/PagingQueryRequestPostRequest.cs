using Giveaway.Data.Enums;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Request
{
    [DataContract]
    public class PagingQueryRequestPostRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "requestStatus")]
        public RequestStatus RequestStatus { get; set; }
    }
}
