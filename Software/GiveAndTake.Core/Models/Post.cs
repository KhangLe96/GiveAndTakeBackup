using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
    public class Address
    {
        public string ProvinceCityName { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public object CategoryImageUrl { get; set; }
        public string Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }

    public class Post
    {
        public string Id { get; set; }
        public string PostStatus { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public List<string> Images { get; set; }
        public string Address { get; set; }
    }

    public class Pagination
    {
        public int Totals { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }

    public class PostResponse
    {
        public List<Post> Results { get; set; }
        public Pagination Pagination { get; set; }
    }
}
