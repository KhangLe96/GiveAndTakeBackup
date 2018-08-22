﻿using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IRequestService
    {
        PagingQueryResponse<RequestPostResponse> GetRequesttForPaging(IDictionary<string, string> @params);
        RequestPostResponse Create(RequestPostRequest request);
    }
}
