using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryPostRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "postName")]
        public string PostName { get; set; }
    }
}
