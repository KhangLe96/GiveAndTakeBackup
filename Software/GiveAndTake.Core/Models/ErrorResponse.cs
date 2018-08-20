using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    public class ErrorResponse
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
