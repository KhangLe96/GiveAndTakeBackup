using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Warning
{
	public class WarningResponse
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
