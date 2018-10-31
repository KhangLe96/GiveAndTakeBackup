using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Warning
{
	[DataContract]
    public class WarningRequest
    {
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [DataMember(Name = "userId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}
