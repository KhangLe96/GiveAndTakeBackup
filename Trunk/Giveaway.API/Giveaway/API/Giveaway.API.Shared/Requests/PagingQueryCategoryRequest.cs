using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryCategoryRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }
    }
}
