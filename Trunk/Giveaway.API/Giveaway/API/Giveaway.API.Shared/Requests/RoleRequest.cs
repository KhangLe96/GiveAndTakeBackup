using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class RoleRequest
    {
        [DataMember(Name = "role")]
        public string Role { get; set; }
    }
}