using System;
using System.Linq;
using Giveaway.API.Shared.Exceptions;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.DTOs.Requests;
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
            if (!isSaved)
            {
                throw new BadRequestException("Bad Request.");
            }
            return true;
        }

        public CategoryResponse Create(CategoryRequest request)
        {
            var category =  _categoryService.Create(new Category { CategoryName = request.CategoryName,ImageUrl = request.CategoryImageUrl}, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException("Bad Request.");
            }
            return new CategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryImageUrl = category.ImageUrl
            };
        }

        private static IQueryable<CategoryResponse> GenerateCategoryResponse(IQueryable<Category> categories)
        {
            return categories.Select(x => new CategoryResponse {Id = x.Id, CategoryName = x.CategoryName, CategoryImageUrl = x.ImageUrl});
        }
    }
}
