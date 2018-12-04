using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class Response
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
	    public User User { get; set; }

	    [DataMember(Name = "post")]
	    public PostResponse Post { get; set; }
	}
}
