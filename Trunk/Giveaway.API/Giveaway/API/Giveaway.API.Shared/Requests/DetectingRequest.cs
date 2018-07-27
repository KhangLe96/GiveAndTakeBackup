using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class DetectingRequest
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "creditId")]
        public Guid CreditId { get; set; }

        [DataMember(Name = "imagesUrls")]
        public List<string> imagesUrls { get; set; }
    }
}
