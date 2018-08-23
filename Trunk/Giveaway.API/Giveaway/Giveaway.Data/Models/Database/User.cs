using Giveaway.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.Data.Models.Database
{
    [Table("User")]
    [DataContract]
    public class User : BaseEntity
    {
        #region Required Properties

        [Required]
        [MaxLength(40)]
        [DataMember(Name = "firtName")]
        public string FirstName { get; set; }

        [Required]
        [DataMember(Name = "lastName")]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [DataMember(Name = "userName")]
        [MaxLength(20)]
        [MinLength(5)]
        public string UserName { get; set; }

        [DataMember(Name = "appreciationNumber")]
        [DefaultValue(0)]
        public int AppreciationNumber { get; set; }

        [DataMember(Name = "passwordSalt")]
        [Required]
        public byte[] PasswordSalt { get; set; }

        [DataMember(Name = "passwordHash")]
        [Required]
        public byte[] PasswordHash { get; set; }

        #endregion

        #region Unrequired Properties

        [DataMember(Name = "allowTokensSince")]
        public DateTimeOffset? AllowTokensSince { get; set; }

        [DataMember(Name = "birthDate")]
        public DateTime? BirthDate { get; set; }

        [DataMember(Name = "socialAccountId")]
        public string SocialAccountId { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "avatarUrl")]
        public string AvatarUrl { get; set; }

        [DataMember(Name = "address")]
        [MaxLength(100)]
        public string Address { get; set; }

        [DataMember(Name = "fullName")]
        [NotMapped]
        public string FullName => LastName + " " + FirstName;

        [DataMember(Name = "gender")]
        public Gender? Gender { get; set; }

        [DataMember(Name = "lastLogin")]
        public DateTimeOffset? LastLogin { get; set; }

        [DataMember(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

        #endregion
        
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserConversation> UserConversations { get; set; }
        public virtual ICollection<WarningMessage> WarningMessages { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
