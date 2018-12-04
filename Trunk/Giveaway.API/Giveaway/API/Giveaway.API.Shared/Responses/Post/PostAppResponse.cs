using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Post
{
	[DataContract]
    public class PostAppResponse : PostBaseResponse
    {
        [DataMember(Name = "commentCount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "commentCount")]
        public int CommentCount { get; set; }

        [DataMember(Name = "requestCount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "requestCount")]
        public int RequestCount { get; set; }

	    [DataMember(Name = "isCurrentUserRequested", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "isCurrentUserRequested")]
	    public bool IsCurrentUserRequested { get; set; }

	    [DataMember(Name = "requestStatus", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "requestStatus")]
	    public string RequestStatus { get; set; }
	}
}
