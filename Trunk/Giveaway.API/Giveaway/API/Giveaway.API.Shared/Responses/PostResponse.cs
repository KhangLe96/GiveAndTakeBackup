using Giveaway.Data.Models.Database;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

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


        [DataMember(Name = "createdTime")]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "category")]
        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }
    }
}
