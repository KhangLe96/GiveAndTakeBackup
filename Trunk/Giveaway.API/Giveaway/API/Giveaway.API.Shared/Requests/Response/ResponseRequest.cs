using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Response
{
	public class ResponseRequest
	{
		[DataMember(Name = "requestId")]
		[JsonProperty(PropertyName = "requestId")]
		public Guid RequestId { get; set; }

		[DataMember(Name = "responseMessage")]
		[JsonProperty(PropertyName = "responseMessage")]
		public string ResponseMessage { get; set; }
	}
}
