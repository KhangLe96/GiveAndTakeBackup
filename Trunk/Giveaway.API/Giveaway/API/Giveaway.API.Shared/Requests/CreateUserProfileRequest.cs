using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class CreateUserProfileRequest : UserProfileRequest
    {
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}