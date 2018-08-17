using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Image
{
    public class ImageRequest
    {
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
