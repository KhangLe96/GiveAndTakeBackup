using Giveaway.Data.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    public class PostRequest
    {
        //Review: Don't need this prop because we can get userId directly from Controller through token
        [DataMember(Name = "userId")]
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "address", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [DataMember(Name = "postImageUrl", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "postImageUrl")]
        public List<ImageRequest> Images { get; set; }

        [DataMember(Name = "categoryId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryId")]
        public Guid CategoryId { get; set; }

        [DataMember(Name = "provinceCityId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "provinceCityId")]
        public Guid ProvinceCityId { get; set; }
    }
}
