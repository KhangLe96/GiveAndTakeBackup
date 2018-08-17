using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class ProvinceCity
    {
        [DataMember(Name = "provinceCityName")]
        public string ProvinceCityName { get; set; }
    }

    public class Post
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "statusApp")]
        public string PostStatus { get; set; }

        [DataMember(Name = "category")]
        public Category Category { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "createdTime")]
        public DateTime CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        public DateTime UpdatedTime { get; set; }

        [DataMember(Name = "images")]
        public List<string> Images { get; set; }

        [DataMember(Name = "address")]
        public ProvinceCity ProvinceCity { get; set; }

        [DataMember(Name = "appreciationCount")]
        public int AppreciationCount { get; set; }

        [DataMember(Name = "requestCount")]
        public int RequestCount { get; set; }

        [DataMember(Name = "commentCount")]
        public int CommentCount { get; set; }
    }

    public class Pagination
    {
        [DataMember(Name = "totals")]
        public int Totals { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }

    public class PostResponse
    {
        [DataMember(Name = "results")]
        public List<Post> Results { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}
