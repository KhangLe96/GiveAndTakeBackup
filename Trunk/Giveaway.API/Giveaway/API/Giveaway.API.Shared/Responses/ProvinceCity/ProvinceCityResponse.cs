using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Giveaway.API.Shared.Responses.ProviceCity
{
    public class ProvinceCityResponse
    {
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember(Name = "provinceCityName")]
        [JsonProperty(PropertyName = "provinceCityName")]
        public string ProvinceCityName { get; set; }
    }
}
