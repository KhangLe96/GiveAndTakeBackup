using Giveaway.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Manage category
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    }
}