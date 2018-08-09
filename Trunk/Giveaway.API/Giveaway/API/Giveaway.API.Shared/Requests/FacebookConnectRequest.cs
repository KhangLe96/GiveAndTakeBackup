using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
	[DataContract]
	public class FacebookConnectRequest
	{
	    [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

	    [DataMember(Name = "lastName")]
        public string LastName { get; set; }

	    [DataMember(Name = "userName")]
        public string UserName { get; set; }

	    [DataMember(Name = "socialAccountId")]
        public string SocialAccountId { get; set; }

	    [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
