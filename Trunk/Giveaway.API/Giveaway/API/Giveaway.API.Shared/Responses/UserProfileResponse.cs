using System;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses
{
	[DataContract]
	public class UserProfileResponse
	{
	    [DataMember(Name = "id")]
	    [JsonProperty(PropertyName = "id")]
	    public Guid Id { get; set; }

        [DataMember(Name="firstName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "firstName")]
		public string FirstName { get; set; }

		[DataMember(Name= "lastName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "lastName")]
		public string LastName { get; set; }

	    [DataMember(Name="birthdate", EmitDefaultValue=false)]
	    [JsonProperty(PropertyName = "birthdate")]
	    public DateTime? BirthDate { get; set; }

	    [DataMember(Name="email", EmitDefaultValue=false)]
	    [JsonProperty(PropertyName = "email")]
	    public string Email { get; set; }

	    [DataMember(Name= "avatarUrl", EmitDefaultValue=false)]
	    [JsonProperty(PropertyName = "avatarUrl")]
	    public string AvatarUrl { get; set; }

	    [DataMember(Name= "phoneNumber", EmitDefaultValue=false)]
	    [JsonProperty(PropertyName = "phoneNumber")]
	    public string PhoneNumber { get; set; }

	    [DataMember(Name="gender", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "gender")]
		public string Gender { get; set; }

	    [DataMember(Name="username", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

	    [DataMember(Name="address", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "address")]
		public string Address { get; set; }

		[DataMember(Name="role", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "role")]
		public string[] Roles { get; set; }

	    [DataMember(Name = "status", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
