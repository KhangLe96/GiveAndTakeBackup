﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{

	[DataContract]
	public class Post : EditPost
	{

		[DataMember(Name = "statusApp")]
		public string PostStatus { get; set; }

		[DataMember(Name = "category")]
		public Category Category { get; set; }

		[DataMember(Name = "user")]
		public User User { get; set; }

		[DataMember(Name = "createdTime")]
		public DateTime CreatedTime { get; set; }

		[DataMember(Name = "updatedTime")]
		public DateTime UpdatedTime { get; set; }

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
	public class PostImage
	{
		[DataMember(Name = "imageUrl")]
		public string ImageData { get; set; }
	}

	[DataContract]
	public class EditPost : CreatePost
	{
		[DataMember(Name = "id")]
		public string PostId { get; set; }

		//[DataMember(Name = "userId")]
		//public string UserId { get; set; }

		//[DataMember(Name = "postStatus")]
		//public int Status { get; set; }
	}

	[DataContract]
	public class CreatePost
	{
		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "images")]
		public List<PostImage> PostImages { get; set; }

		[DataMember(Name = "categoryId")]
		public string PostCategory { get; set; }

		[DataMember(Name = "provinceCityId")]
		public string Address { get; set; }

	}
	[DataContract]
	public class PostStatus
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }
	}

}