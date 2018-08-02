using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses
{
    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "totals")]
        public int Total { get; set; }
        //Review: Should use param name is similar as request params
        //[DataMember(Name = "pageNumber")]
        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
