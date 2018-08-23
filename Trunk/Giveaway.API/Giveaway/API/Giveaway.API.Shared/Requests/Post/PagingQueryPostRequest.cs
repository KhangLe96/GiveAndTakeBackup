using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Requests.Post
{
    [DataContract]
    public class PagingQueryPostRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "categoryId")]
        public string CategoryId { get; set; }

        [DataMember(Name = "provinceCityId")]
        public string ProvinceCityId { get; set; }
    }
}
