using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class User : BaseUser
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        //[DataMember(Name = "appreciationNumber")]
        //public int AppreciationNumber { get; set; }

        [DataMember(Name = "birthdate")]
        public DateTimeOffset? BirthDate { get; set; }

        //public string PasswordSalt { get; set; }
        //public string PasswordHash { get; set; }
        //public DateTimeOffset AllowTokensSince { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        public string FullName => LastName + " " + FirstName;

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

	    [DataMember(Name = "role")]
		public List<string> Role { get; set; }

	    [DataMember(Name = "status ")]
		public string Status { get; set; }

		//public DateTimeOffset LastLogin { get; set; }
	}
}