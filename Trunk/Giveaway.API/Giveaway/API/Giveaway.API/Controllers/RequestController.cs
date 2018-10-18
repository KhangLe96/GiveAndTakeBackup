using Giveaway.API.Shared.Extensions;
using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Requests.Request;
using Giveaway.API.Shared.Responses;
using Giveaway.API.Shared.Responses.Request;
using Giveaway.API.Shared.Services.APIs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/request")]
    public class RequestController : BaseController
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Get list request with params object that includes: page, limit, keyword, requestStatus
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("list")]
        [Produces("application/json")]
        public PagingQueryResponse<RequestPostResponse> GetList([FromHeader]IDictionary<string, string> @params)
        {
            return _requestService.GetRequestForPaging(null, @params);
        }

        /// <summary>
        /// Get list request by postId with params object that includes: page, limit, keyword, requestStatus
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("list/{postId}")]
        [Produces("application/json")]
        public PagingQueryResponse<RequestPostResponse> GetListByPostId(string postId, [FromHeader]IDictionary<string, string> @params)
        {
            return _requestService.GetRequestForPaging(postId, @params);
        }

        /// <summary>
        /// Create a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create")]
        [Produces("application/json")]
        public RequestPostResponse Create([FromBody]RequestPostRequest request)
        {
            request.UserId = User.GetUserId();
            return _requestService.Create(request);
        }

        /// <summary>
        /// Change status of request: Approved, Pending, Rejected
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("status/{requestId}")]
        [Produces("application/json")]
        public bool UpdateStatus(Guid requestId, [FromBody]StatusRequest request)
        {
            return _requestService.UpdateStatus(requestId, request);
        }

        /// <summary>
        /// Delete a request by id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("delete/{requestId}")]
        [Produces("application/json")]
        public bool Delete(Guid requestId)
        {
            return _requestService.Delete(requestId);
        }

        /// <summary>
        /// Check if an user requested a post or not
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("checkUserRequest/{postId}/{userId}")]
        [Produces("application/json")]
        public object CheckUserRequest(Guid postId, Guid userId)
        {
            return _requestService.CheckUserRequest(postId, userId);
        }
    }
}