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
    /// <inheritdoc />
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

        /// <summary>
        /// Get list post with params object that includes: page, limit, keyword, provinceCityId, categoryId, title
        /// </summary>
        /// <param name="params">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [HttpGet("getList")]
        [Produces("application/json")]
        public PagingQueryResponse<PostResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(null, @params);
        }

        /// <summary>
        /// Get list post of an User with userId and params object that includes: page, limit, keyword, provinceCityId, categoryId, title
        /// </summary>
        /// <param name="params">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [Authorize]
        [HttpGet("getListPostOfSingleUser/{userId}")]
        [Produces("application/json")]
        public PagingQueryResponse<PostResponse> GetListPostOfSingleUser(string userId, [FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(userId, @params);
        }

        /// <summary>
        /// Create a post
        /// </summary>
        /// <param name="postRequest">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [Authorize]
        [HttpPost("create")]
        [Produces("application/json")]
        public PostResponse Create([FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();  
            return _postService.Create(postRequest);
        }

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="postRequest"></param>
        /// <returns></returns>
        [Authorize] 
        [HttpPut("update/{postId}")]
        [Produces("application/json")]
        public PostResponse Update(Guid postId, [FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();
            return _postService.Update(postId, postRequest);
        }

        /// <summary>
        /// Change status of a post: Activated, Blocked, Deleted
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("statusCMS/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusCMS(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusCMS(postId, request);
        }

        /// <summary>
        /// Change status of a post: Open, Close
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("statusApp/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusApp(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusApp(postId, request);
        }
    }
}