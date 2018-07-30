using System;
using System.Runtime.Serialization;
using Giveaway.Data.Enums;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Requests
{
	[DataContract]
	public class UserProfileRequest
	{
		[DataMember(Name = "familyName", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "familyName")]
		public string FamilyName { get; set; }

		[DataMember(Name = "givenName", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "givenName")]
		public string GivenName { get; set; }

		[DataMember(Name = "gender", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "gender")]
		public Gender Gender { get; set; }

		[DataMember(Name = "mobilephone", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "mobilephone")]
		public string Mobilephone { get; set; }

		[DataMember(Name = "birthdate", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "birthdate")]
		public DateTime BirthDate { get; set; }

		[DataMember(Name = "email", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[DataMember(Name = "address", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "address")]
		public string Address { get; set; }

		[DataMember(Name = "introduction", EmitDefaultValue = false)]
		[JsonProperty(PropertyName = "introduction")]
		public string Introduction { get; set; }
	}

    [DataContract]
    public class SpecificUserProfileRequest : UserProfileRequest
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
