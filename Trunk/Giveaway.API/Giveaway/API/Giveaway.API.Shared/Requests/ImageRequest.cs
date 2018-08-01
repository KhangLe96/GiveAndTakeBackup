using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    public class ImageRequest
    {
        [DataMember(Name = "imageUrl")]
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
