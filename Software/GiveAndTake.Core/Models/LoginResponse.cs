using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember(Name = "profile", EmitDefaultValue = false)]
        public User Profile { get; set; }

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