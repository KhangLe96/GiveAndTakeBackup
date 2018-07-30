using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses
{
    public class PostResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

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

        [DataMember(Name = "category")]
        [JsonProperty(PropertyName = "category")]
        public CategoryResponse Category { get; set; }
    }
}
