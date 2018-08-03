using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Exceptions;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF;
using Giveaway.Data.Models.Database;
using CategoryRequest = Giveaway.Data.EF.DTOs.Requests.CategoryRequest;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class CategoryService : ICategoryService
    {
        private readonly Service.Services.ICategoryService _categoryService;

        public CategoryService(Service.Services.ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public PagingQueryResponse<CategoryResponse> All(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryPostRequest>();
            var categories = GetPagedCategories(request);
            return new PagingQueryResponse<CategoryResponse>
            {
                Data = categories,
                Pagination = new Pagination
                {
                    Total = _categoryService.Count(),
                    Page = request.Page,
                    Limit = request.Limit
                }
            };
        }

        private List<CategoryResponse> GetPagedCategories(PagingQueryPostRequest request)
        {
            var categories = _categoryService.Where(x => !x.IsDeleted);
            if (request.PostName != null)
            {
                categories = categories.Where(x => x.CategoryName.Contains(request.PostName));
            }
            return categories
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(category => GenerateCategoryResponse(category))
                .ToList();
        }

        public CategoryResponse Delete(Guid id)
        {
            var category = _categoryService.Find(id);
            category.IsDeleted = true;
            var isSaved = _categoryService.Update(category);
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

        private static CategoryResponse GenerateCategoryResponse(Category category) => new CategoryResponse
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            CategoryImageUrl = category.ImageUrl,
            CreatedTime = category.CreatedTime,
            UpdatedTime = category.UpdatedTime
        };
    }
}
