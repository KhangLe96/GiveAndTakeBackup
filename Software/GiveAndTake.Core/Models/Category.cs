﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
    [DataContract]
    public class Category
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "categoryImageUrl")]
        public string CategoryImageUrl { get; set; }
        
        [DataMember(Name = "status")]
        public string Status { get; set; }

/*        [DataMember(Name="color")]
        public string Color { get; set; }*/

        [DataMember(Name = "createdTime")]
        public DateTime CreatedTime { get; set; }

        [DataMember(Name = "updatedTime")]
        public DateTime UpdatedTime { get; set; }
    }

    [DataContract]
    public class CategoryList
    {
        [DataMember(Name = "results")]
        public List<Category> Categories { get; set; }
    }
}
