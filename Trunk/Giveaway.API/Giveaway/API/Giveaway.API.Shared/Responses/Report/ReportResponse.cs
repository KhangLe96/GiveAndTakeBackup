using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Responses.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.Report
{
    public class ReportResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "message")]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [DataMember(Name = "createdTime")]
        [JsonProperty(PropertyName = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        [JsonProperty(PropertyName = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }

        [DataMember(Name = "user")]
        [JsonProperty(PropertyName = "user")]
        public UserPostResponse User { get; set; }

        [DataMember(Name = "post")]
        [JsonProperty(PropertyName = "post")]
        public PostReportResponse Post { get; set; }
    }
}
