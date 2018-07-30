using Giveaway.Data.Enums;
using System;
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

        [DataMember(Name = "address")]
        [MaxLength(100)]
        public string Address { get; set; }

        [DataMember(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Day of birth
        /// </summary>
        [DataMember(Name = "dob")]
        [Required]
        public DateTime Dob { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        private string _role;

        [DataMember(Name = "role")]
        [Required]
        public string Role
        {
            get => _role;
            set
            {
                _role = value;

                if (!string.IsNullOrEmpty(value))
                {
                    _role = char.ToUpper(_role[0]) + _role.Substring(1).ToLower();
                }
            }
        }

        [Required]
        [DataMember(Name = "isActivated")]
        [DefaultValue(true)]
        public bool IsActivated { get; set; }

        [DataMember(Name = "passwordSalt")]
        [Required]
        public byte[] PasswordSalt { get; set; }

        [DataMember(Name = "passwordHash")]
        [Required]
        public byte[] PasswordHash { get; set; }

        [DataMember(Name = "allowTokensSince")]
        [Required]
        public DateTimeOffset AllowTokensSince { get; set; }

        #endregion

        #region Unrequired Properties

        [DataMember(Name = "fullName")]
        [NotMapped]
        public string FullName => LastName + " " + FirstName;

        [DataMember(Name = "gender")]
        public Gender Gender { get; set; }

        [DataMember(Name = "lastLogin")]
        public DateTimeOffset LastLogin { get; set; }

        [DataMember(Name = "avatar")]
        public virtual Avatar Avatar { get; set; }

        #endregion
    }
}
