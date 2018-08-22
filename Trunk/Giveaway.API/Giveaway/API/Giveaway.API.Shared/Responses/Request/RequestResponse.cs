using Giveaway.API.Shared.Responses.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.Request
{
    [DataContract]
    public class RequestResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "requestMessage")]
        [JsonProperty(PropertyName = "requestMessage")]
        public string RequestMessage { get; set; }

        [DataMember(Name = "requestStatus")]
        [JsonProperty(PropertyName = "requestStatus")]
        public string RequestStatus { get; set; }

        [DataMember(Name = "response")]
        [JsonProperty(PropertyName = "response")]
        public ResponseRequestResponse Response { get; set; }

        [DataMember(Name = "createdTime")]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "userId")]
        [JsonProperty(PropertyName = "userId")]
        public Guid? UserId { get; set; }

        [DataMember(Name = "postId")]
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }
    }
}
