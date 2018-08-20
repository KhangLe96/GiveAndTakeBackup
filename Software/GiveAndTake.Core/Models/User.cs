using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class BaseUser
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "socialAccountId")]
        public string SocialAccountId { get; set; }

        [DataMember(Name = "avatarUrl")]
        public string AvatarUrl { get; set; }
    }

    [DataContract]
    public class User : BaseUser
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        //[DataMember(Name = "appreciationNumber")]
        //public int AppreciationNumber { get; set; }

        [DataMember(Name = "birthdate")]
        public DateTime BirthDate { get; set; }

        //public string PasswordSalt { get; set; }
        //public string PasswordHash { get; set; }
        //public DateTimeOffset AllowTokensSince { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        public string FullName => LastName + " " + FirstName;

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        //public DateTimeOffset LastLogin { get; set; }
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember(Name = "profile", EmitDefaultValue = false)]
        public User User { get; set; }

        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string Token { get; set; }

        [DataMember(Name = "tokenType", EmitDefaultValue = false)]
        public string TokenType { get; set; }

        [DataMember(Name = "expiresIn", EmitDefaultValue = false)]
        public DateTimeOffset ExpiresIn { get; set; }

        [DataMember(Name = "refreshToken", EmitDefaultValue = false)]
        public string RefreshToken { get; set; }
    }
}