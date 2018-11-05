using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class ApiRequestsResponse
    {
        [DataMember(Name = "results")]
        public List<Request> Requests { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}
