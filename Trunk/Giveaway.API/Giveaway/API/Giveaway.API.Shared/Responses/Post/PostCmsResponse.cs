using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.Post
{
    [DataContract]
    public class PostCmsResponse : PostBaseResponse
    {
        [DataMember(Name = "statusCMS")]
        [JsonProperty(PropertyName = "statusCMS")]
        public string EntityStatus { get; set; }
    }
}
