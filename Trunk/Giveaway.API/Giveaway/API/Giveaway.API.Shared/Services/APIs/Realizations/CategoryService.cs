using System;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.Models.Database;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class CategoryService : ICategoryService
    {
        private readonly Service.Services.ICategoryService _categoryService;

        public CategoryService(Service.Services.ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IQueryable<CategoryResponse> GetAllCategories()
        {
            var response = _categoryService.GetAllCategories();
            return GenerateCategoryResponse(response.Data as IQueryable<Category>);
        }

        public bool Delete(Guid id)
        {
            _categoryService.Delete(c => c.Id == id, out var isSaved);
            return isSaved;
        }

        private IQueryable<CategoryResponse> GenerateCategoryResponse(IQueryable<Category> categories)
        {
            return categories.Select(x => new CategoryResponse {Id = x.Id, CategoryName = x.CategoryName, CategoryImageUrl = x.ImageUrl});
        }
    }
}
