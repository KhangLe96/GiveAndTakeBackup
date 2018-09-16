using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Category
{
    public class CategoryCmsResponse : CategoryBaseResponse
    {
        [DataMember(Name = "backgroundColor", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
