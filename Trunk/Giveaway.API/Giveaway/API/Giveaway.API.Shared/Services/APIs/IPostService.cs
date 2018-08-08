using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService
    {
        PagingQueryResponse<PostResponse> GetPostForPaging(string userId, IDictionary<string, string> @params);
        PostResponse Create(PostRequest post);
        bool ChangePostStatusCMS(Guid id, StatusRequest request);
        bool ChangePostStatusApp(Guid postId, StatusRequest request);
        PostResponse Update(Guid id, PostRequest postRequest);
    }
}
