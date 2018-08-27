using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests.Category
{
    [DataContract]
    public class PagingQueryCategoryRequest : BasePagingQueryRequest
    {
        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }
    }
}
