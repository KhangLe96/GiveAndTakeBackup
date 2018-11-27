using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Responses.User;
using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Response
{
	[DataContract]
    public class ResponseRequestResponse
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "responseMessage")]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

		[DataMember(Name = "user")]
	    public UserRequestResponse User { get; set; }

	    [DataMember(Name = "post")]
	    public PostRequestResponse Post { get; set; }
	}
}
