using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Image
{
	[DataContract]
	public class ImageResponse
    {
	    [DataMember(Name = "id")]
	    public Guid Id { get; set; }

		[DataMember(Name = "originalImage")]
        public string OriginalImage { get; set; }

        [DataMember(Name = "resizedImage")]
        public string ResizedImage { get; set; }
    }
}
