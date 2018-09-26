using System;
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

		[DataMember(Name = "images")]
		public List<Image> Images { get; set; }

		public bool IsMyPost { get; set; }
	}
}
