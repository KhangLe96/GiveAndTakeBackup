using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses
{
	/// <summary>
	/// Used in API/AuthController
	/// </summary>
	[DataContract]
	public class LoginResponse
	{
		[DataMember(Name="profile", EmitDefaultValue=false)]
		public UserProfileResponse Profile { get; set; }

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
