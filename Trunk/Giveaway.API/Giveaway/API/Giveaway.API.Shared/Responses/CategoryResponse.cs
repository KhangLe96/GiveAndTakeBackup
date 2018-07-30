using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses
{
    public class CategoryResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "categoryName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "imageUrl", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "createdTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }
    }
}
