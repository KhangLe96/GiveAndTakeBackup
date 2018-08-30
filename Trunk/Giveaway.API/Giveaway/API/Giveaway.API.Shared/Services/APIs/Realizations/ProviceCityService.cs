using AutoMapper;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests.ProvinceCity;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.ProviceCity;
using Giveaway.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Giveaway.API.Shared.Services.APIs.Realizations
{
    public class ProviceCityService : IProviceCityService
    {
        private readonly Service.Services.IProviceCityService _proviceCityService;

        public ProviceCityService(Service.Services.IProviceCityService proviceCityService)
        {
            _proviceCityService = proviceCityService;
        }

        public PagingQueryResponse<ProvinceCityResponse> GetPCForPaging(IDictionary<string, string> @params)
        {
            var request = @params.ToObject<PagingQueryPCRequest>();
            var reports = GetPagedReports(request, out var total);
            return new PagingQueryResponse<ProvinceCityResponse>
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

        private List<ProvinceCityResponse> GetPagedReports(PagingQueryPCRequest request, out int total)
        {
            var proviceCities = _proviceCityService.Where(x => x.EntityStatus != EntityStatus.Deleted);

            total = proviceCities.Count();

            return proviceCities
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(x => Mapper.Map<ProvinceCityResponse>(x))
                .ToList();
        }

        #endregion
    }
}
