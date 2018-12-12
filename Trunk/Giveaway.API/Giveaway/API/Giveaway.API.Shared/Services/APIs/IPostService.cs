using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Post;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService<T> where T : PostBaseResponse
    {
        PagingQueryResponse<T> GetPostForPaging(IDictionary<string, string> @params, string userId, bool isListOfSingleUser);
	    PagingQueryResponse<RequestedPostResponse> GetListRequestedPostOfUser(IDictionary<string, string> @params, string userId);
		T GetDetail(Guid postId, string userId);
        PostAppResponse Create(PostRequest post);
        bool ChangePostStatus(Guid postId, Guid userId, StatusRequest request);
        PostAppResponse Update(Guid id, PostRequest postRequest);
    }
}
