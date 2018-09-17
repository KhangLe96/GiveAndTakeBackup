using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses.Category
{
    public class CategoryAppResponse : CategoryBaseResponse
    {
        [DataMember(Name = "backgroundColor", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "backgroundColor")]
        public int BackgroundColor { get; set; }
    }
}
