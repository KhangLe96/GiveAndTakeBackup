using System.Runtime.Serialization;

namespace Giveaway.API.Shared.Requests
{
    [DataContract]
    public class PagingQueryCategoryRequest : BasePagingQueryRequest
    {
        //Review: Create new query object for category to handle in this case
        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }
    }
}
