using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.User
{
    [DataContract]
    public class UserPostResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "username", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
    }
}
