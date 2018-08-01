using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Exceptions;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
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

        public List<CategoryResponse> All()
        {
            var response = (IEnumerable<Category>)_categoryService.GetAllCategories().Data;
            if (response == null)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            return response.Select(GenerateCategoryResponse).ToList();
        }

        public CategoryResponse Delete(Guid id)
        {
            var category = _categoryService.Find(id);
            _categoryService.Delete(c => c.Id == id, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Create(CategoryRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = request.CategoryName,
                ImageUrl = request.CategoryImageUrl
            };
            var createdCategory = _categoryService.Create(category, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            return GenerateCategoryResponse(createdCategory);
        }

        public CategoryResponse Find(Guid id)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
            }
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Update(Guid id, CategoryRequest request)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
            }

            category.CategoryName = request.CategoryName;
            category.ImageUrl = request.CategoryImageUrl;
            
            var response = _categoryService.Update(category);
            if (!response)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            return Find(id);
        }

        private static CategoryResponse GenerateCategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryImageUrl = category.ImageUrl,
                CreatedTime = category.CreatedTime,
                UpdatedTime = category.UpdatedTime
            };
        }
    }
}
