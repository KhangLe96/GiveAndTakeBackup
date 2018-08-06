using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CategoryRequest = Giveaway.Data.EF.DTOs.Requests.CategoryRequest;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Manage category
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <inheritdoc />
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories with filter  
        /// </summary>
        /// <returns>filtrated categories with pagination</returns>
        [HttpGet]
        public PagingQueryResponse<CategoryResponse> All([FromHeader]IDictionary<string, string> @params)
        {
            return _categoryService.All(@params);
        }

        /// <summary>
        /// delete a category by id
        /// only update EntityStatus = Deleted, the category still remain in database
        /// </summary>
        /// <returns>the deleted category object</returns>
        [HttpDelete("{categoryId}")]
        public CategoryResponse DeleteCategory(Guid categoryId)
        {
            return _categoryService.Delete(categoryId);
        }

        /// <summary>
        /// create a new category
        /// </summary>
        /// <returns>the created category object</returns>
        [HttpPost]
        public CategoryResponse Create([FromBody] CategoryRequest request)
        {
            return _categoryService.Create(request);
        }

        /// <summary>
        /// find a category by id
        /// throw exception when category not found
        /// </summary>
        /// <returns>the detected category with id inputed </returns>
        [HttpGet("{categoryId}")]
        public CategoryResponse Find(Guid categoryId)
        {
            return _categoryService.FindCategory(categoryId);
        }

        /// <summary>
        /// update a category's information 
        /// </summary>
        /// <returns>the updated category</returns>
        [HttpPut("{categoryId}")]
        public CategoryResponse Update(Guid categoryId, [FromBody] CategoryRequest request)
        {
            return _categoryService.Update(categoryId, request);
        }

        /// <summary> 
        /// Change category status. Only available for admin or super admin
        /// Available values : Activated, Blocked, Deleted
        /// </summary> 
        /// <returns>the updated user profile</returns> 
        [Authorize(Roles = Const.Roles.AdminOrAbove)]
        [HttpPut("status/{userId}")]
        [Produces("application/json")]
        public CategoryResponse ChangeCategoryStatus(Guid userId, [FromBody]StatusRequest request)
        {
            return _categoryService.ChangeCategoryStatus(userId, request);
        }
    }
}