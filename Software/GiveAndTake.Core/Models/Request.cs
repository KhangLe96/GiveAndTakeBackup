using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class Request
    {
        [DataMember(Name = "requestMessage")]
        public string RequestMessage { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "postId")]
        public string PostId { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "requestStatus")]
        public string RequestStatus { get; set; }

        [DataMember(Name = "createdTime")]
        public DateTime CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        public DateTime UpdatedTime { get; set; }

        [DataMember(Name = "response")]
        public ResponseRequest Response { get; set; }
    }

	[DataContract]
	public class UserRequest
	{
		[DataMember(Name = "requested")]
		public bool IsRequested { get; set; }
	}
}