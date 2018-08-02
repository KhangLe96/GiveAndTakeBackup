using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses
{
    [DataContract]
    public class PagingQueryResponse<T>
    {
        [DataMember(Name = "results")]
        public List<T> Data { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}
