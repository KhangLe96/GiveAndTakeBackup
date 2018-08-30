using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.ProviceCity;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/provicecity")]
    public class ProviceCityController : Controller
    {
        private readonly IProviceCityService _proviceCityService;

        public ProviceCityController(IProviceCityService proviceCityService)
        {
            _proviceCityService = proviceCityService;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        public PagingQueryResponse<ProvinceCityResponse> GetListProviceCity([FromHeader]IDictionary<string, string> @params)
        {
            return _proviceCityService.GetPCForPaging(@params);
        }
    }
}