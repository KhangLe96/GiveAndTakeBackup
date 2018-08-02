using System;
using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.EF.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

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

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public PagingQueryResponse<CategoryResponse> All([FromHeader]IDictionary<string, string> @params)
        {
            return _categoryService.All(@params);
        }

        [HttpDelete("{categoryId}")]
        public CategoryResponse DeleteCategory(Guid categoryId)
        {
            return _categoryService.Delete(categoryId);
        }

        [HttpPost]
        public CategoryResponse Create([FromBody] CategoryRequest request)
        {
            return _categoryService.Create(request);
        }

        [HttpGet("{categoryId}")]
        public CategoryResponse Find(Guid categoryId)
        {
            return _categoryService.Find(categoryId);
        }

        [HttpPut("{categoryId}")]
        public CategoryResponse Update(Guid categoryId, [FromBody] CategoryRequest request)
        {
            return _categoryService.Update(categoryId, request);
        }
    }
}