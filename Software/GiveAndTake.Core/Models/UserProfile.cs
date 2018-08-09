using System;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string SocialAccountId { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public int AppreciationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset AllowTokensSince { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }
        public string Address { get; set; }
        public string FullName => LastName + " " + FirstName;
        public string Gender { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }
}