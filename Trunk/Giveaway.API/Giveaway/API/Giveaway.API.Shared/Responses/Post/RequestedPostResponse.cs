using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses.Post
{
	public class RequestedPostResponse : PostAppResponse
	{
		[DataMember(Name = "requestedPostStatus", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "requestedPostStatus")]
		public string RequestedPostStatus { get; set; }
	}
}
