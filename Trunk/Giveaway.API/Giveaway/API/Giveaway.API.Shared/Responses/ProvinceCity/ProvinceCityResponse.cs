using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.ProviceCity
{
	public class ProvinceCityResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember(Name = "provinceCityName")]
        [JsonProperty(PropertyName = "provinceCityName")]
        public string ProvinceCityName { get; set; }
    }
}
