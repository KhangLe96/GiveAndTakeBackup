using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryPostRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "categoryId")]
        public Guid CategoryId { get; set; }

        [DataMember(Name = "provinceCityId")]
        public Guid ProvinceCityId { get; set; }
    }
}
