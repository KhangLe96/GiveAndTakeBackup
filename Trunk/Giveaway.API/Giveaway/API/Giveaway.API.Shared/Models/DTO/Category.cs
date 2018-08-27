using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Models.DTO
{
    public class Category
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "categoryName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "categoryImageUrl", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryImageUrl")]
        public string ImageUrl { get; set; }
    }
}
