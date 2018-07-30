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
		[JsonProperty(PropertyName = "profile")]
		public UserProfileResponse Profile { get; set; }


	    [DataMember(Name = "token", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "access_token")]
	    public string Token { get; set; }

	    [DataMember(Name = "tokenType", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "token_type")]
	    public string TokenType { get; set; }

	    [DataMember(Name = "expiresIn", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "expires_in")]
	    public DateTimeOffset ExpiresIn { get; set; }

	    [DataMember(Name = "refreshToke", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "refresh_token")]
	    public string RefreshToken { get; set; }
    }
}
