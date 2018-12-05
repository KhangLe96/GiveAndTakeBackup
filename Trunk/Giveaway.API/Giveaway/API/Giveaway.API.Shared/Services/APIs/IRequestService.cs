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
	    RequestPostResponse GetRequestById(Guid requestId);
	    RequestPostResponse GetRequestOfCurrentUserByPostId(Guid userId, Guid postId);
		RequestPostResponse Create(RequestPostRequest requestPost);
        bool UpdateStatus(Guid requestId, StatusRequest request, Guid userId);
        bool Delete(Guid requestId);
        object CheckUserRequest(Guid postId, Guid userId);
	    bool CheckIfRequestProcessed(Guid requestId);

		bool DeleteCurrentUserRequest(Guid postId, Guid userId);
    }
}
