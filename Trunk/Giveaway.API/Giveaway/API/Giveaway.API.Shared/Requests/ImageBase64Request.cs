using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class ImageBase64Request
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "file")]
        public string File { get; set; }
    }
}
