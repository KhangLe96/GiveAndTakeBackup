using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Category;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Category;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface ICategoryService<T> where T : CategoryBaseResponse
    {
        PagingQueryResponse<T> All(IDictionary<string, string> @params);
        CategoryCmsResponse Delete(Guid id);
        CategoryCmsResponse Create(CategoryRequest request);
        T FindCategory(Guid id);
        CategoryCmsResponse Update(Guid id, CategoryRequest request);
        CategoryCmsResponse ChangeCategoryStatus(Guid userId, StatusRequest request);
    }
}