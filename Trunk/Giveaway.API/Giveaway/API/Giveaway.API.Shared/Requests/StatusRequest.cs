using System;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class StatusRequest
    {
        [DataMember(Name = "status")]
        public string UserStatus { get; set; }
    }
}