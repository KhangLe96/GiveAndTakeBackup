using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
using System;
using Giveaway.Data.EF;
using Microsoft.AspNetCore.Authorization;
using Giveaway.API.Shared.Extensions;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Post")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        //When you implement get list. Should have a API to get list for mobile app, we need filter these posts which is activated. 
        //And a api for CMS, return all post with their status
 
        [HttpGet("getList")]
        [Produces("application/json")]
        public PagingQueryResponse<PostResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(null, @params);
        }

        [Authorize]
        [HttpGet("getListPostOfSingleUser/{userId}")]
        [Produces("application/json")]
        public PagingQueryResponse<PostResponse> GetListPostOfSingleUser(string userId, [FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(userId, @params);
        }

        [Authorize]
        [HttpPost("create")]
        [Produces("application/json")]
        public PostResponse Create([FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();  
            return _postService.Create(postRequest);
        }

        [Authorize] 
        [HttpPut("update/{postId}")]
        [Produces("application/json")]
        public PostResponse Update(Guid postId, [FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();
            return _postService.Update(postId, postRequest);
        }

        [Authorize]
        [HttpPut("statusCMS/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusCMS(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusCMS(postId, request);
        }

        [Authorize]
        [HttpPut("statusApp/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusApp(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusApp(postId, request);
        }
    }
}