using Giveaway.Data.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Requests.Request
{
    [DataContract]
    public class PagingQueryRequestPostRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "requestStatus")]
        public RequestStatus RequestStatus { get; set; }
    }
}
