using System.ComponentModel;
using Giveaway.API.Shared.Constants;
using Giveaway.API.Shared.Exceptions;
using Giveaway.Util.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Giveaway.API.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValidToken = filterContext.HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
            if (!isValidToken | apiKey != AppConstant.API_KEY)
            {
                filterContext.Result = new BadRequestObjectResult("Invalid Api key");
                return;
            }
        }
    }
}