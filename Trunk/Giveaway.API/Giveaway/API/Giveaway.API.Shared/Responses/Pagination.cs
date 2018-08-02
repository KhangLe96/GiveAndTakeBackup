using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "totals")]
        public int Total { get; set; }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
    }
}
