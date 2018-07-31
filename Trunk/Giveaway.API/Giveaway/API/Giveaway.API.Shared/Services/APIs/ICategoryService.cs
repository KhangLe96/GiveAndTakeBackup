using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface ICategoryService
    {
        IQueryable<CategoryResponse> GetAllCategories();
    }
}