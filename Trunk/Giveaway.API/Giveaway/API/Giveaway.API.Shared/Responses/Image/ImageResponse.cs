using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Image
{
	public class ImageResponse
    {
        [DataMember(Name = "originalImage")]
        public string OriginalImage { get; set; }

        [DataMember(Name = "resizedImage")]
        public string ResizedImage { get; set; }
    }
}
