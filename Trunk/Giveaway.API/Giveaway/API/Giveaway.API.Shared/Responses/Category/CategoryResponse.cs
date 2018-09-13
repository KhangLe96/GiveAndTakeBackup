using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses.Category
{
    public class CategoryResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "categoryName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "categoryImageUrl", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryImageUrl")]
        public string CategoryImageUrl { get; set; }

        [DataMember(Name = "status")]
        [JsonProperty(PropertyName = "status")]
        public string EntityStatus { get; set; }

        [DataMember(Name = "createdTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "backgroundColor", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
