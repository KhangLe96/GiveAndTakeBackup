using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.Data.EF.DTOs.Requests
{
    public class CategoryRequest
    {
        [DataMember(Name = "categoryName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "categoryImageUrl", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryImageUrl")]
        public string CategoryImageUrl { get; set; }
    }
}