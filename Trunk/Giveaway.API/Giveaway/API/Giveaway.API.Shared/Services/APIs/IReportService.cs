using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IReportService
    {
        PagingQueryResponse<ReportResponse> GetReporttForPaging(IDictionary<string, string> @params);
    }
}
