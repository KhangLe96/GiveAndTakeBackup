using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Category;
using CategoryRequest = Giveaway.Data.EF.DTOs.Requests.CategoryRequest;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface ICategoryService
    {
        PagingQueryResponse<CategoryResponse> All(IDictionary<string, string> @params);
        CategoryResponse Delete(Guid id);
        CategoryResponse Create(CategoryRequest request);
        CategoryResponse FindCategory(Guid id);
        CategoryResponse Update(Guid id, CategoryRequest request);
        CategoryResponse ChangeCategoryStatus(Guid userId, StatusRequest request);
    }
}