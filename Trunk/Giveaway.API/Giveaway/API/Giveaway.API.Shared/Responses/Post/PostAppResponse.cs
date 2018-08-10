using Giveaway.API.Shared.Responses.Post;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Responses.Post
{
    [DataContract]
    public class PostAppResponse : PostBaseResponse
    {
        [DataMember(Name = "statusApp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "statusApp")]
        public string Status { get; set; }
    }
}
