using Newtonsoft.Json;
using System.Runtime.Serialization;

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
