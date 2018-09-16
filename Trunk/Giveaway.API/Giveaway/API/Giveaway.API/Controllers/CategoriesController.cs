using System;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Category;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Category;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Manage category
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/categories")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService<CategoryCmsResponse> _categoryCmsService;
        private readonly ICategoryService<CategoryAppResponse> _categoryAppService;

        /// <inheritdoc />
        public CategoriesController(ICategoryService<CategoryCmsResponse> categoryCmsService, ICategoryService<CategoryAppResponse> categoryAppService)
        {
            _categoryCmsService = categoryCmsService;
            _categoryAppService = categoryAppService;
        }

        /// <summary>
        /// Get all categories with filter  
        /// </summary>
        /// <returns>filtrated categories with pagination</returns>
        [HttpGet("cms/list")]
        public PagingQueryResponse<CategoryCmsResponse> GetListCategoryCms([FromHeader]IDictionary<string, string> @params)
        {
            return _categoryCmsService.All(@params);
        }

        /// <summary>
        /// Get all categories for app with filter  
        /// </summary>
        /// <returns>filtrated categories with pagination</returns>
        [HttpGet("app/list")]
        public PagingQueryResponse<CategoryAppResponse> GetListApp([FromHeader]IDictionary<string, string> @params)
        {
            return _categoryAppService.All(@params);
        }

        /// <summary>
        /// delete a category by id
        /// only update EntityStatus = Deleted, the category still remain in database
        /// </summary>
        /// <returns>the deleted category object</returns>
        [HttpDelete("cms/{categoryId}")]
        public CategoryCmsResponse DeleteCategory(Guid categoryId)
        {
            return _categoryCmsService.Delete(categoryId);
        }

        /// <summary>
        /// create a new category
        /// </summary>
        /// <returns>the created category object</returns>
        [HttpPost("cms")]
        public CategoryCmsResponse Create([FromBody] CategoryRequest request)
        {
            return _categoryCmsService.Create(request);
        }

        /// <summary>
        /// find a category for cms by id
        /// throw exception when category not found
        /// </summary>
        /// <returns>the detected category with id inputed </returns>
        [HttpGet("cms/{categoryId}")]
        public CategoryCmsResponse FindCmsCategory(Guid categoryId)
        {
            return _categoryCmsService.FindCategory(categoryId);
        }

        /// <summary>
        /// find a category for app by id
        /// throw exception when category not found
        /// </summary>
        /// <returns>the detected category with id inputed </returns>
        [HttpGet("app/{categoryId}")]
        public CategoryAppResponse FindAppCategory(Guid categoryId)
        {
            return _categoryAppService.FindCategory(categoryId);
        }

        /// <summary>
        /// update a category's information 
        /// </summary>
        /// <returns>the updated category</returns>
        [HttpPut("cms/{categoryId}")]
        public CategoryCmsResponse Update(Guid categoryId, [FromBody] CategoryRequest request)
        {
            return _categoryCmsService.Update(categoryId, request);
        }

        /// <summary> 
        /// Change category status. Only available for admin or super admin
        /// Available values : Activated, Blocked, Deleted
        /// </summary> 
        /// <returns>the updated user profile</returns> 
        [Authorize(Roles = Const.Roles.AdminOrAbove)]
        [HttpPut("cms/status/{userId}")]
        [Produces("application/json")]
        public CategoryCmsResponse ChangeCategoryStatus(Guid userId, [FromBody]StatusRequest request)
        {
            return _categoryCmsService.ChangeCategoryStatus(userId, request);
        }
    }
}