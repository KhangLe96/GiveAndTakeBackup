using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class PostImage
	{
		[DataMember(Name = "image")]
		public string ImageData { get; set; }
	}
}