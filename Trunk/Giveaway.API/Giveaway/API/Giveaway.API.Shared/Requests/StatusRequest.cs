using System;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class StatusRequest
    {
        [DataMember(Name = "userId")]
        public Guid UserId { get; set; }

        [DataMember(Name = "userStatus")]
        public string UserStatus { get; set; }
    }
}