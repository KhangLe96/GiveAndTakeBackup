using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Giveaway.API.Shared.Responses;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Requests
{
    public class PostRequest
    {
        [DataMember(Name = "title")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "address")]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [DataMember(Name = "postImageUrl")]
        [JsonProperty(PropertyName = "postImageUrl")]
        public string PostImageUrl { get; set; }

        [DataMember(Name = "categoryId")]
        [JsonProperty(PropertyName = "categoryId")]
        public Guid CategoryId { get; set; }

        [DataMember(Name = "provinceCityId")]
        [JsonProperty(PropertyName = "provinceCityId")]
        public Guid ProvinceCityId { get; set; }

        //public Guid UserId { get; set; }

        //public Guid PostStatusId { get; set; }
    }
}
