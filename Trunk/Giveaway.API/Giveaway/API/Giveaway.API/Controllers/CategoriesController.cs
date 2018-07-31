using System;
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
        public IQueryable<CategoryResponse> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpDelete("{categoryId}")]
        public bool DeleteCategory(Guid categoryId)
        {
            return _categoryService.Delete(categoryId);
        }

        [HttpPost]
        public CategoryResponse Create([FromBody] CategoryRequest request)
        {
            return _categoryService.Create(request);
        }
    }
}