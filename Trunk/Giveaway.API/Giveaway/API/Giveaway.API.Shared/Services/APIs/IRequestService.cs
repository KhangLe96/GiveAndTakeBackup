using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IRequestService
    {
        PagingQueryResponse<RequestPostResponse> GetRequestForPaging(string postId, IDictionary<string, string> @params);
        RequestPostResponse Create(RequestPostRequest request);
        bool UpdateStatus(Guid requestId, StatusRequest request);
        bool Delete(Guid requestId);
        object CheckUserRequest(Guid postId, Guid userId);
    }
}
