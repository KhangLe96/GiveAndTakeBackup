using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class Image
	{
		[DataMember(Name = "originalImage")]
		public string OriginalImage { get; set; }

		[DataMember(Name = "resizedImage")]
		public string ResizedImage { get; set; }
	}
}