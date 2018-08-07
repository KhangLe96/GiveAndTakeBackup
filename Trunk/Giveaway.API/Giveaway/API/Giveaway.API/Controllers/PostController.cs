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

        //You can create other API to get list post of currentUser, it is similar as get list post with pagination.
        //When you implement get list. Should have a API to get list for mobile app, we need filter these posts which is activated. 
        //And a api for CMS, return all post with their status
 
        [HttpGet("getList")]
        [Produces("application/json")]
        public PagingQueryResponse<PostResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _postService.GetPostForPaging(@params);
        }

        /// <summary>
        /// Review: You need ad Authorize, after that you can get UserId from User.GetUserId()
        /// </summary>
        /// <param name="postRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create")]
        [Produces("application/json")]
        public PostResponse Create([FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();  
            return _postService.Create(postRequest);
        }

        [Authorize]
        [HttpPut("update")]
        [Produces("application/json")]
        public bool Update([FromBody]PostRequest postRequest)
        {
            postRequest.UserId = User.GetUserId();
            return _postService.Update(postRequest);
        }

        [HttpDelete("delete/{id}")]
        [Produces("application/json")]
        public bool Delete(Guid id)
        {
            return _postService.Delete(id);
        }
    }
}