using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Requests.Request
{
    public class RequestPostRequest
    {
        [DataMember(Name = "requestMessage")]
        [JsonProperty(PropertyName = "requestMessage")]
        public string RequestMessage { get; set; }

        [DataMember(Name = "userId")]
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [DataMember(Name = "postId")]
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }
    }
}
