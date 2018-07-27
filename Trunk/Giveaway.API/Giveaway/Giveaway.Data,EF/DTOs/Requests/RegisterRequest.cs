using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Giveaway.Data.EF.DTOs.Requests
{
	/// <summary>
	/// Used for AuthController
	/// </summary>
	[DataContract]
	public class RegisterRequest
	{
		[DataMember(Name="familyName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "familyName")]
		public string FamilyName { get; set; }

		[DataMember(Name="givenName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "givenName")]
		public string GivenName { get; set; }

		[DataMember(Name="username", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

	    [DataMember(Name = "code", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "code")]
	    public string Code { get; set; }

        [DataMember(Name="mobilephone", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "mobilephone")]
		public string MobilePhone { get; set; }

		[DataMember(Name="birthday", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "birthday")]
		public DateTime Birthday { get; set; }

		[DataMember(Name="email")]
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[DataMember(Name="password", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "password")]
		public string Password { get; set; }

		[DataMember(Name="passwordRetype", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "passwordRetype")]
		public string PasswordRetype { get; set; }

		[DataMember(Name="registerAs", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "registerAs")]
		public string RegisterAs { get; set; }

	    [DataMember(Name = "activityClass", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "activityClass")]
	    public string ActivityClass { get; set; }
    }
}
