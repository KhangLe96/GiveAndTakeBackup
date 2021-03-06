﻿using Giveaway.API.Shared.Requests.Image;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Post
{
    public class PostRequest
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [DataMember(Name = "images", EmitDefaultValue = false)] 
        [JsonProperty(PropertyName = "images")]
        public List<ImageRequest> Images { get; set; }

        [DataMember(Name = "postStatus", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "postStatus")]
        public PostStatus PostStatus { get; set; }

        [DataMember(Name = "categoryId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "categoryId")]
        public Guid CategoryId { get; set; }

        [DataMember(Name = "provinceCityId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "provinceCityId")]
        public Guid ProvinceCityId { get; set; }

        [DataMember(Name = "userId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}
