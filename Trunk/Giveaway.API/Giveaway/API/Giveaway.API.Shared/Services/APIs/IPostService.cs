using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService
    {
        PagingQueryResponse<PostResponse> GetPostForPaging(IDictionary<string, string> @params);
        PostResponse Create(PostRequest post);
        bool Delete(Guid id);
        bool Update(PostRequest postRequest);
    }
}
