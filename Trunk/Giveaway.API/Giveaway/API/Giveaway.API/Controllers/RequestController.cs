using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/request")]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Get list request with params object that includes: page, limit, keyword, requestStatus
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [Produces("application/json")]
        public PagingQueryResponse<RequestResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _requestService.GetRequesttForPaging(@params);
        }
    }
}