using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.ProviceCity;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IProvinceCityService
    {
        PagingQueryResponse<ProvinceCityResponse> GetPCForPaging(IDictionary<string, string> @params);
    }
}
