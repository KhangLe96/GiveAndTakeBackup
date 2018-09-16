using Giveaway.API.Shared.Responses.Category;
using Giveaway.API.Shared.Responses.Image;
using Giveaway.API.Shared.Responses.ProviceCity;
using Giveaway.API.Shared.Responses.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.Post
{
    [DataContract]
    public class PostBaseResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "user", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "user")]
        public UserPostResponse User { get; set; }

        [DataMember(Name = "title")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "createdTime")]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "images")]
        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageResponse> Images { get; set; }

        [DataMember(Name = "address")]
        [JsonProperty(PropertyName = "address")]
        public ProvinceCityResponse ProvinceCity { get; set; }

        [DataMember(Name = "category")]
        [JsonProperty(PropertyName = "category")]
        public CategoryAppResponse Category { get; set; }

        [DataMember(Name = "statusApp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "statusApp")]
        public string Status { get; set; }
    }
}
