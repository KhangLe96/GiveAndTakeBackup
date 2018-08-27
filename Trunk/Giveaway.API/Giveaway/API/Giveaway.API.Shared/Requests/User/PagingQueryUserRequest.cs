using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;

namespace Giveaway.API.Shared.Requests.User
{
    [DataContract]
    public class PagingQueryUserRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}