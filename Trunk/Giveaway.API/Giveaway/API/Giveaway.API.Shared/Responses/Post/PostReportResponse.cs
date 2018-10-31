using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Post
{
	public class PostReportResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "title")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
