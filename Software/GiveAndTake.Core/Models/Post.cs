using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class Post
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "statusApp")]
		public string PostStatus { get; set; }

		[DataMember(Name = "category")]
		public Category Category { get; set; }

		[DataMember(Name = "user")]
		public User User { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "createdTime")]
		public DateTime CreatedTime { get; set; }

		[DataMember(Name = "updatedTime")]
		public DateTime UpdatedTime { get; set; }

		[DataMember(Name = "images")]
		public List<string> Images { get; set; }

		[DataMember(Name = "address")]
		public ProvinceCity ProvinceCity { get; set; }

		[DataMember(Name = "appreciationCount")]
		public int AppreciationCount { get; set; }

		[DataMember(Name = "requestCount")]
		public int RequestCount { get; set; }

		[DataMember(Name = "commentCount")]
		public int CommentCount { get; set; }
	}

	[DataContract]
	public class ProvinceCity
	{
		[DataMember(Name = "provinceCityName")]
		public string ProvinceCityName { get; set; }
	}

	[DataContract]
	public class Category
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "categoryName")]
		public string CategoryName { get; set; }

		[DataMember(Name = "categoryImageUrl")]
		public string CategoryImageUrl { get; set; }

		[DataMember(Name = "status")]
		public string Status { get; set; }

		/*        [DataMember(Name="color")]
				public string Color { get; set; }*/

		[DataMember(Name = "createdTime")]
		public DateTime CreatedTime { get; set; }

		[DataMember(Name = "updatedTime")]
		public DateTime UpdatedTime { get; set; }
	}

	[DataContract]
	public class CategoryResponse
	{
		[DataMember(Name = "results")]
		public List<Category> Categories { get; set; }
	}
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
		public DateTime BirthDate { get; set; }

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

		//public DateTimeOffset LastLogin { get; set; }
	}

	[DataContract]
	public class BaseUser
	{
		[DataMember(Name = "firstName")]
		public string FirstName { get; set; }

		[DataMember(Name = "lastName")]
		public string LastName { get; set; }

		[DataMember(Name = "username")]
		public string UserName { get; set; }

		[DataMember(Name = "socialAccountId")]
		public string SocialAccountId { get; set; }

		[DataMember(Name = "avatarUrl")]
		public string AvatarUrl { get; set; }
	}
}
