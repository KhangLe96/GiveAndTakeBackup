using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Giveaway.API.Shared.Requests;

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
            return _postService.GetAllPost();
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public List<PostResponse> Create(PostRequest postRequest)
        {
            return _postService.GetAllPost();
        }
    }
}