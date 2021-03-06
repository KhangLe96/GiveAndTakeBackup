﻿using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Post;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface IPostService<T> where T : PostBaseResponse
    {
        PagingQueryResponse<T> GetPostForPaging(string userId, IDictionary<string, string> @params, string platform);
        T GetDetail(Guid postId);
        PostAppResponse Create(PostRequest post);
        bool ChangePostStatusCMS(Guid id, StatusRequest request);
        bool ChangePostStatusApp(Guid postId, StatusRequest request);
        PostAppResponse Update(Guid id, PostRequest postRequest);
    }
}
