using System;
using System.Runtime.Serialization;
using Giveaway.API.Shared.Models;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses
{
	[DataContract]
	public class UserProfileResponse
	{
	    [DataMember(Name = "id")]
	    [JsonProperty(PropertyName = "id")]
	    public Guid Id { get; set; }

        [DataMember(Name="familyName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "familyName")]
		public string FamilyName { get; set; }

		[DataMember(Name="givenName", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "givenName")]
		public string GivenName { get; set; }

	    [DataMember(Name = "code", EmitDefaultValue = false)]
	    [JsonProperty(PropertyName = "code")]
	    public string Code { get; set; }

        [DataMember(Name="gender", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "gender")]
		public string Gender { get; set; }

		[DataMember(Name="username", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		[DataMember(Name="avatar", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "avatar")]
		public string AvatarUrl { get; set; }

		[DataMember(Name="mobilephone", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "mobilephone")]
		public string Mobilephone { get; set; }

		[DataMember(Name="birthdate", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "birthdate")]
		public DateTime? BirthDate { get; set; }

		[DataMember(Name="email", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[DataMember(Name="address", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "address")]
		public string Address { get; set; }

		[DataMember(Name="role", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "role")]
		public string Role { get; set; }

		[DataMember(Name="lastLogin", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "lastLogin")]
		public DateTimeOffset? LastLogin { get; set; }

		[DataMember(Name="joinedAt", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "joinedAt")]
		public DateTimeOffset JoinedAt { get; set; }

		[DataMember(Name="status", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "status")]
		public bool Status { get; set; }

		[DataMember(Name="verifyStatus", EmitDefaultValue=false)]
		[JsonProperty(PropertyName = "verifyStatus")]
		public int VerifyStatus { get; set; }
	}
}
