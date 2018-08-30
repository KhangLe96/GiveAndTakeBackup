using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class PostStatus
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }
	}
}