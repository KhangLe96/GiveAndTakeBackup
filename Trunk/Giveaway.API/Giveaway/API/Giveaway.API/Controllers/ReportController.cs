using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Report;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/report")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Get list report with params object that includes: page, limit, keyword 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [Produces("application/json")]
        public PagingQueryResponse<ReportResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _reportService.GetReporttForPaging(@params);
        }
    }
}