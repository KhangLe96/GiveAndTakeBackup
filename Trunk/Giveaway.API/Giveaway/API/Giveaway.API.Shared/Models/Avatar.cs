using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Models
{
	[DataContract]
	public class Avatar
	{
		[DataMember(Name = "small")]
		public string SmallImagePath { get; set; }
		[DataMember(Name = "medium")]
		public string MediumImagePath { get; set; }
		[DataMember(Name = "big")]
		public string BigImagePath { get; set; }
	}
}
