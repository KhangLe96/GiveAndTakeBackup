using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses
{
    public class PostResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "user", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "user")]
        public UserPostRespone User { get; set; }

        [DataMember(Name = "title")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "address")]
        [JsonProperty(PropertyName = "address")]
        public ProvinceCityResponse ProvinceCity { get; set; }

        [DataMember(Name = "images")]
        [JsonProperty(PropertyName = "images")]
        public ICollection<ImageResponse> Images { get; set; }

        [DataMember(Name = "createdTime")]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "statusApp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "statusApp")]
        public string Status { get; set; }

        [DataMember(Name = "statusCMS")]
        [JsonProperty(PropertyName = "statusCMS")]
        public string EntityStatus { get; set; }

        [DataMember(Name = "category")]
        [JsonProperty(PropertyName = "category")]
        public CategoryResponse Category { get; set; }
    }
}
