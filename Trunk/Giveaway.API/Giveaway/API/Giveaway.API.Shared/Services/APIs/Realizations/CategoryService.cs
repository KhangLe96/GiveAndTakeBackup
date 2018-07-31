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

        public IQueryable<CategoryResponse> All()
        {
            var response = (IQueryable<Category>) _categoryService.GetAllCategories().Data;
            if (response == null)
            {
                throw new BadRequestException("No category found");
            }
            return response.Select(x => GenerateCategoryResponse(x));
            
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
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Find(Guid id)
        {
            var category =  _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException("Category doesn't exist");
            }
            return GenerateCategoryResponse(category);
        }

        private static CategoryResponse GenerateCategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryImageUrl = category.ImageUrl
            };
        }
    }
}
