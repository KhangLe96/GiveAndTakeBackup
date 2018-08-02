using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;
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

        [HttpGet("getAll")]
        [Produces("application/json")]
        public List<PostResponse> GetAllCategories()
        {
            //Review: Missing query to filter, pagination. Let's see CategoryController. 1 post contains many categories, so that categories should be an array instead of an object
            return _postService.GetAllPost();
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public PostResponse Create([FromBody]PostRequest postRequest)
        {
            //postRequest.UserId = User.GetUserId();
            return _postService.Create(postRequest);
        }
    }
}