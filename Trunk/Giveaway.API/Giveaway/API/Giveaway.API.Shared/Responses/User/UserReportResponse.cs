using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.User
{
    public class UserReportResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "username", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
