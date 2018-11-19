using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.Post
{
	[DataContract]
	public class PostRequestResponse
	{
		[DataMember(Name = "id")]
		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "image")]
		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }
	}
}
