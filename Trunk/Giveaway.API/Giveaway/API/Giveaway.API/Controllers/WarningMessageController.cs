using Giveaway.API.Shared.Requests.Warning;
using Giveaway.API.Shared.Responses.Warning;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/warning")]
    public class WarningMessageController : Controller
    {
        private readonly IWarningMessageService _warningMessageService;

        public WarningMessageController(IWarningMessageService warningMessageService)
        {
            _warningMessageService = warningMessageService;
        }

        /// <summary>
        /// Create an warning message for an user
        /// </summary>
        /// <param name="warningRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create")]
        [Produces("application/json")]
        public WarningResponse Create([FromBody]WarningRequest warningRequest)
        {
            return _warningMessageService.Create(warningRequest);
        }
    }
}