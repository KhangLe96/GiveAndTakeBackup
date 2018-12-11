using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Giveaway.API.Shared.Responses.Image;
using Giveaway.API.Shared.Responses.User;

namespace Giveaway.API.Shared.Responses.Post
{
	[DataContract]
	public class PostRequestResponse
	{
		[DataMember(Name = "id")]
		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "images")]
		[JsonProperty(PropertyName = "images")]
		public List<ImageResponse> Images { get; set; }

		[DataMember(Name = "user")]
		[JsonProperty(PropertyName = "user")]
		public UserPostResponse User { get; set; }
	}
}
