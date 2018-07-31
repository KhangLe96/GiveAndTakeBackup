using System.Collections.Generic;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Giveaway.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Manage category
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/Categories")]
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
    }
}