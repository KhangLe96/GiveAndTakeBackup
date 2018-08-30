using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.ProviceCity;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/provincecity")]
    public class ProvinceCityController : Controller
    {
        private readonly IProvinceCityService _provinceCityService;

        public ProvinceCityController(IProvinceCityService provinceCityService)
        {
            _provinceCityService = provinceCityService;
        }

        [HttpGet("list")]
        [Produces("application/json")]
        public PagingQueryResponse<ProvinceCityResponse> GetListProviceCity([FromHeader]IDictionary<string, string> @params)
        {
            return _provinceCityService.GetPCForPaging(@params);
        }
    }
}