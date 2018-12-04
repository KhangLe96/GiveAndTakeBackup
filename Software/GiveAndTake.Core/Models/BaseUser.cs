using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class BaseUser
    {
		[DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

	    [DataMember(Name = "name")]
	    public string Name { get; set; }

		[DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "socialAccountId")]
        public string SocialAccountId { get; set; }

        [DataMember(Name = "avatarUrl")]
        public string AvatarUrl { get; set; }
    }
}