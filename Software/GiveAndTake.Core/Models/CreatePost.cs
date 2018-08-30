using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
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
}