using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.User
{
    [DataContract]
    public class ShortUserResponse
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "userName")]
        public string Username { get; set; }

        [DataMember(Name = "fullName")]
        public string FullName { get; set; }


        [DataMember(Name = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "status")]
        public bool Status { get; set; }
    }
}
