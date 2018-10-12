using Giveaway.API.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Giveaway.API.Controllers
{
	public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
			// /REVIEW: Only validate ApiKey if Jwt is null or empty
            //var isValidToken = filterContext.HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
            //if (!isValidToken | apiKey != AppConstant.API_KEY)
            //{
            //    filterContext.Result = new BadRequestObjectResult("Invalid Api key");
            //    return;
            //}
        }
    }
}