using Giveaway.API.Shared.Exceptions;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Category;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Category;
using Giveaway.Data.Enums;
using Giveaway.Data.Models.Database;
using Giveaway.Util.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class CategoryService<T> : ICategoryService<T> where T : CategoryBaseResponse
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

        public PagingQueryResponse<T> All(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryCategoryRequest>();
            var categories = GetPagedCategories(request);
            var pageInfo = GetPageInfo(request);
            return GeneratePagingQueryResponse(categories, pageInfo);
        }

        public CategoryCmsResponse Delete(Guid id)
        {
            var category = _categoryService.UpdateStatus(id, EntityStatus.Deleted.ToString());
            return Mapper.Map<CategoryCmsResponse>(category);
        }

        public CategoryCmsResponse Create(CategoryRequest request)
        {
            var category = GenerateCategory(request);
            var createdCategory = _categoryService.Create(category, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException(CommonConstant.Error.BadRequest);
            }
            return Mapper.Map<CategoryCmsResponse>(category);
        }

        public T FindCategory(Guid id)
        {
            var category = GetCategory(id);
            return Mapper.Map<T>(category);
        }

        public CategoryCmsResponse Update(Guid id, CategoryRequest request)
        {
            var category = GetCategory(id);
            UpdateCategoryFields(request, category);
            Update(category);
            return Mapper.Map<CategoryCmsResponse>(category);
        }

        public CategoryCmsResponse ChangeCategoryStatus(Guid userId, StatusRequest request)
        {
            var updatedCategory = _categoryService.UpdateStatus(userId, request.UserStatus);
            return Mapper.Map<CategoryCmsResponse>(updatedCategory);
        }

        #endregion

        #region Utils

        private List<T> GetPagedCategories(PagingQueryCategoryRequest request)
        {
            var categories = _categoryService.Where(x => x.EntityStatus != EntityStatus.Deleted);
            if (request.CategoryName != null)
            {
                categories = categories.Where(x => x.CategoryName.Contains(request.CategoryName));
            }
            return categories
                .OrderBy(x => x.Priority)
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(category => Mapper.Map<T>(category))
                .ToList();
        }

        private PageInformation GetPageInfo(PagingQueryCategoryRequest request) => new PageInformation
        {
            Total = _categoryService.Where(x => x.EntityStatus != EntityStatus.Deleted).Count(),
            Page = request.Page,
            Limit = request.Limit
        };

        private static PagingQueryResponse<T> GeneratePagingQueryResponse(List<T> categories, PageInformation pageInfo)
            => new PagingQueryResponse<T>
            {
                Data = categories,
                PageInformation = pageInfo
            };

        private void Update(Category category)
        {
            var isSaved = _categoryService.Update(category);
            if (!isSaved)
            {
                throw new BadRequestException(CommonConstant.Error.BadRequest);
            }
        }

        private static Category GenerateCategory(CategoryRequest request) => new Category
        {
            Id = Guid.NewGuid(),
            CategoryName = request.CategoryName,
            ImageUrl = request.CategoryImageUrl,
            BackgroundColor = request.BackgroundColor,
            Priority = 1
        };

        private Category GetCategory(Guid id)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException(CommonConstant.Error.NotFound);
            }
            return category;
        }

        private static void UpdateCategoryFields(CategoryRequest request, Category category)
        {
            category.CategoryName = request.CategoryName;
            category.ImageUrl = request.CategoryImageUrl;
            category.UpdatedTime = DateTimeOffset.UtcNow;
            category.BackgroundColor = request.BackgroundColor;
        }

        #endregion
    }
}
