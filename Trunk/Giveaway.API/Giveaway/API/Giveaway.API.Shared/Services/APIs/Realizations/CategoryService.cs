using System;
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

        //Review: I see some places you use IQueryable, some places you use IEnumrable. Should use List to consistence
        public IQueryable<CategoryResponse> All()
        {
            var response = (IQueryable<Category>)_categoryService.GetAllCategories().Data;
            if (response == null)
            {
                //Review: should define error key in Constant file. Example: BadRequest
                throw new BadRequestException("No category found");
            }
            return response.Select(x => GenerateCategoryResponse(x));
        }


        //Review: it is good if you return deleted object instead of boolean
        public bool Delete(Guid id)
        {
            _categoryService.Delete(c => c.Id == id, out var isSaved);
            if (!isSaved)
            {
                throw new BadRequestException("Bad Request.");
                //Review: should define error key in Constant file. Example: BadRequest
                //throw new BadRequestException(Const.Error.BadRequest);
            }
            return true;
        }

        public CategoryResponse Create(CategoryRequest request)
        {
            //Review: should init Id for object to avoid error
            // should make object in new line to have clean code
            //var categoryRequest =
            //    new Category
            //    {
            //        Id = Guid.NewGuid(),
            //        CategoryName = request.CategoryName,
            //        ImageUrl = request.CategoryImageUrl
            //    };
            var category = _categoryService.Create(new Category { CategoryName = request.CategoryName, ImageUrl = request.CategoryImageUrl }, out var isSaved);
            if (!isSaved)
            {
                //Review: should define error key in Constant file. Example: BadRequest
                throw new BadRequestException("Bad Request.");
            }
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Find(Guid id)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException("Category doesn't exist");
            }
            return GenerateCategoryResponse(category);
        }

        public CategoryResponse Update(Guid id, CategoryRequest request)
        {
            var category = _categoryService.Find(id);
            if (category == null)
            {
                throw new BadRequestException("Category doesn't exist");
            }

            category.CategoryName = request.CategoryName;
            category.ImageUrl = request.CategoryImageUrl;
            
            var response = _categoryService.Update(category);
            if (!response)
            {
                throw new BadRequestException("Bad Request.");
            }
            return Find(id);
        }

        private static CategoryResponse GenerateCategoryResponse(Category category)
        {
            //Review: Should return more info when get detail, we can use it for both CMS and Mobile app without creating other API. It's neccessary. Add create time...
            return new CategoryResponse
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryImageUrl = category.ImageUrl
            };
        }
    }
}
