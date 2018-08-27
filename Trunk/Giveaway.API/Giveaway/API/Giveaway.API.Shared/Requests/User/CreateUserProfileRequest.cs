using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.User
{
    [DataContract]
    public class CreateUserProfileRequest : UserProfileRequest
    {
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}