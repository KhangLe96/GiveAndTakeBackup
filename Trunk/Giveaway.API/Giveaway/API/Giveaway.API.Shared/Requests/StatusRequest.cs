using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
	[DataContract]
    public class StatusRequest
    {
        [DataMember(Name = "status")]
        public string UserStatus { get; set; }
    }
}