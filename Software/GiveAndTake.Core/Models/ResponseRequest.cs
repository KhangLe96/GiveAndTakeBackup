using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class ResponseRequest
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "responseMessage")]
        public string ResponseMessage { get; set; }

        [DataMember(Name = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }
    }
}
