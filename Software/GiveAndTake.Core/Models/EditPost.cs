using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class EditPost : CreatePost
	{
		[DataMember(Name = "id")]
		public string PostId { get; set; }
	}
}