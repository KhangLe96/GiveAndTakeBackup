using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.User
{
    [DataContract]
	public class UserProfileRequest
	{
	    [DataMember(Name = "firtName")]
	    public string FirstName { get; set; }

	    [DataMember(Name = "lastName")]
	    public string LastName { get; set; }

	    [DataMember(Name = "userName")]
	    public string UserName { get; set; }

        [DataMember(Name = "birthDate")]
	    public DateTime BirthDate { get; set; }

	    [DataMember(Name = "phoneNumber")]
	    public string PhoneNumber { get; set; }

	    [DataMember(Name = "avatarUrl")]
	    public string AvatarUrl { get; set; }

	    [DataMember(Name = "address")]
	    [MaxLength(100)]
	    public string Address { get; set; }

	    [DataMember(Name = "gender")]
	    public string Gender { get; set; }

	    [DataMember(Name = "email")]
	    [EmailAddress]
	    public string Email { get; set; }
    }
}
