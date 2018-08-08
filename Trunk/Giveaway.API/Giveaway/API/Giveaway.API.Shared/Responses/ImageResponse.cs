using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Responses
{
    public class ImageResponse
    {
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
