using Giveaway.API.Shared.Constants;
using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Post;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Post;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    /// <inheritdoc />
    [Produces("application/json")]
    [Route("api/v1/Post")]
    public class PostController : BaseController
    {
        private readonly IPostService<PostCmsResponse> _postCmsService;
        private readonly IPostService<PostAppResponse> _postService;

        public PostController(IPostService<PostCmsResponse> postCmsService, IPostService<PostAppResponse> postAppService)
        {
            _postCmsService = postCmsService;
            _postService = postAppService;
        }

        /// <summary>
        /// Get list post with params object that includes: page, limit, keyword, provinceCityId, categoryId, title
        /// </summary>
        /// <param name="params">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [HttpGet("cms/list")]
        [Produces("application/json")]
        public PagingQueryResponse<PostCmsResponse> GetListPostCMS([FromHeader]IDictionary<string, string> @params)
        {
            return _postCmsService.GetPostForPaging(null, @params, WebConstant.Platform.CMS);
        }

        /// <summary>
        /// Get list post with params object that includes: page, limit, provinceCityId, categoryId, title, keyword(title, Description, CategoryName, UserName, ProvinceCityName)
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("app/list")]
        [Produces("application/json")]
        public PagingQueryResponse<PostAppResponse> GetListPostApp([FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(null, @params, WebConstant.Platform.App);
        }

        /// <summary>
        /// Get list post of an User with userId and params object that includes: page, limit, keyword, provinceCityId, categoryId, title
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="params">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [Authorize]
        [HttpGet("app/listPostOfUser/{userId}")]
        [Produces("application/json")]
        public PagingQueryResponse<PostAppResponse> GetListPostOfSingleUser(string userId, [FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(userId, @params, null);
        }

        /// <summary>
        /// Get detail of a post by id 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("app/detail/{postId}")]
        [Produces("application/json")]
        public PostAppResponse GetDetailApp(Guid postId)
        {
            return _postService.GetDetail(postId);
        }

        /// <summary>
        /// Get detail of a post by id 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("cms/detail/{postId}")]
        [Produces("application/json")]
        public PostCmsResponse GetDetailCms(Guid postId)
        {
            return _postCmsService.GetDetail(postId);
        }

        /// <summary>
        /// Create a post
        /// </summary>
        /// <param name="postRequest">page, limit, keyword, provinceCityId, categoryId, title</param>
        /// <returns>List post</returns>
        [Authorize]
        [HttpPost("app/create")]
        [Produces("application/json")]
        public PostAppResponse Create([FromBody]PostRequest postRequest)
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
        [HttpPut("app/update/{postId}")]
        [Produces("application/json")]
        public PostAppResponse Update(Guid postId, [FromBody]PostRequest postRequest)
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
        [HttpPut("cms/status/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusCMS(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusCMS(postId, request);
        }

        /// <summary>
        /// Change status of a post: Giving, Gived
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("app/status/{postId}")]
        [Produces("application/json")]
        public bool ChangePostStatusApp(Guid postId, [FromBody]StatusRequest request)
        {
            return _postService.ChangePostStatusApp(postId, request);
        }
    }
}