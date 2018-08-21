using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.Report;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Report;
using Giveaway.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//REVIEW: remove unused namespace

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class ReportService : IReportService
    {
        private readonly Service.Services.IReportService _reportService;

        public ReportService(Service.Services.IReportService reportService)
        {
            _reportService = reportService;
        }

        public PagingQueryResponse<ReportResponse> GetReporttForPaging(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryReportRequest>();
            var reports = GetPagedReports(request, out var total);
            return new PagingQueryResponse<ReportResponse>
            {
                Data = reports,
                PageInformation = new PageInformation
                {
                    Total = total,
                    Page = request.Page,
                    Limit = request.Limit
                }
            };
        }

        #region Utils

        private List<ReportResponse> GetPagedReports(PagingQueryReportRequest request, out int total)
        {
            var reports = _reportService.Include(x => x.Post).Include(x => x.User.WarningMessages).Where(x => x.EntityStatus != EntityStatus.Deleted);

            total = reports.Count();

            return reports
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(x => Mapper.Map<ReportResponse>(x))
                .ToList();
        }

        #endregion
    }
}
