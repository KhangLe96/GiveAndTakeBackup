using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
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
