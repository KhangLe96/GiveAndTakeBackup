using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.DTOs.Requests;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface ICategoryService
    {
        List<CategoryResponse> All();
        CategoryResponse Delete(Guid id);
        CategoryResponse Create(CategoryRequest request);
        CategoryResponse Find(Guid id);
        CategoryResponse Update(Guid id, CategoryRequest request);
    }
}