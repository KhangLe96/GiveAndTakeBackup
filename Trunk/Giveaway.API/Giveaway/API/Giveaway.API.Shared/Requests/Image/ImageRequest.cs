using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Image
{
	public class ImageRequest
    {
        [DataMember(Name = "image")]
        public string Image { get; set; }
    }
}
