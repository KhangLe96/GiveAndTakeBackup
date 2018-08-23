using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Exceptions;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Category;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Category;
using Giveaway.Data.EF;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using CategoryRequest = Giveaway.Data.EF.DTOs.Requests.CategoryRequest;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class CategoryService : ICategoryService
    {
        #region Properties

        private readonly Service.Services.ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryService(Service.Services.ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Public methods

        public PagingQueryResponse<CategoryResponse> All(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryCategoryRequest>();
            var categories = GetPagedCategories(request);
            var pageInfo = GetPageInfo(request);
            return GeneratePagingQueryResponse(categories, pageInfo);
        }

        public CategoryResponse Delete(Guid id)
        {
            var category = _categoryService.UpdateStatus(id, EntityStatus.Deleted.ToString());
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Create(CategoryRequest request)
        {
            var category = GenerateCategory(request);
            var createdCategory = _categoryService.Create(category, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
            return GenerateCategoryResponse(createdCategory);
        }

        public CategoryResponse FindCategory(Guid id)
        {
            var category = GetCategory(id);
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Update(Guid id, CategoryRequest request)
        {
            var category = GetCategory(id);
            UpdateCategoryFields(request, category);
            Update(category);
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse ChangeCategoryStatus(Guid userId, StatusRequest request)
        {
            var updatedCategoryr = _categoryService.UpdateStatus(userId, request.UserStatus);
            return GenerateCategoryResponse(updatedCategoryr);
        }

        #endregion

        #region Private methods

        private List<CategoryResponse> GetPagedCategories(PagingQueryCategoryRequest request)
        {
            var categories = _categoryService.Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (request.CategoryName != null)
            {
                categories = categories.Where(x => x.CategoryName.Contains(request.CategoryName));
            }
            return categories
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(category => GenerateCategoryResponse(category))
                .ToList();
        }

        private PageInformation GetPageInfo(PagingQueryCategoryRequest request) => new PageInformation
        {
            Total = _categoryService.Where(x => x.EntityStatus != EntityStatus.Deleted).Count(),
            Page = request.Page,
            Limit = request.Limit
        };

        private static PagingQueryResponse<CategoryResponse> GeneratePagingQueryResponse(List<CategoryResponse> categories, PageInformation pageInfo)
            => new PagingQueryResponse<CategoryResponse>
            {
                Data = categories,
                PageInformation = pageInfo
            };

        private void Update(Category category)
        {
            var isSaved = _categoryService.Update(category);
            if (!isSaved)
            {
                throw new BadRequestException(Const.Error.BadRequest);
            }
        }

        private static Category GenerateCategory(CategoryRequest request) => new Category
        {
            Id = Guid.NewGuid(),
            CategoryName = request.CategoryName,
            ImageUrl = request.CategoryImageUrl
        };

        private Category GetCategory(Guid id)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException(Const.Error.NotFound);
            }
            return category;
        }

        private static void UpdateCategoryFields(CategoryRequest request, Category category)
        {
            category.CategoryName = request.CategoryName;
            category.ImageUrl = request.CategoryImageUrl;
            category.UpdatedTime = DateTimeOffset.UtcNow;
        }

        private static CategoryResponse GenerateCategoryResponse(Category category) => new CategoryResponse
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            CategoryImageUrl = category.ImageUrl,
            CreatedTime = category.CreatedTime,
            UpdatedTime = category.UpdatedTime,
            EntityStatus = category.EntityStatus.ToString()
        };

        #endregion
    }
}
