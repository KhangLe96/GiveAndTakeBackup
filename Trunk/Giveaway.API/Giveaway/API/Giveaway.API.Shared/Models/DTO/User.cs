﻿using Giveaway.Data.Enums;
using Giveaway.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Giveaway.API.Shared.Models.DTO
{
    [Table("User")]
    public class User : BaseEntity
    {
        #region Required Properties

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string UserName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public DateTimeOffset AllowTokensSince { get; set; }

        #endregion

        #region Unrequired Properties

        [NotMapped]
        public string FullName => LastName + " " + FirstName;

        public Gender Gender { get; set; }

        public DateTimeOffset LastLogin { get; set; }

        public Avatar Avatar { get; set; }
        #endregion
    }
}
